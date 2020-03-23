using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQLTypes
{
    public class OwnerInputType : InputObjectGraphType
    {
        public OwnerInputType()
        {
            //mutations için owner input type oluşturuldu ve modelin fieldları oluşturuldu
            Name = "ownerInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("address");
        }
    }
}
