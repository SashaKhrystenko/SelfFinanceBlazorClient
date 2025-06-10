using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_12.Entities;

namespace Task_12.Databases.EntityConfiguretions
{
    public class IncomeEntityConfiguretion : IEntityTypeConfiguration<IncomeEntity>
    {
        public void Configure(EntityTypeBuilder<IncomeEntity> builder)
        {
            builder
                .HasKey(income => income.Id)
            ;

            builder
                .Property(income => income.Date)
                .IsRequired()
            ;

            builder
                .Property(income => income.Amount)
                .IsRequired()
            ;

            builder
                .HasOne(income => income.IncomeCategory)
                .WithMany(category => category.Incomes)
                .HasForeignKey(income => income.IncomeCategoryId)
                .OnDelete(DeleteBehavior.SetNull)
            ;
        }
    }
}
