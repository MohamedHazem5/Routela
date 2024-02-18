using Routela.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routela.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
         IBlogRepository Blog { get; }
        ICategoryRepository Category { get; }
        ICommentRepository Comment { get; }
        ICourseRepository Course { get; }
        ILectureRepository Lecture { get; }
        IOrderRepository Order { get; }
        IReviewRepository Review { get; }
        ITagRepository Tag { get; }
        IUserRepository User { get; }
        IUserCourseRepository UserCourse { get; }
        ITagBlogRepository TagBlog { get; }
        ITagCourseRepository TagCourse { get; }
        IImageRepository Image { get; }
        Task<int> Save();
    }
}
