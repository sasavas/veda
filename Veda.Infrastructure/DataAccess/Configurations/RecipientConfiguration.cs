using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Modules.RecipientModule.Models;
using Veda.Application.SharedKernel.Models;

namespace Veda.Infrastructure.DataAccess.Configurations;

public class RecipientConfiguration : IEntityTypeConfiguration<Recipient>
{
    public void Configure(EntityTypeBuilder<Recipient> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        builder.Property(r => r.TCKimlikNo)
            .HasConversion(
                tc => tc.Value,
                value => new TCKimlikNo(value));

        builder.Property(r => r.EMailAddress)
            .HasConversion(
                email => email.Address,
                value => new EmailAddress(value));

        builder.OwnsOne<PhoneNumber>(
            recipient => recipient.PhoneNumber,
            phoneBuilder =>
            {
                phoneBuilder.ToTable("recipient_phone_numbers");
                phoneBuilder.Property(phone => phone.CountryCode);
                phoneBuilder.Property(phone => phone.Number);
            });

        builder.HasOne(recipient => recipient.Folder)
            .WithOne()
            .HasForeignKey<Folder>(folder => folder.RecipientId);

        builder.HasOne<Customer>()
            .WithMany(customer => customer.Recipients)
            .HasForeignKey(recipient => recipient.CustomerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}