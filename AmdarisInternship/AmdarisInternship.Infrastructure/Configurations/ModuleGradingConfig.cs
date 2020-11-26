using AmdarisInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    class ModuleGradingConfig : IEntityTypeConfiguration<ModuleGrading>
    {
        public void Configure(EntityTypeBuilder<ModuleGrading> builder)
        {
            builder.Property(mg => mg.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
