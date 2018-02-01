namespace OrderManagementSystem.Domain.Common
{
    using System;
    using NHibernate.Proxy;

    /// <summary>
    /// Klasa bazowa dla wszystkich klas, które będą używane w kontekscie NHibernate'a
    /// </summary>
    /// <typeparam name="TKey">Rodzaj klucza głównego</typeparam>
    public class Entity<TKey> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// Klucz główny
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Porównanie obiektów
        /// </summary>
        /// <param name="other">Obiekt do porównania</param>
        /// <returns>TRUE, jeśli obiekty są takie same, w innym przypadku FALSE</returns>
        public virtual bool Equals(Entity<TKey> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            var thisType = (this is INHibernateProxy) ? GetType().BaseType : GetType();
            var otherType = (other is INHibernateProxy) ? other.GetType().BaseType : other.GetType();

            if (thisType != otherType)
            {
                return false;
            }

            return Id.CompareTo(default(TKey)) != 0 && other.Id.CompareTo(default(TKey)) != 0 && other.Id.Equals(Id);
        }

        /// <summary>
        /// Porównanie obiektów
        /// </summary>
        /// <param name="obj">Obiekt do porównania</param>
        /// <returns>TRUE, jeśli obiekty są takie same, w innym przypadku FALSE</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return Equals(obj as Entity<TKey>);
        }

        /// <summary>
        /// Zwraca HashCode
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}