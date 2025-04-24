using System;
using FinalTouch.Core.Entities;

namespace FinalTouch.Core.Interfaces;

public interface IProductSaveService
{
    Task<bool> SaveChangesAsync();
}
