using TekhneCafe.Core.DataAccess.Concrete.EntityFramework;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework.Context;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.DataAccess.Concrete.EntityFramework
{
    public class EfCartLineDal : EfEntityRepositoryBase<CartLine, EfTekhneCafeContext>, ICartLineDal
    {
        public Guid id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Guid cartLineId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
