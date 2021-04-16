using System;
using System.Collections.Generic;
using System.Text;

namespace AppUI
{
    public interface IDataManager
    {
        /// <summary>
        /// Путь расположения данных.
        /// </summary>
        public string PathData { get; }

        /// <summary>
        /// Сохранить данные.
        /// </summary>
        public abstract void SaveData();

        /// <summary>
        /// Найти пользователя.
        /// </summary>
        /// <param name="key"> Хеш логина. </param>
        /// <param name="value"> Выходной параметр - аккаунт пользователя. </param>
        /// <returns> Возвращает TRUE, если пользователь найден, иначе возвращает FALSE/ </returns>
        public abstract bool SearchUser(ulong key, out Account value);
    }
}
