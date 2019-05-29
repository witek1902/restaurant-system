namespace OrderManagementSystem.Domain.Common
{
    using System;
    using NHibernate.Proxy;

    /// <summary>
    /// Base class for all classes that will be used in the NHibernate context
    /// </summary>
    /// <typeparam name="TKey">Type of primary key</typeparam>
    public class Entity<TKey> where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// The main key        
        /// </summary>
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Object comparison
        /// </summary>
        /// <param name="other">Object to compare</param>
        /// <returns>TRUE if the objects are the same, otherwise FALSE</returns>
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
        /// Object comparison
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>TRUE if the objects are the same, otherwise FALSE</returns>
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
        /// Returns HashCode
        /// </summary>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}