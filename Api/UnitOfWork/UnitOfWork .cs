using Api.Models;
using Api.Repositories;

namespace Api.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IBaseRepository<Student> Students { get; }
        public IBaseRepository<Course> Courses { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Students = new BaseRepository<Student>(context);
            Courses = new BaseRepository<Course>(context);
        }

        public async Task<int> Complete()
                => await _context.SaveChangesAsync();

    }
}
