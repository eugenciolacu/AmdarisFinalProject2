using AmdarisInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmdarisInternship.Infrastructure.Configurations
{
    public class ExamGradeComponentConfig : IEntityTypeConfiguration<ExamGradeComponent>
    {
        public void Configure(EntityTypeBuilder<ExamGradeComponent> builder)
        {
            builder
                .HasOne(egc => egc.ModuleGrading)
                .WithMany(mg => mg.ExamGradeComponents)
                .HasForeignKey(egc => egc.ModuleGradingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
