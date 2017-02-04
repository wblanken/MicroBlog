using System.Threading.Tasks;
using MicroBlog.Entities;
using MicroBlog.Repository;

namespace MicroBlog.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MicroBlogContext _context;

        public UnitOfWork(MicroBlogContext context)
        {
            _context = context;
            Posts = new Repository<Post>(_context);
        }

        public IRepository<Post> Posts { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
