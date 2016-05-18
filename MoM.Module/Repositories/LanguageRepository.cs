using MoM.Module.Base;
using MoM.Module.Interfaces;
using MoM.Module.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoM.Module.Extensions;
using System.Linq.Expressions;

namespace MoM.Module.Repositories
{
    public class LanguageRepository : RepositoryBase<Language>, ILanguageRepository
    {
        public void Create(Language entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Language entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> Fetch(Expression<Func<Language, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> Fetch(Expression<Func<Language, bool>> predicate, Action<Orderable<Language>> order)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> Fetch(Expression<Func<Language, bool>> predicate, Action<Orderable<Language>> order, int skip, int count)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Language> Table()
        {
            throw new NotImplementedException();
        }

        public void Update(Language entity)
        {
            throw new NotImplementedException();
        }
    }
}
