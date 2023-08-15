namespace TekhneCafe.Business.Abstract
{
    public interface ICartLineProductAttributeService
    {
        Task<bool> CartLineProductAttributeExistsAsync(string id);
    }
}
