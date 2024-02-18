using Routela.DataAccess.Repository.IRepository;
using Routela.Models;


namespace Routela.DataAccess.Repository
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
