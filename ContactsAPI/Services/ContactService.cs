using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Services
{
    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _dataContext;

        public ContactService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateContactAsync(Contact contact)
        {
            await _dataContext.Contacts.AddAsync(contact);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Contact> GetContactByIdAsync(Guid contactId)
        {
            return await _dataContext.Contacts.FindAsync(contactId);
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _dataContext.Contacts.ToListAsync();
        }
    }
}
