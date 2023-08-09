using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekhneCafe.Core.DTOs.CartLine;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Abstract
{
    public interface ICartLineService
    {
        Task AddCartLineAsync(CartLineAddDto cartLineAddDto);
        Task UpdateCartLineAsync(CartLineUpdateDto cartLineUpdateDto);
        Task DeleteCartLineAsync(string id);
    }
}
