using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTasks.Domain;

namespace TestTasks.DataAccess.ModelsConfiguration;

class PatienConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.ToTable("patients");
        builder.HasKey(p => p.Id);
        builder
            .Property(p => p.FirstName)
            .HasMaxLength(128);
        builder
            .Property(p => p.LastName)
            .HasMaxLength(128);
        builder
            .Property(p => p.MiddleName)
            .HasMaxLength(128);
        builder
            .Property(p => p.Address)
            .HasMaxLength(128);
        builder
            .Property(p => p.BirthDate);
        builder
            .Property(p => p.Sex)
            .HasConversion<string>();
        builder
            .HasOne(p => p.Region)
            .WithMany()
            .HasForeignKey(p => p.RegionId)
            .IsRequired();
    }
}