using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.IdentityModel.Tokens;
using Presistence.Data;

namespace Presistence.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           var typeName=typeof(TEntity).Name;
            if(_repositories.TryGetValue(typeName,out object? value))
                    return(IGenericRepository<TEntity,TKey>)value;
            else
            {
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories["typeName"] = Repo;
                return(Repo);
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
