namespace HashBL
{
    public interface IAccount
    {
        /// <summary>
        /// Возвращает статус аккаунта
        /// </summary>
        public string Status { get; }

        /// <summary>
        /// Возвращает баланс
        /// </summary>
        public int Balance { get; }

        /// <summary>
        /// Зачисляет деньги на баланс
        /// </summary>
        /// <param name="count"> Количество зачисляемых средств </param>
        public void AddMoney(int count);

        /// <summary>
        /// Отчет об аккаунте, это могут быть его какие то данные, или жалования пользователя
        /// Или может какие нибудь логи
        /// </summary>
        public void Report();
    }
}
