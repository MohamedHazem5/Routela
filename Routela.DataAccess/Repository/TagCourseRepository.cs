


namespace Routela.DataAccess.Repository
{
    public class TagCourseRepository : Repository<TagCourse>, ITagCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public TagCourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
