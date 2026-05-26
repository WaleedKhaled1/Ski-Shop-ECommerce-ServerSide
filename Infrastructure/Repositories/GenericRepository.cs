using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class GenericRepository<T>(StoreDbContext storeDbContext) : IGenericRepository<T> where T : BaseEntity
    {
        public void Add(T entity)
        {
            storeDbContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            storeDbContext.Set<T>().Remove(entity);
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await storeDbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await storeDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public bool IsExist(int id)
        {
            return storeDbContext.Set<T>().Any(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> ListWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }


        public async Task<bool> SaveChangesAsync()
        {
            return await storeDbContext.SaveChangesAsync() > 0;
        }


        public void Update(T entity)
        {
            storeDbContext.Set<T>().Entry(entity).State = EntityState.Modified;
        }


        public IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(storeDbContext.Set<T>().AsQueryable(), spec);
        }

        public IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(storeDbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<TResult?> GetEntityWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
        {
           return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TResult>> ListWithSpecAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
           var query=storeDbContext.Set<T>().AsQueryable();

           var countQuery=spec.ApplyCriteia(query);

            return await countQuery.CountAsync();
        }
    }
    }

