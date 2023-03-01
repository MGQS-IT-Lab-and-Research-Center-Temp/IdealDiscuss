using System;
using IdealDiscuss.Entities;
using IdealDiscuss.Repository.Interfaces;
using IdealDiscuss.Context;
using Microsoft.EntityFrameworkCore;
using IdealDiscuss.Dtos.CategoryDto;
using IdealDiscuss.Dtos.RoleDto;

namespace IdealDiscuss.Repository.Implementations
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IdealDiscussContext context)
        {
            _context = context;
        }
    }
}