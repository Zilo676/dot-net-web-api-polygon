using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.QueryFilter
{
    public static class QueryFilterCreator<TModel>
    {
        public static Expression<Func<TModel, bool>> GetFilterExpression(QueryFilter? filter)
        {
            if (filter == null || filter.Filters == null || !filter.Filters.Any())
            {
                return null;
            }

            Expression<Func<TModel, bool>> filterExpression = null;

            if (filter.Logic?.ToLower() == "and")
            {
                filterExpression = GetAndExpression(filter.Filters);
            }

            else if (filter.Logic?.ToLower() == "or")
            {
                filterExpression = GetOrExpression(filter.Filters);
            }

            if (filter.Logic == null && filter.Filters.Count == 1)
            {
                filterExpression = GetOrExpression(filter.Filters);
            }

            return filterExpression;
        }

        private static Expression<Func<TModel, bool>> GetAndExpression(List<Filter> filters)
        {
            if (filters == null || !filters.Any())
            {
                return null;
            }

            var parameter = Expression.Parameter(typeof(TModel), "x");
            Expression andExpression = null;

            foreach (var filter in filters)
            {
                var filterExpression = BuildFilterExpression(filter, parameter);
                if (filterExpression != null)
                {
                    if (andExpression == null)
                    {
                        andExpression = filterExpression;
                    }
                    else
                    {
                        andExpression = Expression.AndAlso(andExpression, filterExpression);
                    }
                }
            }

            if (andExpression == null)
            {
                andExpression = Expression.Constant(false);
            }

            return Expression.Lambda<Func<TModel, bool>>(andExpression, parameter);
        }

        private static Expression<Func<TModel, bool>> GetOrExpression(List<Filter> filters)
        {
            if (filters == null || !filters.Any())
            {
                return null;
            }

            var parameter = Expression.Parameter(typeof(TModel), "x");
            Expression orExpression = null;

            foreach (var filter in filters)
            {
                var filterExpression = BuildFilterExpression(filter, parameter);
                if (filterExpression != null)
                {
                    if (orExpression == null)
                    {
                        orExpression = filterExpression;
                    }
                    else
                    {
                        orExpression = Expression.OrElse(orExpression, filterExpression);
                    }
                }
            }

            if (orExpression == null)
            {
                orExpression = Expression.Constant(false);
            }

            return Expression.Lambda<Func<TModel, bool>>(orExpression, parameter);
        }

        private static Expression BuildFilterExpression(Filter filter, ParameterExpression parameter)
        {
            if (filter.Filters != null && filter.Filters.Any())
            {
                if (filter.Logic?.ToLower() == "and")
                {
                    var andFilters = filter.Filters.Select(filter => BuildFilterExpression(filter, parameter));
                    return andFilters.Aggregate(Expression.AndAlso);
                }
                else if (filter.Logic?.ToLower() == "or")
                {
                    var orFilters = filter.Filters.Select(filter => BuildFilterExpression(filter, parameter));
                    return orFilters.Aggregate(Expression.OrElse);
                }
            }

            //if (filter.Value == null || string.IsNullOrWhiteSpace(filter.Value.ToString()))
            //{
            //    return null;
            //}

            var property = Expression.Property(parameter, filter.Field);
            ConstantExpression? value;

            try
            {
                value = CreateConstantExpression(property, filter);
            }
            catch
            {
                throw new Exception(
                    $"Ошибка создания переменной для сравнения с полем {filter.Field} со значением {filter.Value}");
            }

            switch (filter.Operator.ToLower())
            {
                case "eq":
                {
                    return Expression.Equal(property, value);
                }
                case "neq":
                {
                    return Expression.NotEqual(property, value);
                }
                case "lt":
                {
                    return Expression.LessThan(property, value);
                }
                case "lte":
                {
                    return Expression.LessThanOrEqual(property, value);
                }
                case "gt":
                {
                    return Expression.GreaterThan(property, value);
                }
                case "gte":
                {
                    // TODO: delete
                    //if (Nullable.GetUnderlyingType(property.Type) != null)
                    //{
                    //    (MemberExpression, MemberExpression) extraExpressions = GetNullableExpressions(property);
                    //    var gteExp = Expression.GreaterThanOrEqual(extraExpressions.Item1, value);
                    //    return Expression.AndAlso(extraExpressions.Item2, gteExp);
                    //}
                    return Expression.GreaterThanOrEqual(property, value);
                }
                case "contains":
                {
                    var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    return Expression.Call(property, method, value);
                }
                default:
                    throw new ArgumentException($"Unsupported operator: {filter.Operator}");
            }
        }

        //private static (MemberExpression hasValueExpression, MemberExpression valueExpression) GetNullableExpressions(MemberExpression property)
        //{
        //    return (Expression.Property(property, "Value"), Expression.Property(property, "HasValue"));
        //}

        private static ConstantExpression CreateConstantExpression(MemberExpression property, Filter filter)
        {
            if (filter.Value == null)
            {
                return Expression.Constant(null, property.Type);
            }

            // Поле guid и не nullable
            if (property != null && filter.Field.ToLower().Contains("id") && property.Type == typeof(Guid))
            {
                //return Expression.Constant(Convert.ChangeType(Guid.Parse((string)filter.Value), property.Type));
                Guid.TryParse((string)filter.Value, out var id);
                return Expression.Constant(id, property.Type);
            }

            // Поле guid и nullable
            if (property != null && filter.Field.ToLower().Contains("id") && property.Type == typeof(Guid?))
            {
                //return Expression.Constant(Convert.ChangeType(Guid.Parse((string)filter.Value), Nullable.GetUnderlyingType(property.Type)));
                Guid.TryParse((string)filter.Value, out var id);
                return Expression.Constant((Guid?)id, property.Type);
            }

            // Поле enum и не nullable
            if (property != null && property.Type.IsEnum)
            {
                return Expression.Constant(Enum.Parse(property.Type, filter.Value.ToString()));
            }

            // Поле nullable
            if (property != null && Nullable.GetUnderlyingType(property.Type) != null)
            {
                var a = Nullable.GetUnderlyingType(property.Type);
                return Expression.Constant(Convert.ChangeType(filter.Value, Nullable.GetUnderlyingType(property.Type)),
                    property.Type);
            }

            // Поле не nullable
            if (property != null && filter.Value != null)
            {
                return Expression.Constant(Convert.ChangeType(filter.Value, property.Type), property.Type);
            }

            return Expression.Constant(null, typeof(object));
        }
    }
}