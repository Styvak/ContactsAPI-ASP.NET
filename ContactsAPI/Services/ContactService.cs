﻿using ContactsAPI.Data;
using ContactsAPI.Filters;
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
            return await _dataContext.Contacts.Include(c => c.Skills).SingleOrDefaultAsync(c => c.ContactID == contactId);
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync()
        {
            return await _dataContext.Contacts.Include(c => c.Skills).ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByUserAsync(string userId, PaginationFilter paginationFilter = null)
        {
            if (paginationFilter == null)
            {
                return await _dataContext.Contacts.Include(c => c.Skills).Where(c => c.UserID == userId).ToListAsync();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await _dataContext.Contacts.Include(c => c.Skills).Where(c => c.UserID == userId).Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<bool> DeleteContactByIdAsync(Guid contactId)
        {
            var contact = await GetContactByIdAsync(contactId);

            if (contact == null) return false;
            _dataContext.Contacts.Remove(contact);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> UpdateContact(Contact contactToUpdate)
        {
            _dataContext.Contacts.Update(contactToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UserOwnsContactAsync(Guid contactId, string userId)
        {
            var contact = await _dataContext.Contacts.AsNoTracking().SingleOrDefaultAsync(x => x.ContactID == contactId);
            if (contact == null) return false;
            if (contact.UserID != userId) return false;
            return true;
        }
    }
}
