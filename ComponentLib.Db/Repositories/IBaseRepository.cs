//-----------------------------------------------------------------------
// <copyright file="IBaseRepository.cs" company="Тепляшин Сергей Васильевич">
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
// <date>21.07.2015</date>
// <time>13:23</time>
// <summary>Defines the IBaseRepository class.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Db.Repositories
{
    using System;
    using System.Xml;
    
    public interface IBaseRepository : IPropertyRepository
    {
        event EventHandler<EntityEventArgs> EntityModified;
        
        void Execute(Entity item, ObjectAction action);
        
        /// <summary>
        /// <para>Метод вызывается при удалении объекта из какой-либо коллекции и предназначен
        /// для проверки возможности удаления этого объекта. Для запрещения удаления объекта
        /// коллекция должна перекрыть этот метод и вернуть false.</para>
        /// <para>Метод вызывается до удаления объекта и до вызова ObjectRemoving.</para>
        /// </summary>
        /// <param name="removingItem">Удаляемый объект.</param>
        bool CanObjectRemoving(Entity removingItem);
        
        /// <summary>
        /// Метод вызывается при удалении объекта из какой-либо коллекции. Вызов этого метода
        /// происходит после вызова CanObjectRemoving (причем он должен вернуть true для всех коллекций).
        /// В этом методе можно предусмотреть дополнительную обработку при удаление объектов из других
        /// коллекций.
        /// </summary>
        /// <param name="removingItem">Удаляемый объект.</param>
        void ObjectRemoving(Entity removingItem);
        
        /// <summary>
        /// Метод вызывается после удаления объекта из какой-либо коллекции.
        /// </summary>
        /// <param name="removedItem">Удаленный объект.</param>
        void ObjectRemoved(Entity removedItem);
        
        /// <summary>
        /// Метод возвращает true, если объект item можно добавить в данную коллекцию.
        /// </summary>
        /// <param name="item">Проверяемый объект.</param>
        /// <returns>true, если объект item можно добавить в коллекцию.</returns>
        bool Belonge(Entity item);
        
        void ClearAllData();
        
        void SaveToXml(XmlDocument document);
        
        void LoadFromXml(XmlDocument document);
    }
}
