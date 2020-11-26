using AmdarisInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    public class UserPromotionConfig : IEntityTypeConfiguration<UserPromotion>
    {
        public void Configure(EntityTypeBuilder<UserPromotion> builder)
        {
            builder.
                HasIndex(up => new { up.UserId, up.PromotionId })
                .IsUnique();
        }
    }
}
