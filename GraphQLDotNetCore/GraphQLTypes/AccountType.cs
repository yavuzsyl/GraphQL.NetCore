using GraphQL.Types;
using GraphQLDotNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQLTypes
{
    public class AccountType : ObjectGraphType<Account>
    {
        public AccountType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id prop from to nibba Account object lets roll");
            Field(x => x.Description).Description("Description prop from the account object");
            Field(x => x.OwnerId, type: typeof(IdGraphType)).Description("Account objesinin ownerın ait id");
        }
    }
}
