using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task_12.Entities;

namespace Task_12.Databases.EntityConfiguretions
{
    public class ExpenseEntityConfiguretion : IEntityTypeConfiguration<ExpenseEntity>
    {
        public void Configure(EntityTypeBuilder<ExpenseEntity> builder)
        {
            builder
                .HasKey(expense => expense.Id)
            ;

            builder
                .Property(expense => expense.Date)
                .IsRequired()
            ;

            builder
                .Property(expanse => expanse.Amount)
                .IsRequired()
            ;

            builder
                .HasOne(expense => expense.ExpenseCategory)
                .WithMany(category => category.Expenses)
                .HasForeignKey(expense => expense.ExpenseCategoryId)
                .OnDelete(DeleteBehavior.SetNull)
            ;
        }
    }
}
