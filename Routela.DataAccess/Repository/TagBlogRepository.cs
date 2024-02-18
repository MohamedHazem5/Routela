using Routela.DataAccess.Repository.IRepository;
using Routela.Models;


namespace Routela.DataAccess.Repository
{
    public class TagBlogRepository : Repository<TagBlog>, ITagBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public TagBlogRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
