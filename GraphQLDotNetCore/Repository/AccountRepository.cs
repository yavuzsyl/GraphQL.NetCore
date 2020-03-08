using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;

        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ILookup<Guid, Account>> GetAccountsByOwnerIds(IEnumerable<Guid> ownerIds)
        {
            //LookUp ile accountlar ownerId ye göre gruplanacak 
            var accounts = await _context.Accounts.Where(ac => ownerIds.Contains(ac.OwnerId)).ToListAsync();
            return accounts.ToLookup(ac => ac.OwnerId);
        }

        public IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId)
            => _context.Accounts.Where(ac => ac.OwnerId.Equals(ownerId)).ToList();
    }
}
