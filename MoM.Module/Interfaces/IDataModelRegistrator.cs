using Microsoft.Data.Entity;

namespace MoM.Module.Interfaces
{
    public interface IDataModelRegistrator
    {
        void RegisterModels(ModelBuilder modelbuilder);
    }
}
