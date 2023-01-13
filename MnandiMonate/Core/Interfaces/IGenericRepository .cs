using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.specifications;

namespace Core.Interfaces

{
    public interface IGenericRepository<T> where T: BaseIdentity 
    {
        Task<T> GetByID(int id);

        Task<IReadOnlyList<T>> ListAllSync();

        Task<T> GetentitywithSpec(Ispecification<T> spec); 


        Task<IReadOnlyList<T>> ListAsync(Ispecification<T> spec);

        Task<int> CountAsync(Ispecification<T> spec);
    }
}