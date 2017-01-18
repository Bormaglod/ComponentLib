//-----------------------------------------------------------------------
// <copyright file="IExtCollection.cs" company="Sergey Teplyashin">
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
// <date>17.10.2011</date>
// <time>13:31</time>
// <summary>Defines the IExtCollection interface.</summary>
//-----------------------------------------------------------------------

namespace ComponentLib.Data
{
    using System;
    using System.Collections;
    
    /// <summary>
    /// Description of IExtCollection.
    /// </summary>
    public interface IExtCollection
    {
        long Count { get; }
        
        IEnumerable Collection { get; }
        
        bool IsEmpty { get; }
        
        Type ContentsType { get; }
        
        /// <summary>
        /// Метод очищает коллекцию, удаляя все данные содержащиеся в ней.
        /// </summary>
        void Clear();
        
        /// <summary>
        /// Метод устанавливает режим начала обновления коллекции. Каждый BeginUpdate должен быть
        /// в паре с EndUpdate.
        /// </summary>
        void BeginUpdate();
        
        /// <summary>
        /// Метод устанавливает режим окончания обновления коллекции.
        /// </summary>
        void EndUpdate();
        
        /// <summary>
        /// <para>Метод вызывается при удалении объекта из какой-либо коллекции IDatabase и предназначен
        /// для проверки возможности удаления этого объекта. Для запрещения удаления объекта
        /// коллекция должна перекрыть этот метод и вернуть false.</para>
        /// <para>Метод вызывается до удаления рбъекта и до вызова ObjectRemoving.</para>
        /// </summary>
        /// <param name="removingObject">Удаляемый объект.</param>
        bool CanObjectRemoving(object removingObject);
        
        /// <summary>
        /// Метод вызывается при удалении объекта из какой-либо коллекции IDatabase. Вызов этого метода
        /// происходит после вызова CanObjectRemoving (причем он должен вернуть true для всех коллекций).
        /// В этом методе можно предусмотреть дополнительную обработку при удаление объектов из других
        /// коллекций.
        /// <param name="removingObject">Удаляемый объект.</param>
        void ObjectRemoving(object removingObject);
    }
}
