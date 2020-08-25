using ContactsAPI.Filters;
using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContactsAsync();
        Task<Contact> GetContactByIdAsync(Guid contactId);
        Task<bool> CreateContactAsync(Contact contact);
        Task<bool> DeleteContactByIdAsync(Guid contactId);

        Task<bool> UpdateContact(Contact contactToUpdate);
        Task<bool> UserOwnsContactAsync(Guid contactId, string userId);
        Task<IEnumerable<Contact>> GetContactsByUserAsync(string userId, PaginationFilter paginationFilter = null);
    }
}
