﻿using System;
using System.Collections.Generic;
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
        protected BaseSpecifications(Expression<Func<TEntity,bool>>? CriteriaExpression)
        {
            Criteria=CriteriaExpression;
        }
        public Expression<Func<TEntity, bool>>? Criteria {get;private set;}

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];

        public void AddInclude ( Expression<Func<TEntity, object>> includeExpression) => IncludeExpression.Add(includeExpression);

}
}
