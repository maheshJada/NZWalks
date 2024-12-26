using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories
{
    public class SqlWalkRespository : IWalkRespository
    {
        private readonly NZWalksDbContext dbContext;
        public SqlWalkRespository(NZWalksDbContext  dbContext)
        {
            this.dbContext = dbContext;   
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
           await dbContext.walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }


        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalk = await dbContext.walks.FirstOrDefaultAsync(x=>x.Id==id);
            if (existingWalk == null)
            {
                return null;
            }
            dbContext.walks.Remove(existingWalk);
            await dbContext.SaveChangesAsync();
            return existingWalk;    
        }

        public async Task<List<Walk>> GetAllAsync(string? filteron = null, string? filterQuery = null
        , string? sortBy = null, bool IsAscending = true,
            int pageNumber = 1, int pageSize = 1000)

        {
            //filtering
            var walks = dbContext.walks.Include("Difficulity").Include("Region").AsQueryable();
            if(string.IsNullOrWhiteSpace(filteron)==false && string.IsNullOrWhiteSpace(filterQuery)==false) 
            {
                if (filteron.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                { 
                    walks=walks.Where(x=>x.Name.Contains(filterQuery));  
                }
            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                else if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = IsAscending ? walks.OrderBy(x => x.LenghtInKM) : walks.OrderByDescending(x => x.LenghtInKM);
                }
            }
            //pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
           //return await dbContext.walks.Include("Difficulity").Include("Region").ToListAsync();
        }


        public async Task<Walk?> GetByIdasync(Guid id)
        {
          return  await dbContext.walks.
                Include("Difficulity").
                Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
           var ExistingWalk=await dbContext.walks.FirstOrDefaultAsync(x=>x.Id== id);
            if(ExistingWalk != null)
            {
                return null;
            }
            ExistingWalk.Name= walk.Name;
            ExistingWalk.Description= walk.Description;
            ExistingWalk.LenghtInKM = walk.LenghtInKM;
            ExistingWalk.WalkImageUrl = walk.WalkImageUrl;
            ExistingWalk.DifficultyId = walk.DifficultyId;
            ExistingWalk.RegionId = walk.RegionId;
            await dbContext.SaveChangesAsync();
            return ExistingWalk;
        }
    }
}
