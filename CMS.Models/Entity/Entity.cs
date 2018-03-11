using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Models.Entity
{
    public class Entity : DbObject
    {
        public Guid? DefinitionId { get; set; }
        public List<EntityMember> Properties { get; set; } = new List<EntityMember>();

        public EntityMember GetProperty(string name)
        {
            return Properties.FirstOrDefault(p => p.Name == name);
        }

        public void SetProperty<T>(string name, T value)
        {
            var property = Properties.FirstOrDefault(p => p.Name == name);
            if(property != null)
                property.Value = value;
        }
    }
}