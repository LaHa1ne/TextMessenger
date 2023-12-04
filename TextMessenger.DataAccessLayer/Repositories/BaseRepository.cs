using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextMessenger.DataAccessLayer.Interfaces;

namespace TextMessenger.DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _db;
        public BaseRepository(ApplicationDbContext db) 
        { 
            _db = db;
        }

        public async Task Create(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _db.Set<T>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}
