using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseIdentity 
    {
        private readonly StoreContext _cotext;

        public GenericRepository(StoreContext context)
        {
            _cotext = context;
        }    
        
        public async Task<T> GetByID(int id)
        {
            return await _cotext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllSync()
        {
             return await _cotext.Set<T>().ToListAsync();
        }


        public async Task<T> GetentitywithSpec(Ispecification<T> spec)
        {
            return await applySPecification(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(Ispecification<T> spec)
        { 
            return await applySPecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(Ispecification<T> spec)
        {
            return await applySPecification(spec).CountAsync();
        }

        private IQueryable<T> applySPecification(Ispecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_cotext.Set<T>().AsQueryable(),spec);
        }

    }
}