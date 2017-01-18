///-----------------------------------------------------------------------
/// <copyright file="BaseDatabase.cs" company="Sergey Teplyashin">
///     Copyright (c) 2010-2012 Sergey Teplyashin. All rights reserved.
/// </copyright>
/// <author>Тепляшин Сергей Васильевич</author>
/// <email>sergio.teplyashin@gmail.com</email>
/// <license>
///     This program is free software; you can redistribute it and/or modify
///     it under the terms of the GNU General Public License as published by
///     the Free Software Foundation; either version 3 of the License, or
///     (at your option) any later version.
///
///     This program is distributed in the hope that it will be useful,
///     but WITHOUT ANY WARRANTY; without even the implied warranty of
///     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
///     GNU General Public License for more details.
///
///     You should have received a copy of the GNU General Public License
///     along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </license>
/// <date>26.10.2012</date>
/// <time>12:59</time>
/// <summary>Defines the BaseDatabase class.</summary>
///-----------------------------------------------------------------------

namespace ComponentLib.Data
{
    #region Using directives
    
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Db4objects.Db4o;
    using Db4objects.Db4o.Config;
    
    #endregion
    
    /// <summary>
    /// Description of Database.
    /// </summary>
    public class BaseDatabase : ITransaction, IDatabase
    {
        private IObjectContainer data;
        private List<object> tables;
        private string fileName;
        private Dictionary<string, object> parameters;
        private DatabaseState state;
        
        public BaseDatabase()
        {
            this.tables = new List<object>();
            this.parameters = new Dictionary<string, object>();
            this.fileName = string.Empty;
            this.state = DatabaseState.Closed;
        }
        
        public event EventHandler<EventArgs> DatabaseClosing;
        
        public event EventHandler<EventArgs> DatabaseClosed;
        
        public event EventHandler<EventArgs> DatabaseOpened;
        
        public event EventHandler<DatabaseOpeningEventArgs> DatabaseOpening;

        public string FileName
        {
            get { return this.fileName; }
        }
        
        #region IDatabase interface implemented
        
        /// <summary>
        /// Gets the opened flag.
        /// </summary>
        /// <value>true, если база данных открыта.</value>
        public bool Opened
        {
            get { return this.state == DatabaseState.Opened; }
        }
        
        public DatabaseState State
        {
            get { return this.state; }
        }
        
        public IObjectContainer Data
        {
            get { return this.data; }
        }
        
        /// <summary>
        /// Gets the collection list stored in IDatabase.
        /// </summary>
        /// <value>Список коллекций, хранящихся в IDatabase.</value>
        public IEnumerable<IExtCollection> Collections
        {
            get { return this.tables.OfType<IExtCollection>(); }
        }
        
        /// <summary>
        /// Gets the true if database is empty, false otherwise.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                bool isEmpty = true;
                foreach (IExtCollection c in this.Collections)
                {
                    isEmpty = isEmpty && c.IsEmpty;
                    if (!isEmpty)
                    {
                        break;
                    }
                }
                
                return isEmpty;
            }
        }
        
        public IBaseCollection<T> GetCollection<T>()
        {
            foreach (IExtCollection c in this.Collections)
            {
                if (c.ContentsType == typeof(T))
                {
                    return (IBaseCollection<T>)c;
                }
            }
            
            return null;
        }
        
        #endregion
        
        #region ITransaction interface implemented
        
        public void StartTransaction()
        {
            foreach (ITransaction t in this.tables)
            {
                t.StartTransaction();
            }
        }
        
        public void Commit()
        {
            foreach (ITransaction t in this.tables)
            {
                t.Commit();
            }
        }
        
        public void Rollback()
        {
            foreach (ITransaction t in this.tables)
            {
                t.Rollback();
            }
        }
        
        #endregion
        
        /// <summary>
        /// Метод создает новую базу данных и подготавливает ее для работы. Никакой дополнительной информации
        /// в БД не добавляется (реализация этого полностью ложится на программиста). Открытие БД, которая
        /// создана этим методом (без добавления доп. информации), приведет к ошибке открытия.
        /// </summary>
        /// <param name="fileName">Имя создаваемого файла.</param>
        public void CreateEmpty(string fileName)
        {
            bool creating = this.state == DatabaseState.Creating;
            
            try
            {
                this.state = DatabaseState.Creating;
                this.fileName = fileName;
                if (this.data != null)
                {
                    this.data.Close();
                }
                
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                
                this.PrepareAndOpenDatabase();
                if (!creating)
                {
                    this.state = DatabaseState.Opened;
                }
            }
            catch (Exception e)
            {
                this.state = DatabaseState.Closed;
                throw new Exception(e.Message);
            }
        }
        
        public void Create(string fileName)
        {
            this.state = DatabaseState.Creating;
            try
            {
                this.CreateEmpty(fileName);
                this.CreateDefaultRecords();
                this.state = DatabaseState.Opened;
                this.OnDatabaseOpened();
            }
            catch (Exception e)
            {
                this.state = DatabaseState.Closed;
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Открывает существующую базу данных.
        /// </summary>
        /// <param name="name">Имя файла базы данных.</param>
        public void Open(string name)
        {
            if (this.data != null)
            {
                this.data.Close();
                this.state = DatabaseState.Closed;
            }
            
            this.state = DatabaseState.Opening;
            try
            {
                this.fileName = name;
                this.PrepareAndOpenDatabase();
                this.state = DatabaseState.Opened;
                this.OnDatabaseOpened();
            }
            catch
            {
                this.data.Close();
                this.data = null;
                this.state = DatabaseState.Closed;
            }
        }
        
        /// <summary>
        /// Закрывает открытую базу данных.
        /// </summary>
        public void Close()
        {
            if (this.data != null)
            {
                this.state = DatabaseState.Closing;
                this.OnDatabaseClosing();
                this.data.Close();
                this.data = null;
                this.fileName = string.Empty;
                this.state = DatabaseState.Closed;
                this.OnDatabaseClosed();
                this.parameters.Clear();
                
            }
        }
        
        public void Backup()
        {
            if (this.data != null)
            {
                this.data.Ext().Backup(this.fileName + ".backup");
            }
        }
        
        public void AddCollection(IExtCollection collection)
        {
            this.tables.Add(collection);
        }
        
        public void AddCollections(IEnumerable<IExtCollection> collections)
        {
            this.tables.AddRange(collections);
        }
        
        public void AddDatabaseParameter(string name, object parameter)
        {
            if (this.parameters.ContainsKey(name))
            {
                this.parameters[name] = parameter;
            }
            else
            {
                this.parameters.Add(name, parameter);
            }
        }
        
        public object GetDatabaseParameter(string name)
        {
            if (this.parameters.ContainsKey(name))
            {
                return this.parameters[name];
            }
            else
            {
                return null;
            }
        }
        
        protected virtual void CreateDefaultRecords() {}
        
        protected virtual void OnPrepareAndOpenDatabase(IEmbeddedConfiguration config)
        {
        }
        
        protected virtual void PrepareAndOpenDatabase()
        {
            IEmbeddedConfiguration config = Db4oEmbedded.NewConfiguration();
            config.Common.ActivationDepth = 7;
            this.OnPrepareAndOpenDatabase(config);
            if (this.DatabaseOpening != null)
            {
                this.DatabaseOpening(this, new DatabaseOpeningEventArgs(config));
            }
            
            this.data = Db4oEmbedded.OpenFile(config, this.fileName);
        }
        
        private void OnDatabaseClosing()
        {
            if (this.DatabaseClosing != null)
            {
                this.DatabaseClosing(this, new EventArgs());
            }
        }
        
        private void OnDatabaseClosed()
        {
            if (this.DatabaseClosed != null)
            {
                this.DatabaseClosed(this, new EventArgs());
            }
        }
        
        private void OnDatabaseOpened()
        {
            if (this.DatabaseOpened != null)
            {
                this.DatabaseOpened(this, new EventArgs());
            }
        }
    }
}
