using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ICartLineService
    {
        Task AddCartLineAsync(CartLine cartLine);
        Task UpdateCartLineAsync(CartLine cartLine);
        Task DeleteCartLineAsync(Guid cartLineId);
    }
}
