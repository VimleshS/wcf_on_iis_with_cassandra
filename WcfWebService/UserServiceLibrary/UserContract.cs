using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfWebService
{

    [ServiceContract]
    public interface IUserService
    {

        //[OperationContract]
        //IEnumerable<User> GetUsers();

        [OperationContract]
        [WebGet(UriTemplate = "user/{guid}", ResponseFormat = WebMessageFormat.Json)]
        User GetUser(string guid);

    }
}