using Api.Models;
using Api.Repositories;

namespace Api.UnitOfWork
{
    public interface IUnitOfWork
    {
        IBaseRepository<Student> Students { get; }
        IBaseRepository<Course> Courses { get; }

        Task<int> Complete();
    }
}
