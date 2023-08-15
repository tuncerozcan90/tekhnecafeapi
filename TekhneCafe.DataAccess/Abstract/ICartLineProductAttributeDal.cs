namespace TekhneCafe.DataAccess.Abstract
{
    public interface ICartLineProductAttributeDal
    {
        Task<bool> CartLineProductAttributeExistsAsync(string id);
    }
}
