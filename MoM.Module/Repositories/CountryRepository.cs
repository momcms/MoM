using MoM.Module.Interfaces;
using MoM.Module.Models;
using MoM.Module.Base;
using System;
using System.Linq;
using MoM.Module.Extensions;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace MoM.Module.Repositories
{
    public class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public IEnumerable<Country> Countries(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            switch (sortColumn.ToLower())
            {
                case "name":
                    if (sortByAscending)
                        return DbSet
                            .OrderBy(p => p.Name)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                    else
                        return DbSet
                            .OrderByDescending(p => p.Name)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                case "culturecode":
                    if (sortByAscending)
                        return DbSet
                            .OrderBy(p => p.CultureCode)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                    else
                        return DbSet
                            .OrderByDescending(p => p.CultureCode)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                case "currencyname":
                    if (sortByAscending)
                        return DbSet
                            .OrderBy(p => p.CurrencyName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                    else
                        return DbSet
                            .OrderByDescending(p => p.CurrencyName)
                            .Skip(pageNo * pageSize)
                            .Take(pageSize);
                default:
                    return DbSet
                        .OrderByDescending(p => p.Name)
                        .Skip(pageNo * pageSize)
                        .Take(pageSize);
            }
        }

        public Country Country(int id)
        {
            return DbSet.FirstOrDefault(x => x.CountryId == id);
        }

        public void Create(Country entity)
        {
            DbSet.Add(entity);
            Db.SaveChanges();
        }

        public void Delete(Country entity)
        {
            DbSet.Remove(entity);
            Db.SaveChanges();
        }

        public IQueryable<Country> Fetch(Expression<Func<Country, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<Country> Fetch(Expression<Func<Country, bool>> predicate, Action<Orderable<Country>> order)
        {
            var orderable = new Orderable<Country>(Fetch(predicate));
            order(orderable);
            return orderable.Queryable;
        }

        public IQueryable<Country> Fetch(Expression<Func<Country, bool>> predicate, Action<Orderable<Country>> order, int skip, int count)
        {
            return Fetch(predicate, order).Skip(skip).Take(count);
        }

        public IQueryable<Country> Table()
        {
            return DbSet.AsQueryable();
        }

        public void Update(Country entity)
        {
            DbSet.Update(entity);
            Db.SaveChanges();
        }
    }
}
