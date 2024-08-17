using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public MyContext( DbContextOptions<MyContext> options ) : base( options )
        {
            Database.Migrate( );
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating( modelBuilder );
            modelBuilder.Entity<UserEntity>( new UserMap( ).Configure );

            modelBuilder.Entity<UserEntity>( ).HasData(
                new UserEntity
                {
                    Id = Guid.NewGuid( ),
                    Name = "Administrador",
                    Email = "leandro@email.com",
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                }
            );
        }
    }
}