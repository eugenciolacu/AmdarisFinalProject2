using AmdarisInternship.Domain;
using AmdarisInternship.Domain.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasCheckConstraint("CK_User_FirstName", "FirstName != ''")
                .HasCheckConstraint("CK_User_LastName", "LastName != ''");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .HasCheckConstraint("CK_User_UserEmail", "Email != '' and Email LIKE '%_@_%._%'");

            builder
                .HasIndex(us => us.Skype)
                .IsUnique();

            builder.HasCheckConstraint("CK_User_Skype", "DATALENGTH(Skype) >= 6 and Skype != ''");
        }
    }
}
