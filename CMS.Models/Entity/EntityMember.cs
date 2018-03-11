using System;

namespace CMS.Models.Entity
{
    public class EntityMember
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }

        public T GetValue<T>()
        {
            return (T)Convert.ChangeType(Value, Type);
        }
    }
}