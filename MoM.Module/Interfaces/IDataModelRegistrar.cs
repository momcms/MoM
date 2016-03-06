using Microsoft.Data.Entity;

namespace MoM.Module.Interfaces
{
    public interface IDataModelRegistrar
    {
        void RegisterModels(ModelBuilder modelbuilder);
    }
}
