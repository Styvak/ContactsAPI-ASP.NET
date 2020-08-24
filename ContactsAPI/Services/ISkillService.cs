using ContactsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsAPI.Services
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> GetSkillsAsync();
        Task<Skill> GetSkillByIdAsync(Guid skillId);
        Task<bool> CreateSkillAsync(Skill skill);
        Task<bool> DeleteSkillByIdAsync(Guid skillId);
    }
}
