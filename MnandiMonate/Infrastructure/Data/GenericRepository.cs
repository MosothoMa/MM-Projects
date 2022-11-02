using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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
    }
}