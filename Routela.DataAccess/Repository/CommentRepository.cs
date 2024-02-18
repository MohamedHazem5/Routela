using Routela.DataAccess.Repository.IRepository;
using Routela.Models;


namespace Routela.DataAccess.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
