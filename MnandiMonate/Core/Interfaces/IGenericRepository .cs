using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces

{
    public interface IGenericRepository<T> where T: BaseIdentity 
    {
        Task<T> GetByID(int id);

        Task<IReadOnlyList<T>> ListAllSync();
    }
}