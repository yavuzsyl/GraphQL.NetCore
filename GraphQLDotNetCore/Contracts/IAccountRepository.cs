using GraphQLDotNetCore.Entities;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GraphQLDotNetCore.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId);
    }
}
