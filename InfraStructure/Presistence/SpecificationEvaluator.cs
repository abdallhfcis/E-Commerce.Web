using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Presistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity ,TKey>(IQueryable<TEntity> InputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = InputQuery;

            if(specifications.Criteria != null)
            {
                Query = Query.Where(specifications.Criteria);
            }
            if(specifications.OrderBy != null)
            {
                Query=Query.OrderBy(specifications.OrderBy);
            }
            if(specifications.OrderByDecending != null)
            {
                Query=Query.OrderByDescending(specifications.OrderByDecending);
            }
            if(specifications.IncludeExpression != null && specifications.IncludeExpression.Count >0)
            {
                Query=specifications.IncludeExpression.Aggregate(Query,(CurrentQuery,IncludeExpression)=> CurrentQuery.Include(IncludeExpression));
            }

            return Query;
        }
    }
}
