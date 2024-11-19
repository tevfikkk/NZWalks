using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data;

public class NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var readerRoleId = "330e787d-8e97-4a3b-9bb2-0457761e1caa";
        var writerRoleId = "21c38ff7-e573-4231-80db-9ad69993feaf";

        var roles = new List<IdentityRole>
        {
            new () 
            {
                Id = readerRoleId,
                ConcurrencyStamp = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper(),
            },
            new ()
            {
                Id = writerRoleId,
                ConcurrencyStamp = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper(),
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}

