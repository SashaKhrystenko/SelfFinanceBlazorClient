using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_12.Entities;

namespace Task_12.Databases.EntityConfiguretions
{
    public class ExpenseCategoryEntityConfiguretion : IEntityTypeConfiguration<ExpenseCategoryEntity>
    {
        public void Configure(EntityTypeBuilder<ExpenseCategoryEntity> builder)
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
