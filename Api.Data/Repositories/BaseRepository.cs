using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private DbSet<T> _DataSet;
        protected readonly MyContext _Context;

        public BaseRepository( MyContext context )
        {
            _Context = context;
            _DataSet = context.Set<T>( );
        }

        public async Task<bool> ExistAsync( Guid id )
        {
            return await _DataSet.AnyAsync( x => x.Id == id );
        }

        public async Task<bool> DeleteAsync( Guid id )
        {
            try
            {
                var result = await _DataSet.SingleOrDefaultAsync( x => x.Id.Equals( id ) ) ?? throw new Exception( "Usuário não existem no sistema" );

                _DataSet.Remove( result );
                await _Context.SaveChangesAsync( );
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }

            return true;
        }

        public async Task<T> InsertAsync( T item )
        {
            try
            {
                if ( item.Id == Guid.Empty )
                {
                    item.Id = Guid.NewGuid( );
                }

                item.CreateAt = DateTime.UtcNow;
                _DataSet.Add( item );

                await _Context.SaveChangesAsync( );
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }

            return item;
        }

        public async Task<T> SelectAsync( Guid id )
        {
            try
            {
                return await _DataSet.SingleOrDefaultAsync( x => x.Id == id );
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }
        }

        public async Task<IEnumerable<T>> SelectAsync( )
        {
            try
            {
                return await _DataSet.ToListAsync( );
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }
        }

        public async Task<T> UpdateAsync( T item )
        {
            try
            {
                var result = await _DataSet.SingleOrDefaultAsync( x => x.Id.Equals( item.Id ) ) ?? throw new Exception( "Usuário não existem no sistema" );
                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = result.CreateAt;

                _Context.Entry( result ).CurrentValues.SetValues( item );

                await _Context.SaveChangesAsync( );
            }
            catch ( Exception ex )
            {
                throw new Exception( ex.Message );
            }

            return item;
        }
    }
}