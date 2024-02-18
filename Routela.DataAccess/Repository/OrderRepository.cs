using Routela.Models.DTO;

namespace Routela.DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(OrderDto orderDto)
        {
            
       
            var order = new Order
            {
                UserId = orderDto.UserId,
                Price = orderDto.Price,
                CourseId = orderDto.CourseId,
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;

        }
    }
}
