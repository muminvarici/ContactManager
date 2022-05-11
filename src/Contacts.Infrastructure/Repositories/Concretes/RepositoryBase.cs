using Contacts.Domain.Entities;
using Contacts.Domain.Repositories.Abstracts;
using Contacts.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Contacts.Infrastructure.Repositories.Concretes;

public class GenericRepository<TData> : IGenericRepository<TData> where TData : ModelBase
{
    private readonly IMongoCollection<TData> _collection;

    public GenericRepository(IOptions<DbSettings> options)
    {
        if (string.IsNullOrWhiteSpace(options.Value?.ConnectionString)) throw new ArgumentNullException(nameof(DbSettings));

        var client = new MongoClient(options.Value.ConnectionString);
        var database = client.GetDatabase(options.Value.Database);
        _collection = database.GetCollection<TData>(GetName(typeof(TData).Name));
    }

    private string GetName(string name) => $"{char.ToLowerInvariant(name[0])}{name[1..]}";

    public async Task<TData> CreateAsync(TData data)
    {
        data.CreatedAt = DateTime.Now;
        await _collection.InsertOneAsync(data);
        return data;
    }


    public async Task<List<TData>> FilterAsync(Expression<Func<TData, bool>> expression)
    {
        var result = await (await _collection.FindAsync(expression)).ToListAsync();
        return result.ToList();
    }

    public async Task<List<TData>> GetAllAsync()
    {
        var result = await _collection.AsQueryable().ToListAsync();
        return result;
    }

    public async Task<TData> GetOneAsync(string id)
    {
        return await (await _collection.FindAsync(w => w.Id == id)).FirstOrDefaultAsync();
    }

    public async Task<bool> RemoveAsync(TData data)
    {
        return await RemoveAsync(data.Id);
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var result = await _collection.DeleteOneAsync(w => w.Id == id);
        return result.IsAcknowledged;
    }

    public async Task<bool> UpdateAsync(TData data)
    {
        data.UpdatedAt = DateTime.Now;
        var result = await _collection.ReplaceOneAsync(w => w.Id == data.Id, data);
        return result.IsAcknowledged;
    }
}
