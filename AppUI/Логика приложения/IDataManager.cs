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
        public void SaveData();

        /// <summary>
        /// Добавить данные.
        /// </summary>
        /// <param name="login"> Логин пользователя. </param>
        /// <param name="password"> Пароль пользователя. </param>
        public void AddData(string login, string password);

        /// <summary>
        /// Проверяет записан ли уже данный пользователь.
        /// </summary>
        /// <param name="key"> Хеш-ключ пользователя. </param>
        /// <param name="value"> Значение у ключа, если пользователь будет найден. </param>
        /// <returns> Возвращает TRUE, если пользователь найден, иначе возвращает FALSE. </returns>
        public bool SearchUser(ulong key, out Account value);
    }
}
