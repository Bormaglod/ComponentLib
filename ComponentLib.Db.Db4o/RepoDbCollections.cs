//-----------------------------------------------------------------------
// <copyright file="BDDatabase.cs" company="Sergey Teplyashin">
//     Copyright (c) 2010-2016 Sergey Teplyashin. All rights reserved.
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
// <date>19.06.2014</date>
// <time>10:43</time>
// <summary>Defines the BDDatabase class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Db4o
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Db4objects.Db4o;
    using Db4objects.Db4o.Config;
    using Repositories;
    
    public class RepoDbCollections : RepoCollections
    {
        IObjectContainer container;
        string fileName;
        Stack<Session> sessions;
        
        public RepoDbCollections()
        {
            fileName = string.Empty;
            sessions = new Stack<Session>();
        }
        
        public event EventHandler<DatabaseConfigEventArgs> ConfigCreate;
        
        public event EventHandler<EventArgs> CreateDefaultRecords;
        
        public IObjectContainer Container
        {
            get { return container; }
        }
        
        public string FileName
        {
            get { return fileName; }
        }
        
        public Session Session
        {
            get
            {
                return sessions.Count == 0 ? null : sessions.Peek();
            }
            
            internal set
            {
                if (value == null)
                {
                    if (sessions.Count > 0)
                    {
                        sessions.Pop();
                    }
                }
                else
                {
                    sessions.Push(value);
                }
            }
        }

        protected void Create(string fileName, IEnumerable<Tuple<string, object>> parameters, bool clearParameters, bool createDefault = true)
        {
            try
            {
                CreateEmpty(fileName, clearParameters);
                if (parameters != null)
                {
                    foreach (var param in parameters)
                    {
                        AddParameter(param.Item1, param.Item2);
                    }
                }
                
                if (createDefault)
                {
                    OnCreateDefaultRecords();
                }
                
                DoAfterOpen();
            }
            catch (Exception e)
            {
                Close();
                throw new Exception(e.Message);
            }
        }
        
        public void Recreate(bool createDefault = true)
        {
            Create(fileName, null, false, createDefault);
        }
        
        /// <summary>
        /// Открывает существующую базу данных.
        /// </summary>
        /// <param name="name">Имя файла базы данных.</param>
        public void Open(string name)
        {
            fileName = name;
            Open();
        }
        
        public void Backup()
        {
            if (container != null)
            {
                container.Ext().Backup(fileName + ".backup");
            }
        }
        
        protected override void CloseDatabase()
        {
            if (container != null)
            {
                container.Close();
                container = null;
            }
        }
        
        protected override void OpenDatabase()
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("File name required");
            }
            
            IEmbeddedConfiguration config = Db4oEmbedded.NewConfiguration();
            config.Common.UpdateDepth = 2;
            foreach (IPropertyRepository repo in Collections)
            {
                foreach (PropertyInfo pi in repo.ContentsType.GetProperties().Where(p => p.PropertyType == typeof(DbImage)))
                {
                    config.Common.ObjectClass(repo.ContentsType).ObjectField(pi.Name).CascadeOnUpdate(true);
                    config.Common.ObjectClass(repo.ContentsType).ObjectField(pi.Name).CascadeOnDelete(true);
                }
            }
            
            OnConfigCreate(config);
            container = Db4oEmbedded.OpenFile(config, fileName);
        }
        
        protected virtual void DoCreateDefaultRecords() {}

        protected virtual void DoBeforeCreating() { }

        /// <summary>
        /// Метод создает новую базу данных и подготавливает ее для работы. Никакой дополнительной информации
        /// в БД не добавляется (реализация этого полностью ложится на программиста). Открытие БД, которая
        /// создана этим методом (без добавления доп. информации), приведет к ошибке открытия.
        /// </summary>
        /// <param name="dbFileName">Имя создаваемого файла.</param>
        /// <param name = "clearParameters">true, если необходимо очистить спмсок параметров БД.</param>
        void CreateEmpty(string dbFileName, bool clearParameters)
        {
            try
            {
                Close(clearParameters);

                fileName = dbFileName;
                DoCreating();
                DoBeforeCreating();
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                OpenDatabase();
            }
            catch (Exception e)
            {
                Close();
                throw new Exception(e.Message);
            }
        }
        
        void OnConfigCreate(IEmbeddedConfiguration config)
        {
            ConfigCreate?.Invoke(this, new DatabaseConfigEventArgs(config));
        }
        
        void OnCreateDefaultRecords()
        {
            CreateDefaultRecords?.Invoke(this, new EventArgs());
            DoCreateDefaultRecords();
        }
    }
}
