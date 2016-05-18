using MoM.Module.Models;
using MoM.Module.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MoM.Module.Interfaces
{
    public interface ICountryRepository : IDataRepository
    {
        void Create(Country entity);
        void Update(Country entity);
        void Delete(Country entity);
        IQueryable<Country> Table();
        IQueryable<Country> Fetch(Expression<Func<Country, bool>> predicate);
        IQueryable<Country> Fetch(Expression<Func<Country, bool>> predicate, Action<Orderable<Country>> order);
        IQueryable<Country> Fetch(Expression<Func<Country, bool>> predicate, Action<Orderable<Country>> order, int skip, int count);

        Country Country(int id);
        IEnumerable<Country> Countries(int pageNo, int pageSize, string sortColumn, bool sortByAscending);
    }
}
