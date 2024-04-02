using AutoMapper;
using Dominio.Interfaces.Generic;
using Infraestrutura.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infraestrutura.Repository.Generic
{
    public class RepositoryBase<T> : IGeneric<T> where T : class
    {
        private readonly ContextBase _contextBase;
        
        
        public RepositoryBase(ContextBase contextBase)
        {
            _contextBase = contextBase;
            
        }

        public async Task Add(T entity)
        {

             await _contextBase.Set<T>().AddAsync(entity);
             await _contextBase.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entityId = await _contextBase.Set<T>().FindAsync(id);
            _contextBase.Set<T>().Remove(entityId);
            await _contextBase.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _contextBase.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _contextBase.Set<T>().FindAsync(id);
        }

        public async Task Update( T entity)
        {
            //var entityId = await _contextBase.Set<T>().FindAsync(id);
            _contextBase.Set<T>().Update(entity);
            await _contextBase.SaveChangesAsync();
        }
    }
}
