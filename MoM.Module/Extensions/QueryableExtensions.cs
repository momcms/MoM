using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MoM.Module.Extensions
{
    public static class QueryableExtensions
    {

        /// <summary>
        /// Gets the where in expression.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        private static Expression<Func<TElement, bool>> GetWhereInExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            var enumerable = values as TValue[] ?? values.ToArray();
            if (!enumerable.Any())
                return e => false;

            var equals = enumerable.Select(value => (Expression)Expression.Equal(propertySelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate(Expression.Or);
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
        /// <summary>
        /// Gets the where like expression.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="propertySelector">The property selector.</param>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        private static Expression<Func<TElement, bool>> GetWhereLikeExpression<TElement, TValue>(Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            ParameterExpression p = propertySelector.Parameters.Single();
            var enumerable = values as TValue[] ?? values.ToArray();
            if (!enumerable.Any())
                return e => false;
            var propertyName = (((MemberExpression)(propertySelector.Body)).Member).Name;
            var parameterExp = ((ParameterExpression)(((MemberExpression)(propertySelector.Body)).Expression)); //Expression.Parameter(typeof(TElement), "i");
            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            //var like = enumerable.Select(value => (Expression)GetExpression<ItemsDto>(propertyName, value.ToString()));
            var like = enumerable.Select(value => (Expression)Expression.Call(propertyExp, method, Expression.Constant(value, typeof(TValue))));

            var body = like.Aggregate(Expression.Or);
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
        /// <summary> 
        /// Return the element that the specified property's value is contained in the specifiec values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, params TValue[] values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static IQueryable<TElement> WhereLike<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, params TValue[] values)
        {
            return source.Where(GetWhereLikeExpression(propertySelector, values));
        }

        /// <summary> 
        /// Return the element that the specified property's value is contained in the specifiec values 
        /// </summary> 
        /// <typeparam name="TElement">The type of the element.</typeparam> 
        /// <typeparam name="TValue">The type of the values.</typeparam> 
        /// <param name="source">The source.</param> 
        /// <param name="propertySelector">The property to be tested.</param> 
        /// <param name="values">The accepted values of the property.</param> 
        /// <returns>The accepted elements.</returns> 
        public static IQueryable<TElement> WhereIn<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            return source.Where(GetWhereInExpression(propertySelector, values));
        }

        public static IQueryable<TElement> WhereLike<TElement, TValue>(this IQueryable<TElement> source, Expression<Func<TElement, TValue>> propertySelector, IEnumerable<TValue> values)
        {
            return source.Where(GetWhereLikeExpression(propertySelector, values));
        }
    }
}
