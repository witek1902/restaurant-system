namespace OrderManagementSystem.Infrastructure.Command
{
    using Castle.Windsor;
    using NHibernate;
    using Exception;
    using Query;

    /// <summary>
    /// Interfejs do obsługi komend
    /// </summary>
    /// <typeparam name="T">Zwracany rezultat</typeparam>
    public interface ICommand<T>
    {
        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        T Execute();

        /// <summary>
        /// Sprawdza, czy komenda może zostać wykonana
        /// </summary>
        /// <returns>Prawda, jeśli komenda może zostać wykonana, w innym przypadku fałsz</returns>
        bool CanExecute();
    }

    /// <summary>
    /// Klasa bazowa dla komend
    /// </summary>
    /// <typeparam name="T">Zwracany typ</typeparam>
    public abstract class Command<T> : ICommand<T>
    {
        /// <summary>
        /// Wywołuje komendę i zwraca wskazany typ
        /// </summary>
        /// <returns>Rezultat</returns>
        public abstract T Execute();

        /// <summary>
        /// Sprawdza, czy komenda może zostać wykonana
        /// </summary>
        /// <returns>Prawda, jeśli komenda może zostać wykonana, w innym przypadku fałsz</returns>
        public virtual bool CanExecute()
        {
            return true;
        }

        /// <summary>
        /// Uruchamia zapytanie. 
        /// </summary>
        protected virtual TResult Query<TResult>(Query<TResult> queryToExecute)
        {
            var sessionProvider = this as INeedSession;
            if (sessionProvider == null)
                throw new TechnicalException("");
            return queryToExecute.Execute(sessionProvider.Session);
        }

        /// <summary>
        /// Dodawanie własnych zależności do komendy.
        /// </summary>
        /// <param name="container">Kontener IoC</param>
        public abstract void SetupDependencies(IWindsorContainer container);
    }

    /// <summary>
    /// Interfejs, który mówi, że do CommandRunnera należy wstrzyknąć sesje NHibernate
    /// </summary>
    public interface INeedSession
    {
        /// <summary>
        /// Sesja NHibernate.
        /// </summary>
        ISession Session { get; set; }
    }

    /// <summary>
    /// Interfejs, który mówi, że CommandRanner powinien być uruchomiony w transakcji
    /// Jeśli podczas 'Execute' nastąpi wyjątek, transakcja jest wycofywana.
    /// </summary>
    public interface INeedAutocommitTransaction { }
}