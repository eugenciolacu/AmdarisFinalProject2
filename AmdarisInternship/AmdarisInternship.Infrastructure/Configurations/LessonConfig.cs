using AmdarisInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasCheckConstraint("CK_Lesson_Name", "Name != ''")
                .HasCheckConstraint("CK_Lesson_Time", "StartTime < EndTime");

            builder
                .HasOne(l => l.User)
                .WithMany(u => u.Lessons)
                .HasForeignKey(l => l.LecturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
