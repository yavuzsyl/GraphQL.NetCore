using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GraphQLDotNetCore.Entities.Context
{
    public class OwnerContextConfiguration : IEntityTypeConfiguration<Owner>
    {
        private Guid[] _ids;

        public OwnerContextConfiguration(Guid[] ids)
        {
            _ids = ids;
        }

        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder
              .HasData(
                new Owner
                {
                    Id = _ids[0],
                    Name = "Müntekim Gıcırbey",
                    Address = "Üsküdar"
                },
                new Owner
                {
                    Id = _ids[1],
                    Name = "Abidin Dandini",
                    Address = "Beykoz"
                }
            );
        }
    }
}
