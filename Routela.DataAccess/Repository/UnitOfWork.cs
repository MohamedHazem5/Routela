namespace Routela.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBlogRepository Blog { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public ICommentRepository Comment { get; private set; }

        public ICourseRepository Course { get; private set; }

        public ILectureRepository Lecture { get; private set; }


        public IOrderRepository Order { get; private set; }

        public IReviewRepository Review { get; private set; }

        public ITagRepository Tag { get; private set; }

        public IUserRepository User { get; private set; }

        public IUserCourseRepository UserCourse { get; private set; }

        public ITagBlogRepository TagBlog { get; private set; }

        public ITagCourseRepository TagCourse { get; private set; }
        public IImageRepository Image { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Blog = new BlogRepository(_context);
            Category = new CategoryRepository(_context);
            Comment = new CommentRepository(_context);
            Course = new CourseRepository(_context);
            Lecture = new LectureRepository(_context);
            Order = new OrderRepository(_context);
            Review = new ReviewRepository(_context);
            TagBlog = new TagBlogRepository(_context);
            TagCourse = new TagCourseRepository(_context);
            Tag = new TagRepository(_context);
            UserCourse = new UserCourseRepository(_context);
            User = new UserRepository(_context);
            Image = new ImageRepository(_context);

        }

        public void Dispose()
        {
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }


    }
}

