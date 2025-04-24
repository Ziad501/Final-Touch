using FinalTouch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Core.Interfaces
{
    public interface IProductCommandRepository<T> where T : BaseEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool ProductExists(int id);
        Task<bool> SaveChangesAsync();
    }

}
