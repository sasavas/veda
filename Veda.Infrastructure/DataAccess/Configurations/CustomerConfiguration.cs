using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.SharedKernel.Models;

namespace Veda.Infrastructure.DataAccess.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.Property(customer => customer.Id).ValueGeneratedOnAdd();

        builder.Property(customer => customer.TCKimlikNo).HasConversion(
            no => no.Value,
            s => new TCKimlikNo(s));
        
        builder.Property(customer => customer.EmailAddress).HasConversion(
            no => no.Address,
            s => new EmailAddress(s));

        //TODO! hashleme islemini dikkate almalisin ???
        builder.Property(customer => customer.Password).HasConversion(
            password => password.Value,
            s => new Password(s));

        builder.HasMany<MemberShip>(customer => customer.Memberships)
            .WithOne();
    }
}