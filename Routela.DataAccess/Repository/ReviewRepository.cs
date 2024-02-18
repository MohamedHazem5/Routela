using Routela.DataAccess.Repository.IRepository;
using Routela.Models;


namespace Routela.DataAccess.Repository
{
    public class ReviewRepository : Repository<UserReview>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
