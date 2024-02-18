using Routela.DataAccess.Repository.IRepository;
using Routela.Models;


namespace Routela.DataAccess.Repository
{
    public class LectureRepository : Repository<Lecture>, ILectureRepository
    {
        private readonly ApplicationDbContext _context;

        public LectureRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
