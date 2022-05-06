﻿using Kaidao.Domain.Specifications;

namespace Kaidao.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        void Add(TEntity obj);

        TEntity GetById(string id);

        IQueryable<TEntity> GetAll();

        SpecificationResponse<TEntity> GetAll(ISpecification<TEntity> spec);

        IQueryable<TEntity> GetAllSoftDeleted();

        void Update(TEntity obj);

        void Remove(Guid id);

        int SaveChanges();
    }
}