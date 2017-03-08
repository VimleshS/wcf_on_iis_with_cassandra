using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using Cassandra.Data.Linq;

namespace WcfWebService
{

    public class UserService : IUserService
    {
        static UserServiceLibrary.Data.DataService dataService;
        static UserService()
        {
            dataService = new UserServiceLibrary.Data.DataService();
        }
        
        /// <summary>
        /// POSTMAN REQUEST WHEN HOSTED IN A CONSOLE APP.
        /// http://localhost:8080/userservice/user/62c36092-82a1-3a00-93d1-46196ee77204
        /// </summary>
        /// 
        /// FROM IIS
        /// http://localhost/user/UserService.svc/user/62c36092-82a1-3a00-93d1-46196ee77204
        public User GetUser(string guid)
        {
            SetOutGoingMessageFormat(WebOperationContext.Current);

            var users = new Table<User>(dataService.session);
            var user = users.
                Where(u => u.Id == Guid.Parse(guid))
                .FirstOrDefault()
                .Execute();

            return user;
        }

        /// <summary>
        /// POSTMAN REQUEST WHEN HOSTED IN A CONSOLE APP.
        /// http://localhost:8080/userservice/users?format=Xml
        /// </summary>
        /// 
        /// FROM IIS
        /// http://localhost/user/UserService.svc/users?format=Xml
        public IEnumerable<User> GetUsers()
        {
            SetOutGoingMessageFormat(WebOperationContext.Current);
            var users = new Table<User>(dataService.session);
            return users.Execute();
        }
        
        public void Create(User user)
        {
            SetOutGoingMessageFormat(WebOperationContext.Current);
            var users = new Table<User>(dataService.session);
            users.Insert(user).Execute();
        }

        /* POST request
         * http://localhost:8080/userservice/users
         * Request body
           {
            "Addresses": [
              {
                "Key": "home",
                "Value": {
                  "City": "Paris",
                  "Phones": [
                    "33 6 78 90 12 34"
                  ],
                  "Street": "191 Rue St. Charles",
                  "ZipCode": 75015
                }
              }
            ],
            "Id": "62c36092-82a1-3a00-93d1-46196ee77205",
            "Name": {
              "FirstName": "Marie-Claude",
              "LastName": "Josset"
            }
          }
          * for IIS
          * http://localhost/user/UserService.svc/users
          */

        public void SetOutGoingMessageFormat(WebOperationContext currentContext)
        {
            if (currentContext.IncomingRequest.UriTemplateMatch.QueryParameters["format"] == "Xml")
            {
                currentContext.OutgoingResponse.Format = WebMessageFormat.Xml;
            }
        }
    }
}