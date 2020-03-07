using GraphQL.Types;
using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.GraphQLTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        /// <summary>
        /// ListGraphType list Field 1.parametre client tarafından gelecek olan query-ifade bu yüzden client tarafından istek atarken buradaki ifade ile birebir aynı olmalı , 2.parametre ise bu isteğe bağlı dönecek result 
        /// </summary>
        /// <param name="ownerRepository"></param>
        public AppQuery(IOwnerRepository ownerRepository)
        {
            Field<ListGraphType<OwnerType>>("owners", resolve: context => ownerRepository.GetAll());
        }
    }
}
