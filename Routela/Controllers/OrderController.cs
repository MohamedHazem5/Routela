using Microsoft.AspNetCore.Mvc;
using Routela.DataAccess.Repository.IRepository;
using Routela.Models;
using Routela.Models.DTO;
using Stripe.Checkout;

namespace Routela.Controllers
{
    public class OrderController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
/*        [HttpGet]
        public async Task<IActionResult> GetOrder() {
            var orders = await _unitOfWork.Order.GetAll();
            return Ok(orders);
        }*/

        [HttpGet]
        public async Task<IActionResult> GetOrderByUser(int id)
        {
            var user = await _unitOfWork.Order.GetAllAsync(x=>x.UserId==id, c=>c.Course);

            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            bool flag = await _unitOfWork.UserCourse.CheckCourse(orderDto.CourseId, orderDto.UserId);

            if(flag)
            {
                return BadRequest("User Already bought the course");
            }
            var order = await _unitOfWork.Order.CreateOrder(orderDto);
            
            var result = new UserCourse
            {
               CourseId = orderDto.CourseId,
               UserId = orderDto.UserId,
            };
            var course = await _unitOfWork.Course.FirstOrDefaultAsync(x=>x.Id==orderDto.CourseId);

            await _unitOfWork.UserCourse.AddAsync(result);

            await _unitOfWork.Save();

            var domain = "http://127.0.0.1:5500/test.html";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                  "card",
                },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"/suck.html",
                CancelUrl = domain + $"/fool.html",
            };

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(order.Price * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = course.Title,
                        },
                    },
                    Quantity = 1,
                };
                options.LineItems.Add(sessionLineItem);


            var service = new SessionService();
            Session session = service.Create(options);

/*            Response.Headers.Add("Location", session.Url);
*/            return new StatusCodeResult(303);


        }
    }

    }

