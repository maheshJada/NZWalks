using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleID = "60783016-8370-4a62-9412-b3bbe800a5af";
            var writerRoleID = "73d6a7d0-3845-4758-afe6-6832b7ad09ff";
            var roles = new List<IdentityRole> {
                new IdentityRole{
                    Id=readerRoleID,
                    ConcurrencyStamp=readerRoleID,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()

                },
                 new IdentityRole{
                    Id=writerRoleID,
                    ConcurrencyStamp=writerRoleID,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()

                }

            };
            builder.Entity<IdentityRole>().HasData(roles);
            
            

        }
    }
}
