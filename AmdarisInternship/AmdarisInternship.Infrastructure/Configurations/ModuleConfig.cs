using AmdarisInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    public class ModuleConfig : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasIndex(m => m.Name)
                .IsUnique();

            builder.HasCheckConstraint("CK_Module_Name", "Name != ''");
        }
    }
}
