namespace Routela.DataAccess.Repository
{
    public class UserCourseRepository : Repository<UserCourse>, IUserCourseRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public  async Task<bool> CheckCourse(int courseId,int userId)
        {
            var result = await _context.UsersCourses.FirstOrDefaultAsync(x=>x.CourseId==courseId && x.UserId==userId);

           if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
