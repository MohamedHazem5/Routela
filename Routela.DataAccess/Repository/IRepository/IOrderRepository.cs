using Routela.Models;
using Routela.Models.DTO;

namespace Routela.DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> CreateOrder(OrderDto orderDto);
    }
}
