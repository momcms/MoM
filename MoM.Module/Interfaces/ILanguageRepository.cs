using MoM.Module.Extensions;
using MoM.Module.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MoM.Module.Interfaces
{
    public interface ILanguageRepository : IDataRepository
    {
        void Create(Language entity);
        void Update(Language entity);
        void Delete(Language entity);
        IQueryable<Language> Table();
        IQueryable<Language> Fetch(Expression<Func<Language, bool>> predicate);
        IQueryable<Language> Fetch(Expression<Func<Language, bool>> predicate, Action<Orderable<Language>> order);
        IQueryable<Language> Fetch(Expression<Func<Language, bool>> predicate, Action<Orderable<Language>> order, int skip, int count);
    }
}
