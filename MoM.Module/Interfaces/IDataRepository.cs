namespace MoM.Module.Interfaces
{
    public interface IDataRepository
    {
        void SetStorageContext(IApplicationDbContext storageContext);
    }
}
