﻿using GraphQL.DataLoader;
using GraphQL.Types;
using GraphQLDotNetCore.Contracts;
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
        public OwnerType(IAccountRepository accountRepository, IDataLoaderContextAccessor dataLoader)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object");
            Field(x => x.Name).Description("Name property from the owner object");
            Field(x => x.Address).Description("Address property from the owner object");
            //V1 ownerların acccountlarını getirmek için "accounts" queryden gelecek burada gelen istek "owners" querysi olmalı bu query'de istenilen proplara bağlı olarak buradaki proplar dönecek . Dönecek result ObjectGraphType<Owner> tipte olacak. accountlar için istenen ownerId ler context nesnesinin Source (Owner) propertysinin Idleri alınacak. Çünkü burada resolve edilen owner sınıfı ve her bir owner resolve edilirken idleri context.source.Id den alınıp accountlar dönecek
            //V2 dataLoader ile accountlar db den çekilirken her ayrı owner için ayrı sorgu atmak yerine toplu olarak ownerlara ait accountlar çekilir
            Field<ListGraphType<AccountType>>("accounts", resolve: context =>
            {
                var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, Account>("GetAccountsByOwnerIds", accountRepository.GetAccountsByOwnerIds);
                return loader.LoadAsync(context.Source.Id);
            });
        }
    }
}
