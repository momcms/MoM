namespace MoM.Module.Interfaces
{
    public interface IDataStorage
    {
        T GetRepository<T>() where T : IDataRepository;
        void Save();
    }
}
