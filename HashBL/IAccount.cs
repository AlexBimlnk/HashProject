using System;
using System.Collections.Generic;
using System.Text;

namespace HashBL
{
    public interface IAccount
    {
        /// <summary>
        /// Возвращает баланс, можно сделать сеттер приватным
        /// </summary>
        public int Balance { get; }

        /// <summary>
        /// Возвращает информацию об аккаунте (заморожен/забанен например, обычный, вип)
        /// </summary>
        public string Status { get; }
        

        /// <summary>
        /// Зачисляет деньги на баланс
        /// </summary>
        public void AddMoney();

        /// <summary>
        /// Отчет об аккаунте, это могут быть его какие то данные, или жалования пользователя
        /// Или может какие нибудь логи
        /// </summary>
        public void Report();

        /// <summary>
        /// Удаляет экземпляр класса из БД
        /// </summary>
        public void Delete();
    }
}
