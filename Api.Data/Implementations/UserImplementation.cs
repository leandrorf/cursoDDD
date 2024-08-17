using Api.Data.Context;
using Api.Data.Repositories;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;

        public UserImplementation( MyContext context ) : base( context )
        {
            _dataset = context.Set<UserEntity>( );
        }

        public async Task<UserEntity> FindByLogin( string email )
        {
            return await _dataset.FirstOrDefaultAsync( x => x.Email.Equals( email ) );
        }
    }
}