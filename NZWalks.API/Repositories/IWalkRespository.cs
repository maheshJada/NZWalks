using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRespository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filteron=null, string? filterQuery=null, 
            string? sortBy=null, bool IsAscending=true,
            int pageNumber = 1, int pageSize = 1000);
        Task<Walk?> GetByIdasync(Guid id);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);

    }
}
