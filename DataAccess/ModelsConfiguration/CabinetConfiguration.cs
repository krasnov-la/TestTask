using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTasks.Domain;

namespace TestTasks.DataAccess.ModelsConfiguration;

class CabinetConfiguration : IEntityTypeConfiguration<Cabinet>
{
    public void Configure(EntityTypeBuilder<Cabinet> builder)
    {
        builder.ToTable("cabinets");
        builder.HasKey(c => c.Id);
    }
}