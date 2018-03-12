using System;

namespace CMS.Base.Models.Entity
{
    public class Property
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