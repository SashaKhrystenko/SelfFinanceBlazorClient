using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_12.Entities;

namespace Task_12.Databases.EntityConfiguretions
{
    public class IncomeCategoryEntityConfiguretion : IEntityTypeConfiguration<IncomeCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<IncomeCategoryEntity> builder)
        {
            builder
                .HasKey(category => category.Id)
            ;

            builder
                .Property(category => category.Name)
                .IsRequired()
            ;
        }
    }
}
