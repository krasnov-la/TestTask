using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTasks.Domain;

namespace TestTasks.DataAccess.ModelsConfiguration;

class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors");
        builder.HasKey(d => d.Id);
        builder
            .Property(d => d.FullName)
            .HasMaxLength(128);
        builder
            .HasOne(d => d.Cabinet)
            .WithMany()
            .HasForeignKey(d => d.CabinetId);
        builder
            .HasOne(d => d.Specialization)
            .WithMany()
            .HasForeignKey(d => d.SpecializationId);
        builder
            .HasOne(d => d.Region)
            .WithMany()
            .HasForeignKey(d => d.RegionId)
            .IsRequired(false);
    }
}