using Routela.Models;

namespace Routela.DataAccess.Repository.IRepository
{
    public interface IUserCourseRepository : IRepository<UserCourse>
    {
        Task<bool> CheckCourse(int courseId, int userId);
    }
}
