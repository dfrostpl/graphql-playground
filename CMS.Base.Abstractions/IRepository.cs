namespace CMS.Base.Abstractions
{
    public interface IRepository
    {
        IDefinitionRepository Definitions { get; }
        IEntityRepository Entities { get; }
    }
}