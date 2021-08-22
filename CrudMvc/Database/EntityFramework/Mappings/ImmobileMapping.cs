using CrudMvc.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrudMvc.Database.EntityFramework.Mappings
{
    public class ImmobileMapping : IEntityTypeConfiguration<Immobile>
    {
        public void Configure(EntityTypeBuilder<Immobile> builder)
        {
            builder.ToTable("Immobiles");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TypeBusiness).IsRequired();
            builder.Property(p => p.Description).HasColumnType("VARCHAR(400)").IsRequired();
            builder.Property(p => p.Value).HasColumnType("FLOAT").IsRequired();

            builder.HasOne(c => c.Client)
                .WithMany(i => i.Immobiles)
                .HasForeignKey(c => c.ClientId);
        }
    }
}
