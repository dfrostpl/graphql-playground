using System;
using System.Collections.Generic;
using System.Linq;

namespace CMS.Base.Models.Entity
{
    public class Entity : RecordBase
    {
        public Guid? DefinitionId { get; set; }
        public List<Property> Properties { get; set; } = new List<Property>();
        public List<Relation> Relations { get; set; } = new List<Relation>();

        public Property GetProperty(string name)
        {
            return Properties.FirstOrDefault(p => p.Name == name);
        }

        public Relation GetRelation(string name)
        {
            return Relations.FirstOrDefault(r => r.Name == name);
        }

        public void SetProperty<T>(string name, T value)
        {
            var property = GetProperty(name);
            if (property != null)
                property.Value = value;
        }

        public void AddRelation(string name, Guid relatedEntityId)
        {
            var relation = GetRelation(name);
            if (relation != null && relatedEntityId != Guid.Empty && !relation.RelatedEntitiesIds.Contains(relatedEntityId))
                relation.RelatedEntitiesIds.Add(relatedEntityId);
        }

        public void SetRelations(string name, List<Guid> relatedEntityIds)
        {
            var relation = GetRelation(name);
            if (relation != null && relatedEntityIds != null)
                relation.RelatedEntitiesIds = relatedEntityIds;
        }
    }
}