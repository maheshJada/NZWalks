using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.regions.AddAsync(region);
            await dbContext.SaveChangesAsync(); 
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var exstingRegion = await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            if (exstingRegion != null)
            {
                return null;

            }
            dbContext.regions.Remove(exstingRegion);
            await dbContext.SaveChangesAsync();
            return exstingRegion;
        }

        public  async Task<List<Region>> GetAllAsync()
        {
           return await dbContext.regions.ToListAsync();
        }

        public async Task<Region?> GetByIDAsync(Guid id)
        {
           return await dbContext.regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion=await dbContext.regions.FirstOrDefaultAsync(x=>x.Id == id);
            if(existingRegion != null)
            {
                return null;
            }
            existingRegion.Code= region.Code;
            existingRegion.Name= region.Name;
            existingRegion.RegionImageUrl= region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
