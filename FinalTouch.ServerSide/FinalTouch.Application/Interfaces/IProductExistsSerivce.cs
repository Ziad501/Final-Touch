using System;
using FinalTouch.Core.Entities;

namespace FinalTouch.Core.Interfaces;

public interface IProductExistsSerivce
{

    bool ProductExists(int id);

}
