using AmdarisInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    class PromotionModuleConfig : IEntityTypeConfiguration<PromotionModule>
    {
        public void Configure(EntityTypeBuilder<PromotionModule> builder)
        {
            builder
                .HasIndex(pm => new { pm.ModuleId, pm.PromotionId })
                .IsUnique();
        }
    }
}
