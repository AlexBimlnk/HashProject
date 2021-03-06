namespace HashBL
{
    public interface IHashTable <Tvalue>
    {
        /// <summary>
        /// Добавление элемента в хеш-словарь.
        /// </summary>
        /// <param name="loginHash"> Ключ. </param>
        /// <param name="passwordHashData"> Значение. </param>
        /// <exception cref="OverflowException"> Возникает когда достигнуто макс. кол-во элементов. </exception>
        /// <exception cref="ArgumentException"> Возникает при попытке добавить одинаковых пользователей. </exception>
        public void AddHash(ulong key, Tvalue value);

        /// <summary>
        /// Проверяет вхождение ключа в коллекцию.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <returns></returns>
        public bool Contains(ulong key);

        /// <summary>
        /// Возвращает значение данного ключа.
        /// </summary>
        /// <param name="key"> Ключ искомого значения. </param>
        /// <returns> Значение ключа, если он существует. Иначе вернет default. </returns>
        public Tvalue GetValueByKey(ulong key);

        /// <summary>
        /// Сериализует хеш-словарь класса в бинарный файл.
        /// </summary>
        /// <param name="path"> Путь к создаваемому файлу. </param>
        public void Serealize(string path);

        /// <summary>
        /// Десериализует бинарный файл в хеш-словарь.
        /// </summary>
        /// <param name="path"> Путь к десериализуемому файлу. </param>
        /// <exception cref="FileNotFoundException"> Путь не указывает на существующий файл.  </exception>
        public void Deserialize(string path);
    }
}
