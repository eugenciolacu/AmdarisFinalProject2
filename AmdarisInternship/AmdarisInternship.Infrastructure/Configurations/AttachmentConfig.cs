using AmdarisInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    public class AttachmentConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.Property(a => a.AttachmentExtension)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.AttachmentName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(a => a.Attachment_)
                .IsRequired();
        }
    }
}
