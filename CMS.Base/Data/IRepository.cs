namespace CMS.Base.Data
{
    public interface IRepository
    {
        IDefinitionRepository Definitions { get; }
        IEntityRepository Entities { get; }
    }
}