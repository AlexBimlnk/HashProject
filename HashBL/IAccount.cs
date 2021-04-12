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
        /// Зачисляет деньги на баланс
        /// </summary>
        public void AddMoney(int a);

        /// <summary>
        /// Отчет об аккаунте, это могут быть его какие то данные, или жалования пользователя
        /// Или может какие нибудь логи
        /// </summary>
        public void Report();
    }
}
