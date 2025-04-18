using FinalTouch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

            IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
            Task<bool> Complete();

    }
}
