namespace CMS.Base.ProviderContracts
{
    public interface IRepository
    {
        IDefinitionRepository Definitions { get; }
        IEntityRepository Entities { get; }
    }
}