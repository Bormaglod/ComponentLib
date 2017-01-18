//-----------------------------------------------------------------------
// <copyright file="Session.cs" company="Sergey Teplyashin">
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
// <date>25.12.2014</date>
// <time>12:47</time>
// <summary>Defines the Session class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Db4o
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Repositories;
    
    public class Session : IDisposable
    {
        bool disposed;
        readonly RepoDbCollections collections;
        Queue<Tuple<Entity, ObjectAction>> objects;
        
        public Session(RepoDbCollections repoCollections)
        {
            objects = new Queue<Tuple<Entity, ObjectAction>>();
            collections = repoCollections;
            repoCollections.Session = this;
            foreach (IBaseRepository c in collections.Collections.OfType<IBaseRepository>())
            {
                c.EntityModified += CollectionEntityModified;
            }
        }
        
        ~Session()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        public void Commit()
        {
            foreach (var e in objects.Where(x => x.Item2 == ObjectAction.Modify))
            {
                collections.Container.Store(e.Item1);
            }
            
            objects.Clear();
        }
        
        void Rollback()
        {
            foreach (IBaseRepository c in collections.Collections.OfType<IBaseRepository>())
            {
                c.EntityModified -= CollectionEntityModified;
            }

            while (objects.Count > 0)
            {
                Tuple<Entity, ObjectAction> key = objects.Dequeue();
                IBaseRepository repo = (IBaseRepository)collections.Get(key.Item1.GetType());
                switch (key.Item2)
                {
                    case ObjectAction.Undefined:
                        
                        break;
                    case ObjectAction.Add:
                        repo.Execute(key.Item1, ObjectAction.Delete);
                        break;
                    case ObjectAction.Delete:
                        repo.Execute(key.Item1, ObjectAction.Modify);
                        break;
                    case ObjectAction.Modify:
                        ((Db4objects.Db4o.Ext.IExtObjectContainer)collections.Container).Refresh(key.Item1, 1);
                        break;
                    default:
                        throw new Exception("Invalid value for ObjectAction");
                }
            }
        }
        
        void CollectionEntityModified(object sender, EntityEventArgs e)
        {
            objects.Enqueue(new Tuple<Entity, ObjectAction>(e.Entity, e.Action));
        }
        
        void Dispose(bool disposing)
        {
            if(!disposed)
            {
                Rollback();
                collections.Session = null;
                disposed = true;
            }
        }
    }
}
