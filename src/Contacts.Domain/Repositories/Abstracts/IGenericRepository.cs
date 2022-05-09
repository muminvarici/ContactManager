using Contacts.Domain.Entities;
using System.Linq.Expressions;

namespace Contacts.Domain.Repositories.Abstracts;

public interface IGenericRepository<TData> where TData : ModelBase
{
    Task<TData> CreateAsync(TData data);
    Task<List<TData>> FilterAsync(Expression<Func<TData, bool>> expression);
    Task<TData> GetOneAsync(string id);
    Task<bool> RemoveAsync(string id);
    Task<bool> RemoveAsync(TData data);
    Task<bool> UpdateAsync(TData data);
    Task<List<TData>> GetAllAsync();
}
