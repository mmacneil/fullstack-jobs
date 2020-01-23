using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStackJobs.GraphQL.Core.Interfaces.Gateways.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetById(int id);
        Task<List<T>> ListAll();
        Task<T> GetSingleBySpec(ISpecification<T> spec);
        Task<List<T>> List(ISpecification<T> spec);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
