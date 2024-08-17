using Api.Domain.Dtos.User;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Service.Services
{
    public class UserService : IUserService
    {
        private IRepository<UserEntity> _Repository;
        private readonly IMapper _mapper;

        public UserService( IRepository<UserEntity> repository, IMapper mapper )
        {
            _Repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Delete( Guid id )
        {
            return await _Repository.DeleteAsync( id );
        }

        public async Task<UserDto> Get( Guid id )
        {
            var entity = await _Repository.SelectAsync( id );

            return _mapper.Map<UserDto>( entity ) ?? new UserDto( );
        }

        public async Task<IEnumerable<UserDto>> GetAll( )
        {
            var listEntity = await _Repository.SelectAsync( );

            return _mapper.Map<IEnumerable<UserDto>>( listEntity );
        }

        public async Task<UserDtoCreateResult> Post( UserDtoCreate user )
        {
            var model = _mapper.Map<UserModel>( user );
            var entity = _mapper.Map<UserEntity>( model );
            var result = await _Repository.InsertAsync( entity );

            return _mapper.Map<UserDtoCreateResult>( result );
        }

        public async Task<UserDtoUpdateResult> Put( UserDtoUpdate user )
        {
            var model = _mapper.Map<UserModel>( user );
            var entity = _mapper.Map<UserEntity>( model );
            var result = await _Repository.UpdateAsync( entity );

            return _mapper.Map<UserDtoUpdateResult>( result );
        }
    }
}