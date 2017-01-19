//-----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Тепляшин Сергей Васильевич">
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
// <time>14:22</time>
// <summary>Defines the Entity class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Базовый класс для всех объектов хранящихся в коллекциях базы данных. При создании объекта
    /// IsNew всегда равно true. При добавлении объекта в коллекцию, IsNew присваивается false.
    /// </summary>
    public abstract class Entity : INotifyPropertyChanged
    {
        protected Entity()
        {
            foreach (PropertyInfo pi in GetType().GetProperties().Where(p => p.PropertyType == typeof(DbImage)))
            {
                pi.SetValue(this, DbImage.Create(), null);
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Флаг определяющий наличие объекта в коллекции базы данных. Если IsNew равно true,
        /// то объект только что создан, false - объект уже есть в коллекции и записан на диске.
        /// </summary>
        public bool IsNew { get; internal set; } = true;
        
        /// <summary>
        /// Метод возвращает строку, являющуюся ключем данной записи. Если объект содержит
        /// ключевый свойства, данный метод должен быть переопределен.
        /// </summary>
        /// <returns></returns>
        public abstract string Key();
        
        public void CopyTo(Entity entity, bool deep = false)
        {
            if (entity.GetType() != GetType())
            {
                return;
            }
            
            foreach (PropertyInfo p in GetType().GetProperties())
            {
                if (p.PropertyType.GetInterface(nameof(IList)) != null)
                {
                    IList source = (IList)p.GetValue(this, null);
                    IList dest = (IList)p.GetValue(entity, null);
                    dest.Clear();
                    foreach (var x in source)
                    {
                        dest.Add(CreateCopyObject(x, deep));
                    }
                }
                else if (p.CanWrite)
                {
                    MethodInfo mi = p.GetSetMethod(true);
                    if (mi.IsPublic)
                    {
                        p.SetValue(entity, CreateCopyObject(p, deep), null);
                    }
                }
            }
        }
        
        // FIXME: ContainsObject
        /// <summary>
        /// Возвращает true, если одно из свойств объекта содержит entity. Метод ОДНОУРОВНЕВЫЙ и не работает
        /// в глубину. Т.е., если свойство - это список или словарь или объект Entity, то метод их проверять 
        /// не будет, даже если они содержат entity. ВРЕМЕНННО. НАДО УТОЧНИТЬ.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ContainsObject(Entity entity)
        {
            foreach (PropertyInfo fi in GetType().GetProperties())
            {
                object v = fi.GetValue(this, null);
                if (v == null)
                {
                    continue;
                }
                
                if (ReferenceEquals(v, entity))
                {
                    return true;
                }
            }

            return false;
        }
        
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        object CreateCopyObject(object source, bool deep) => CreateCopyObject(source.GetType(), source, deep);
        
        object CreateCopyObject(PropertyInfo source, bool deep) => CreateCopyObject(source.PropertyType, source.GetValue(this, null), deep);
        
        object CreateCopyObject(Type sourceType, object source, bool deep)
        {
            if (deep && sourceType == typeof(Entity))
            {
                ConstructorInfo ci = sourceType.GetConstructor(Type.EmptyTypes);
                Entity entity = (Entity)ci.Invoke(null);
                ((Entity)source).CopyTo(entity);
                return entity;
            }

            return source;
        }
    }
}
