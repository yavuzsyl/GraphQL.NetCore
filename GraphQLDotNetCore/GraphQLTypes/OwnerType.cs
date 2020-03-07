using GraphQL.Types;
using GraphQLDotNetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQLTypes
{
    /// <summary>
    /// 1. GraphQLAPI de owner objesi yerine kullanacağımız class.
    /// 2. bu typeın inherit aldığı class IObjectGraphType'ı implement ettiği için bu type çözümlenebilecek
    /// </summary>
    public class OwnerType : ObjectGraphType<Owner>
    {
        public OwnerType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object");
            Field(x => x.Name).Description("Name property from the owner object");
            Field(x => x.Address).Description("Address property from the owner object");
        }
    }
}
