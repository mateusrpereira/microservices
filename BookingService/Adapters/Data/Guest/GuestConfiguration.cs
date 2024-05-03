using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entities = Domain.Entities;

namespace Data.Guest
{
    public class GuestConfiguration : IEntityTypeConfiguration<Domain.Entities.Guest>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Guest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.IdNumber);

            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.DocumentType);
        }
    }
}
