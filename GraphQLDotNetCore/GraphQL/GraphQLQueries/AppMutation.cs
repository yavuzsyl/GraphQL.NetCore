using GraphQL;
using GraphQL.Types;
using GraphQLDotNetCore.Contracts;
using GraphQLDotNetCore.Entities;
using GraphQLDotNetCore.GraphQLTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQL.GraphQLQueries
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(IOwnerRepository repository)
        {
            //createOwner glecek ve argümanu owner olacak bu owner ownerInputType tipinde olacak
            Field<OwnerType>(
                "createOwner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" }),
                resolve: context =>
               {//contextin içindeki owner nesnesi Owner tipine dönüştürülecek ve entity tipinde return edilecek
                   var owner = context.GetArgument<Owner>("owner");
                   return repository.CreateOwner(owner);
               });

            //update owner mutation gelecek 2 tane argümanı olacak biri owner diğeri ownerId
            //bunlar Owner tipine ve Guid tipine resolve edilecek
            //eğere db de böyle bir kullanıcı varsa update eder
            //yoksa hata dönecek
            Field<OwnerType>(
                "updateOwner",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),

                resolve: context =>
               {
                   var owner = context.GetArgument<Owner>("owner");
                   var ownerId = context.GetArgument<Guid>("ownerId");

                   var dbOwner = repository.GetById(ownerId);
                   if (dbOwner != null)
                       return repository.UpdateOwner(dbOwner, owner);

                   context.Errors.Add(new ExecutionError("Couldnt find the owner"));
                   return null;
               }
                );
        }
    }
}
