using ApiGeo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiGeo.Data.Context
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public virtual DbSet<AddressHistory> AddressHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
