﻿using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Services
{
    public class SkillService : ISkillService
    {
        private readonly ApplicationDbContext _dataContext;

        public SkillService(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateSkillAsync(Skill skill)
        {
            await _dataContext.Skills.AddAsync(skill);
            //TODO -> Add skill to contact
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> DeleteSkillByIdAsync(Guid skillId)
        {
            var skill = await GetSkillByIdAsync(skillId);

            if (skill == null) return false;
            _dataContext.Skills.Remove(skill);
            //TODO -> Remove skill in contact
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<Skill> GetSkillByIdAsync(Guid skillId)
        {
            return await _dataContext.Skills.FindAsync(skillId);
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await _dataContext.Skills.ToListAsync();
        }

        
    }
}
