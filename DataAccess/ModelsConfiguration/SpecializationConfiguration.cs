using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTasks.Domain;

namespace TestTasks.DataAccess.ModelsConfiguration;

class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable("specializations");
        builder.HasKey(c => c.Id);
    }
}