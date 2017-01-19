//-----------------------------------------------------------------------
// <copyright file="RepoCollections.cs" company="Тепляшин Сергей Васильевич">
//     Copyright (c) 2010-2017 Тепляшин Сергей Васильевич. All rights reserved.
// </copyright>
// <author>Тепляшин Сергей Васильевич</author>
// <email>sergio.teplyashin@gmail.com</email>
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
// <date>18.06.2014</date>
// <time>14:45</time>
// <summary>Defines the RepoCollections class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Repositories;
    
    public class RepoCollections
    {
        readonly List<IPropertyRepository> tables = new List<IPropertyRepository>();
        readonly Dictionary<string, object> parameters = new Dictionary<string, object>();

        public event EventHandler<EventArgs> BeforeClose;
        
        public event EventHandler<EventArgs> AfterClose;
        
        public event EventHandler<EventArgs> BeforeOpen;
        
        public event EventHandler<EventArgs> AfterOpen;
        
        /// <summary>
        /// Свойство возвращает состояние базы данных.
        /// </summary>
        public DatabaseState State { get; internal set; } = DatabaseState.Closed;

        /// <summary>
        /// Список коллекций, хранящихся в базе данных.
        /// </summary>
        public IEnumerable<IPropertyRepository> Collections => tables;
        
        /// <summary>
        /// Свойство возвращает true, если все коллекции не содержат ни одного объекта.
        /// </summary>
        public bool IsEmpty => Collections.FirstOrDefault(c => !c.IsEmpty) == null;
        
        public IRepository<T> Get<T>() where T: Entity => Get(typeof(T)) as IRepository<T>;
        
        public IPropertyRepository Get(Type content)
        {
            if (content.IsInterface)
            {
                return Collections.FirstOrDefault(c => c.ContentsType == content);
            }
            
            return Collections.FirstOrDefault(c => c.ContentsType == content);
        }
        
        public IPropertyRepository Get(string contentType) => Collections.FirstOrDefault(c => c.ContentsType.Name == contentType);
        
        public IPropertyRepository Get(int id) => Collections.FirstOrDefault(c => c.Identifier == id);
        
        /// <summary>
        /// Возвращает коллекцию содержащую объекты такого-же типа как item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public IPropertyRepository GetCollectionBelonge(Entity item) => Collections.OfType<IBaseRepository>().FirstOrDefault(c => c.Belonge(item));
        
        /// <summary>
        /// Открывает базу данных.
        /// </summary>
        public void Open()
        {
            try
            {
                DoBeforeOpen();
                OpenDatabase();
                DoAfterOpen();
            }
            catch
            {
                Close();
            }
        }
        
        /// <summary>
        /// Закрывает открытую базу данных.
        /// </summary>
        public void Close()
        {
            Close(true);
        }
        
        public void ClearAllData()
        {
            foreach (IBaseRepository repo in Collections.OfType<IBaseRepository>())
            {
                repo.ClearAllData();
            }
        }
        
        public void SaveToXml(string fileName)
        {
            DatabaseState ds = State;
            try
            {
                State = DatabaseState.Saving;
                XmlDocument doc = new XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", string.Empty));
                
                foreach (IBaseRepository repo in Collections.Where(x => !x.ReadOnly && !x.IsEmpty).OfType<IBaseRepository>())
                {
                    repo.SaveToXml(doc);
                }
                
                doc.Save(fileName);
            }
            finally
            {
                State = ds;
            }
        }
        
        public void LoadFromXml(string fileName)
        {
            DatabaseState ds = State;
            try
            {
                State = DatabaseState.Loading;
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                LoadFromXml(doc);
            }
            finally
            {
                State = ds;
            }
        }
        
        public void AddCollection(IPropertyRepository collection)
        {
            tables.Add(collection);
        }
        
        public void AddCollections(IEnumerable<IPropertyRepository> collections)
        {
            tables.AddRange(collections);
        }
        
        public void AddParameter(string name, object parameter)
        {
            if (parameters.ContainsKey(name))
            {
                parameters[name] = parameter;
            }
            else
            {
                parameters.Add(name, parameter);
            }
        }
        
        public object GetParameter(string name) => parameters.ContainsKey(name) ? parameters[name] : null;
        
        protected void Close(bool clearParameters)
        {
            if (clearParameters)
            {
                parameters.Clear();
            }
            
            DoBeforeClose();
            CloseDatabase();
            DoAfterClose();
        }
        
        protected virtual void OpenDatabase() {}
        
        protected virtual void CloseDatabase() {}
        
        protected virtual void LoadFromXml(XmlDocument document) {}
        
        protected void DoBeforeOpen()
        {
            State = DatabaseState.Opening;
            BeforeOpen?.Invoke(this, new EventArgs());
        }
        
        protected void DoAfterOpen()
        {
            State = DatabaseState.Opened;
            AfterOpen?.Invoke(this, new EventArgs());
        }
        
        protected void DoBeforeClose()
        {
            State = DatabaseState.Closing;
            BeforeClose?.Invoke(this, new EventArgs());
        }
        
        protected void DoAfterClose()
        {
            State = DatabaseState.Closed;
            AfterClose?.Invoke(this, new EventArgs());
        }
        
        protected void DoCreating()
        {
            State = DatabaseState.Creating;
        }
    }
}
