using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;

namespace TekhneCafe.Business.Concrete
{
    public class CartLineProductAttributeManager : ICartLineProductAttributeService
    {
        private readonly ICartLineProductAttributeDal _productAttributeService;

        public CartLineProductAttributeManager(ICartLineProductAttributeDal productAttributeService)
        {
            _productAttributeService = productAttributeService;
        }

        public async Task<bool> CartLineProductAttributeExistsAsync(string id)
            => await _productAttributeService.CartLineProductAttributeExistsAsync(id);
    }
}
