using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Domain.Core
{
    /// <summary>
    /// Usar classes de enumeração em vez de tipos enumerados
    /// https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    /// </summary>
    public abstract class Enumeration : IComparable
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        protected Enumeration() { }
        protected Enumeration(int id, string name) => (Id, Nome) = (id, name);

        public override string ToString() => Nome;
        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);
            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome);
        }

        // Other utility methods ...
    }
}
