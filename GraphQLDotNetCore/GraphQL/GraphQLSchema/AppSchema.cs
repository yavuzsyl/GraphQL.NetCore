using GraphQL;
using GraphQL.Types;
using GraphQLDotNetCore.GraphQL.GraphQLQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLDotNetCore.GraphQL.GraphQLSchema
{
    public class AppSchema : Schema
    {
        //objeleri çözümlemek için resolver inject edilir bu resolver IObjectGraphType'ı implemente eden objeleri çözümleyecek
        public AppSchema(IDependencyResolver resolver) : base(resolver)
        {
            //gelen query resolver ile çözümlenir 
            Query = resolver.Resolve<AppQuery>();
        }
    }
}
