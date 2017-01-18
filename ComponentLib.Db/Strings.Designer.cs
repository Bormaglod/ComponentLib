﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ComponentLib.Db {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Strings() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ComponentLib.Db.Strings", typeof(Strings).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на База данных закрыта. Перед изменением базы данных, ее необходимо предварительно открыть..
        /// </summary>
        internal static string DatabaseClose {
            get {
                return ResourceManager.GetString("DatabaseClose", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объект &apos;{0}&apos; невозможно удалить, т.к. он зависит от объекта &apos;{1}&apos;..
        /// </summary>
        internal static string DeleteEntityError {
            get {
                return ResourceManager.GetString("DeleteEntityError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Error transaction..
        /// </summary>
        internal static string ErrorTransaction {
            get {
                return ResourceManager.GetString("ErrorTransaction", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объект с ключем &apos;{0}&apos; уже существует..
        /// </summary>
        internal static string KeyExist {
            get {
                return ResourceManager.GetString("KeyExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Для объекта &apos;{0}&apos; не указано значение ключа..
        /// </summary>
        internal static string KeyIsEmpty {
            get {
                return ResourceManager.GetString("KeyIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объект &apos;{0}&apos; уже существует..
        /// </summary>
        internal static string ObjectExist {
            get {
                return ResourceManager.GetString("ObjectExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Значение объекта не может быть равно null..
        /// </summary>
        internal static string ObjectIsNull {
            get {
                return ResourceManager.GetString("ObjectIsNull", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объект не находится в режиме редактирования..
        /// </summary>
        internal static string ObjectNotEdit {
            get {
                return ResourceManager.GetString("ObjectNotEdit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объект &apos;{0}&apos; отсутствует в коллекции..
        /// </summary>
        internal static string ObjectNotExist {
            get {
                return ResourceManager.GetString("ObjectNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на Объект&apos;{0}&apos; используется в коллекции &apos;{1}&apos;. Удаление невозможно..
        /// </summary>
        internal static string ObjectUsed {
            get {
                return ResourceManager.GetString("ObjectUsed", resourceCulture);
            }
        }
    }
}
