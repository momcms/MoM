using Microsoft.EntityFrameworkCore;

namespace MoM.Module.Interfaces
{
    public interface IDataModelRegistrator
    {
        void RegisterModels(ModelBuilder modelbuilder);
    }
}
