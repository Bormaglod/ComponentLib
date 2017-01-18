//-----------------------------------------------------------------------
// <copyright file="DataSourceManager.cs" company="Sergey Teplyashin">
//     Copyright (c) 2006 Herre Kuijpers - <herre@xs4all.nl>
//     Copyright (c) 2010-2011 Sergey Teplyashin. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergey-teplyashin@yandex.ru</email>
// <license>
//     This program is free software; you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation; either version 3 of the License, or
//     (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </license>
// <date>27.01.2011</date>
// <time>14:59</time>
// <summary>Defines the DataSourceManager class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Controls.ThirdParty.OutlookStyle
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Data;
    using System.Reflection;
    using System.Windows.Forms;
    
    /// <summary>
    /// the DataDourceManager class is a wrapper class around different types of datasources.
    /// in this case the DataSet, object list using reflection and the OutlooGridRow objects are supported
    /// by this class. Basically the DataDourceManager works like a facade that provides access in a uniform
    /// way to the datasource.
    /// Note: this class is not implemented optimally. It is merely used for demonstration purposes
    /// </summary>
    internal class DataSourceManager
    {
        private object dataSource;
        private string dataMember;

        public ArrayList Columns;
        public ArrayList Rows;

        public DataSourceManager(object dataSource, string dataMember)
        {
            this.dataSource = dataSource;
            this.dataMember = dataMember;
            InitManager();
        }

        /// <summary>
        /// datamember readonly for now
        /// </summary>
        public string DataMember
        {
            get { return dataMember; }
        }

        /// <summary>
        /// datasource is readonly for now
        /// </summary>
        public object DataSource
        {
            get { return dataSource; }
        }

        /// <summary>
        /// this function initializes the DataSourceManager's internal state.
        /// it will analyse the datasource taking the following source into account:
        /// - DataSet
        /// - Object array (must implement IList)
        /// - OutlookGrid
        /// </summary>
 
        private void InitManager()
        {
            if (dataSource is IListSource)
                InitDataSet();
            if (dataSource is IList)
                InitList();
            if (dataSource is OutlookGrid)
                InitGrid();
        }

        private void InitDataSet()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();

            DataTable table = ((DataSet)dataSource).Tables[this.dataMember];
            // use reflection to discover all properties of the object
            foreach (DataColumn c in table.Columns)
                Columns.Add(c.ColumnName);

            foreach (DataRow r in table.Rows)
            {
                DataSourceRow row = new DataSourceRow(this, r);
                for (int i = 0; i < Columns.Count; i++)
                    row.Add(r[i]);
                Rows.Add(row);
            }
        }

        private void InitGrid()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();

            OutlookGrid grid = (OutlookGrid)dataSource;
            // use reflection to discover all properties of the object
            foreach (DataGridViewColumn c in grid.Columns)
                Columns.Add(c.Name);

            foreach (OutlookGridRow r in grid.Rows)
            {
                if (!r.IsGroupRow && !r.IsNewRow)
                {
                    DataSourceRow row = new DataSourceRow(this, r);
                    for (int i = 0; i < Columns.Count; i++)
                        row.Add(r.Cells[i].Value);
                    Rows.Add(row);
                }
            }
        }

        private void InitList()
        {
            Columns = new ArrayList();
            Rows = new ArrayList();
            IList list = (IList)dataSource;

            // use reflection to discover all properties of the object
            BindingFlags bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty;
            PropertyInfo[] props = list[0].GetType().GetProperties();
            foreach (PropertyInfo pi in props)
                    Columns.Add(pi.Name);

            foreach (object obj in list)
            {
                DataSourceRow row = new DataSourceRow(this, obj);
                foreach (PropertyInfo pi in props)
                {
                    object result = obj.GetType().InvokeMember(pi.Name, bf, null, obj, null);
                    row.Add(result);  
                }
                Rows.Add(row);
            }
        }

        public void Sort(System.Collections.IComparer comparer)
        {
            Rows.Sort(new DataSourceRowComparer(comparer));
        }
    }
}
