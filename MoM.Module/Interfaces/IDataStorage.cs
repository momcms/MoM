using System;
using System.Threading;
using System.Threading.Tasks;

namespace MoM.Module.Interfaces
{
    public interface IDataStorage : IDisposable
    {
        T GetRepository<T>() where T : IDataRepository;
        void SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
