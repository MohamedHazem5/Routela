

namespace Routela.DataAccess.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
