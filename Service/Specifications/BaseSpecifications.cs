using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Services.Specifications
{
    abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        #region InCLude
        protected BaseSpecifications(Expression<Func<TEntity,bool>>? CriteriaExpression)
        {
            Criteria=CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria {get;private set;}

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = []; 
        public void AddInclude ( Expression<Func<TEntity, object>> includeExpression) => IncludeExpression.Add(includeExpression);
        #endregion


        #region Sorting
        public Expression<Func<TEntity, object>> OrderBy {get;private set;}
        public Expression<Func<TEntity, object>> OrderByDecending { get; private set; }

        public void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) => OrderBy=orderByExp;
        public void AddOrderByDesxending(Expression<Func<TEntity, object>> orderByDescExp) => OrderByDecending = orderByDescExp;

        #endregion


        #region Pagination
        public int Take { get; private set; }

        public int Sikp { get; private set; }

        public bool IsPaginated { get; set; }
        //40
        //size=10
        //index=3
        //[10 ,10,10,10]
        protected void ApplyPagintion(int PageSize,int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Sikp =(PageIndex-1)*PageSize;
            
        }

        #endregion

    }
}
