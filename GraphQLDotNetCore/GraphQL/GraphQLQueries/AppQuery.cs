using GraphQL;
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
            //owner querysi arguman gönderilecek bu arguman ownerId olacak ve queryArgument null olmayacak IdGraphType tipinde olacak  gelen arguman Id guid'e parse edilebilirse owner dönecek , edilmezse  hata mesajı ve null dönecek
            Field<OwnerType>(
                "owner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
                resolve: context =>
                 {
                     Guid id;
                     if (!Guid.TryParse(context.GetArgument<string>("ownerId"), out id))
                     {
                         context.Errors.Add(new ExecutionError("Wrong value for guid id"));
                         return null;
                     }
                     return ownerRepository.GetById(id);
                 }

                );
        }
    }
}
