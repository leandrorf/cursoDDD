﻿using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure( EntityTypeBuilder<UserEntity> builder )
        {
            builder.ToTable( "Users" );

            builder.HasKey( x => x.Id );

            builder.HasIndex( x => x.Email ).IsUnique( );

            builder.Property( x => x.Name ).IsRequired( ).HasMaxLength( 60 );

            builder.Property( x => x.Email ).HasMaxLength( 100 );
        }
    }
}