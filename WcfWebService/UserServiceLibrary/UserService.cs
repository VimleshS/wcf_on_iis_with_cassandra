using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;
using Cassandra;
using Cassandra.Mapping;
using Cassandra.Data.Linq;

namespace WcfWebService
{

    public class UserService : IUserService
    {

        /// <summary>
        /// POSTMAN REQUEST WHEN HOSTED IN A CONSOLE APP.
        /// http://localhost:8080/userservice/user/62c36092-82a1-3a00-93d1-46196ee77204
        /// </summary>
        /// 
        /// FROM IIS
        /// http://localhost/user/UserService.svc/user/62c36092-82a1-3a00-93d1-46196ee77204



        public User GetUser(string guid)
        {
            var cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("mykeyspace");

            session.UserDefinedTypes.Define(
                   UdtMap.For<FullName>(),
                   UdtMap.For<Address>()
                      .Map(a => a.Street, "street")
                      .Map(a => a.City, "city")
                      .Map(a => a.ZipCode, "zip_code")
                      .Map(a => a.Phones, "phones")
               );

            var users = new Table<User>(session,
                Cassandra.Mapping.MappingConfiguration.Global.Define(new Map<User>().TableName("users")
                    .Column(c => c.Id, cm => cm.WithName("id"))
                    .Column(c => c.Addresses, cm => cm.WithName("addresses")))
                );

            var parameters = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters;
            if (parameters["format"] == "Xml")
            {
                WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Xml;
            }

            var user = users.
                Where(u => u.Id == Guid.Parse(guid))
                .FirstOrDefault()
                .Execute();

            return user;

        }

        //public IEnumerable<User> GetUsers()
        //{

        //}
    }
}