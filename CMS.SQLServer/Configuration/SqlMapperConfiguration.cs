using System.Linq;
using AutoMapper;
using CMS.Base.Models.Definition;
using CMS.Base.Models.Entity;
using CMS.Providers.SQL.Adapters.Definition;
using CMS.Providers.SQL.Adapters.Entity;

namespace CMS.Providers.SQL.Configuration
{
    public class SqlMapperConfiguration
    {
        public MapperConfiguration Configuration { get;}
        public SqlMapperConfiguration()
        {
            Configuration = new MapperConfiguration(cfg =>
            {
                CreatePropertyDefinitionMapping(cfg);
                CreateRelationDefinitionMapping(cfg);
                CreatePropertyMapping(cfg);
                CreateRelationMapping(cfg);
                CreateEntityMapping(cfg);
                CreateDefinitionMapping(cfg);
            });
        }

        private void CreatePropertyDefinitionMapping(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<PropertyDefinitionAdapter, PropertyDefinition>(MemberList.Destination)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest=>dest.Type, opt=>opt.MapFrom(src=>src.Type));
        }

        private void CreateRelationDefinitionMapping(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<RelationDefinitionAdapter, RelationDefinition>(MemberList.Destination)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Cardinality, opt => opt.MapFrom(src => src.Cardinality))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.RelatedDefinitionId, opt => opt.MapFrom(src => src.RelatedDefinitionId));
        }

        private void CreatePropertyMapping(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<PropertyAdapter, Property>(MemberList.Destination)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));
        }

        private void CreateRelationMapping(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<RelationAdapter, Relation>(MemberList.Destination)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Cardinality, opt => opt.MapFrom(src => src.Cardinality))
                .ForMember(dest => dest.RelatedDefinitionId, opt => opt.MapFrom(src => src.RelatedDefinitionId))
                .ForMember(dest => dest.ParentIds, opt => opt.MapFrom(src => src.RelatedEntitiesIds.Select(s => s.ParentId)))
                .ForMember(dest => dest.ChildIds, opt => opt.MapFrom(src => src.RelatedEntitiesIds.Select(s => s.ChildId)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
        }

        private void CreateEntityMapping(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<EntityAdapter, Entity>(MemberList.Destination)
                .ForMember(dest => dest.DefinitionId, opt => opt.MapFrom(src => src.DefinitionId))
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties))
                .ForMember(dest => dest.Relations, opt => opt.MapFrom(src => src.Relations));
        }

        private void CreateDefinitionMapping(IMapperConfigurationExpression expression)
        {
            expression.CreateMap<DefinitionAdapter, Definition>(MemberList.Destination)
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties))
                .ForMember(dest => dest.Relations, opt => opt.MapFrom(src => src.Relations));
        }
    }
}
