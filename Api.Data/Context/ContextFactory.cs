using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    internal class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext( string[ ] args )
        {
            var connectionString = "Server=localhost;Port=3306;Database=dbAPI2;Uid=root;Pwd=5326";
            //var connectionString = "Server=.\\LRFDEV;Initial Catalog=dbapi;TrustServerCertificate=true;User ID=sa;Password=1";
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>( );
            optionsBuilder.UseMySql( connectionString, ServerVersion.AutoDetect( connectionString ) );
            //optionsBuilder.UseSqlServer( connectionString );
            return new MyContext( optionsBuilder.Options );
        }
    }
}