using Microsoft.EntityFrameworkCore;
using Veda.Application.Modules.CustomerModule.Models;
using Veda.Application.Ports.DataAccess;

namespace Veda.Infrastructure.DataAccess.RepositoryAdapters;

public class CustomerRepository(VedaDbContext context) : Repository<Customer>(context), ICustomerRepository
{
    public Customer? GetByIdIncludingRecipients(int id)
    {
        return Context.Set<Customer>()
            .Include(customer => customer.Memberships)
            .ThenInclude(membership => membership.MembershipStatus)
            .Include(customer => customer.Recipients)
            .FirstOrDefault(customer => customer.Id == id);
    }

    public Customer? GetByIdIncludingRecipientsAndContens(int id)
    {
        return Context.Set<Customer>()
            .Include(customer => customer.Memberships)
            .ThenInclude(membership => membership.MembershipStatus)
            .Include(customer => customer.Recipients)
            .ThenInclude(recipient => recipient.Folder)
            .ThenInclude(folder => folder.DigitalContents.Where(content => content.DeletionDate == null))
            .FirstOrDefault(customer => customer.Id == id);
    }

    public IEnumerable<Customer> GetAllIncludingAll()
    {
        return Context.Set<Customer>()
            .Include(customer => customer.Memberships)
            .ThenInclude(membership => membership.MembershipStatus)
            .Include(customer => customer.Recipients)
            .ThenInclude(recipient => recipient.Folder)
            .ThenInclude(folder => folder.DigitalContents.Where(content => content.DeletionDate == null))
            .AsEnumerable();
    }
}