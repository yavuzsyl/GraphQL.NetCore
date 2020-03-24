using GraphQL.Common.Request;
using GraphQLClient.DTO;
using GraphQLClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLClient.Consumer
{
    public class OwnerConsumer
    {
        private readonly GraphQL.Client.GraphQLClient _client;
        public OwnerConsumer(GraphQL.Client.GraphQLClient client)
        {
            _client = client;
        }

        /// <summary>
        /// query gönderilecek
        /// getDatafiledas methoduna queryde gönderilen argüman adı yazılmalıdır
        /// dönen model istenilen tipe convert edilir
        /// </summary>
        /// <returns></returns>
        public async Task<List<Owner>> GetAllOwnersAsync()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                          query ownersQuery{
                            owners{
                              id
                              name
                              address
                              accounts{
                                id
                                type
                                description
                              }
                            }
                        }"
            };

            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<List<Owner>>("owners");
        }

        public async Task<Owner> GetOwnerAsync(Guid id)
        {
            var query = new GraphQLRequest
            {
                Query = @" 
                          query ownerQuery($ownerId : ID!){
                            owner(ownerId : $ownerId){
                                id
                                name
                                address
                                accounts{
                                    id
                                    type
                                    description
                                }
                            }
                         }",
                Variables = new { ownerId = id }
            };

            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<Owner>("owner");
        }

        public async Task<Owner> CreateOwner(OwnerInput ownerInput)
        {
            var query = new GraphQLRequest
            {
                Query = @"
                        mutation($owner:ownerInput!){
                            createOwner(owner : $owner){
                                id,
                                name,
                                address
                            }
                        }",
                Variables = new { owner = ownerInput }
            };

            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<Owner>("createOwner");
        }

        public async Task<Owner> UpdateOwner(Guid id, OwnerInput ownerInput)
        {
            var query = new GraphQLRequest
            {
                Query = @" mutation($owner : ownerInput!, $ownerId : ID!){
                            updateOwner(owner : $owner , ownerId : $ownerId){
                                id, 
                                name,
                                address
                            }
                           }",
                Variables = new {owner = ownerInput, ownerId = id}
            };

            var response = await _client.PostAsync(query);
            return response.GetDataFieldAs<Owner>("updateOwner");
        }

        public async Task<string> DeleteOwner(Guid id)
        {
            var query = new GraphQLRequest
            {
                Query = @" mutation($ownerId : ID!){
                             deleteOwner(ownerId : $ownerId)
                          }",
                Variables = new { ownerId = id }
            };

            var response = await _client.PostAsync(query);
            return response.Data.deleteOwner;
        }
    }
}
