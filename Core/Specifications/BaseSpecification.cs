using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Specification
{
    public class BaseSpecification<T> (Expression<Func<T, bool>>? criteria) : ISpecification<T>
    {
        BaseSpecification():this(null)
        {
        }


        public Expression<Func<T, bool>>? Criteria => criteria;

        public Expression<Func<T, object>>? OrderBy {  get;private set; }

        public Expression<Func<T, object>>? OrderByDesc {  get;private set; }

        public bool IsDistinct { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public IQueryable<T> ApplyCriteia(IQueryable<T> query)
        {

            var qry = query;
            if (Criteria != null)
            {
                qry = query.Where(Criteria);
            }
            return qry;
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDesc = orderByDescExpression;
        }

        protected void ApplyDistinct()
        {
             IsDistinct= true;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }

    public class BaseSpecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseSpecification<T>(criteria),
        ISpecification<T, TResult>
    {
        public BaseSpecification() : this(null)
        {
        }
        public Expression<Func<T, TResult>> Select { get; private set; }
       
        protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
        {
            Select = selectExpression;
        }
    }
}
