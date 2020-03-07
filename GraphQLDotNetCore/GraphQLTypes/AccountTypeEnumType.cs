using GraphQL.Types;
using GraphQLDotNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQLTypes
{
    //Account.cs  type propu enum graphQL de enumlar için ayrı birer class tanımlanarak EnumarationGraphType sınıfından inherit edilir buradaki Name main class içindeki enum property adı ile aynı olmalıdır.
    public class AccountTypeEnumType : EnumerationGraphType<TypeOfAccount>
    {
        public AccountTypeEnumType()
        {
            Name = "Type";
            Description = "Enum for the account type object";
        }
    }
}
