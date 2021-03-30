using System.Collections.Generic;

namespace HashBL
{
    public interface IHashTable
    {
        /// <summary>
        /// Добавление элемента в хеш-словарь.
        /// </summary>
        /// <param name="loginHash"> Ключ. </param>
        /// <param name="passwordHashData"> Значение. </param>
        /// <exception cref="OverflowException"> Возникает когда достигнуто макс. кол-во элементов. </exception>
        /// <exception cref="ArgumentException"> Возникает при попытке добавить одинаковых пользователей. </exception>
        public void AddHash(ulong key, uint[] hash, string salt);

        /// <summary>
        /// Поиск в хеш-словаре по хеш-ключу.
        /// </summary>
        /// <param name="key"> Ключ искомого значения. </param>
        /// <returns> Значение ключа, если он существует. Иначе вернет null. </returns>
        public void Search(ulong key);

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
