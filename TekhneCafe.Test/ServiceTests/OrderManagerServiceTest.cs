using AutoMapper;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Helpers.Transaction;
using TekneCafe.SignalR.Abstract;

namespace TekhneCafe.Test.ServiceTests
{
    public class OrderManagerServiceTest
    {
        Mock<IOrderDal> _orderDal = new();
        Mock<IMapper> _mapper = new();
        Mock<IHttpContextAccessor> _httpContextAccessor = new();
        Mock<IOrderHistoryService> _orderHistoryService = new();
        Mock<IWalletService> _walletService = new();
        Mock<IOrderProductService> _orderProductService = new();
        Mock<ITransactionHistoryService> _transactionHistoryService = new();
        Mock<ITransactionManagement> _transactionManagement = new();
        Mock<IOrderNotificationService> _orderNotificationService = new();
        Mock<IOneSignalNotificationService> _oneSignalNotificationService = new();
        Mock<INotificationService> _notificationService = new();
        private readonly ClaimsPrincipal _claimsPrincipal;
        public OrderManagerServiceTest()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "A97D8D47-3228-4DC9-FC81-08DBA3DC8273"),
                // Add other claims as needed
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Bearer");
            _claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        }

        [Fact]
        public async Task CreateOrder_WithValidParameters_ReturnsTask()
        {
            //// Arrange
            //OrderAddDto orderDto = new()
            //{
            //    Description = "description",
            //    OrderProducts = new[] { new OrderProductAddDto() { ProductId = Guid.Parse("F1E37F97-433E-44C0-861B-2D6B193BF054"), Quantity = 3 } }
            //};
            //Order order = new()
            //{
            //    Description = orderDto.Description,
            //    OrderProducts = new[] { new OrderProduct() { ProductId = Guid.Parse("F1E37F97-433E-44C0-861B-2D6B193BF054"), Quantity = 3 } }
            //};

            //var orderDbSetMock = new Mock<DbSet<Order>>();
            //var queryableOrder = new List<Order> { order }.AsQueryable();
            //orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(queryableOrder.Provider);
            //orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(queryableOrder.Expression);
            //orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(queryableOrder.ElementType);
            //orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(() => queryableOrder.GetEnumerator());
            //orderDbSetMock.Setup(_ => _.Include(It.IsAny<Expression<Func<Order, ICollection<TransactionHistory>>>>()))
            //    .Returns((Expression<Func<Order, ICollection<TransactionHistory>>> include) => {
            //        var includedQueryable = queryableOrder.Include(include);
            //        return includedQueryable;
            //    });
            //_orderDal.Setup(_ => _.GetAll(It.IsAny<Expression<Func<Order, bool>>>()))
            //    .Returns(orderDbSetMock.Object);



            //var mockHttpContext = new DefaultHttpContext();
            //mockHttpContext.User = _claimsPrincipal;

            //_mapper.Setup(_ => _.Map<Order>(It.IsAny<OrderAddDto>())).Returns(order);
            //_httpContextAccessor.Setup(_ => _.HttpContext).Returns(mockHttpContext);
            //_httpContextAccessor.Setup(_ => _.HttpContext.User).Returns(mockHttpContext.User);
            //_orderDal.Setup(_ => _.GetAll(It.IsAny<Expression<Func<Order, bool>>>())
            //    .Include(It.IsAny<Expression<Func<Order, ICollection<TransactionHistory>>>>())
            //    .ThenInclude(It.IsAny<Expression<Func<TransactionHistory, AppUser>>>())
            //    .Include(It.IsAny<Expression<Func<Order, ICollection<OrderProduct>>>>()))
            //    .Returns();
            //OrderManager orderManager = new(_orderDal.Object, _mapper.Object, _httpContextAccessor.Object, _orderHistoryService.Object, _walletService.Object, _orderProductService.Object, _transactionHistoryService.Object,
            //    _transactionManagement.Object, _orderNotificationService.Object, _oneSignalNotificationService.Object, _notificationService.Object);

            //// Act
            //await orderManager.CreateOrderAsync(new());

            //// Assert
            //// No need to check the result type; it's already known to be a Task
        }
    }
}
