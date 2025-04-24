using FinalTouch.Core.Entities;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTouch.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductCommandRepository<TEntity> CommandRepository<TEntity>() where TEntity : BaseEntity;
        IProductQueryRepository<TEntity> QueryRepository<TEntity>() where TEntity : BaseEntity;
        Task<bool> Complete();


    }
}
