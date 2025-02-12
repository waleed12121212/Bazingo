using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bazingo_Core.Entities.Identity;
using Bazingo_Core.Interfaces;
using Bazingo_Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bazingo_Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppIdentityDbContext _context;

        public UserRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetByIdAsync(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser> GetByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetAllAsync()
        {
            return await _context.Users
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email, string excludeUserId = null)
        {
            var query = _context.Users.Where(u => u.Email == email);
            if (!string.IsNullOrEmpty(excludeUserId))
            {
                query = query.Where(u => u.Id != excludeUserId);
            }
            return !await query.AnyAsync();
        }

        public async Task<bool> IsUsernameUniqueAsync(string username, string excludeUserId = null)
        {
            var query = _context.Users.Where(u => u.UserName == username);
            if (!string.IsNullOrEmpty(excludeUserId))
            {
                query = query.Where(u => u.Id != excludeUserId);
            }
            return !await query.AnyAsync();
        }

        public async Task<IReadOnlyList<ApplicationUser>> GetUsersByRoleAsync(string role)
        {
            var usersInRole = await _context.UserRoles
                .Where(ur => ur.RoleId == role)
                .Select(ur => ur.UserId)
                .ToListAsync();

            return await _context.Users
                .Where(u => usersInRole.Contains(u.Id))
                .OrderBy(u => u.UserName)
                .ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
