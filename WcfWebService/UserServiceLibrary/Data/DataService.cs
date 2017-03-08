using Cassandra;
using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfWebService;

namespace UserServiceLibrary.Data
{
    public class DataService
    {
        public Cluster cluster { get; set; }
        public ISession session { get; set; }

        public DataService()
        {
            cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            session = cluster.Connect("mykeyspace");

            session.UserDefinedTypes.Define(
                   UdtMap.For<FullName>(),
                   UdtMap.For<Address>()
                      .Map(a => a.Street, "street")
                      .Map(a => a.City, "city")
                      .Map(a => a.ZipCode, "zip_code")
                      .Map(a => a.Phones, "phones")
               );

            Cassandra.Mapping.MappingConfiguration.Global.Define(new Map<User>().TableName("users")
                .Column(c => c.Id, cm => cm.WithName("id"))
                .Column(c => c.Addresses, cm => cm.WithName("addresses").WithFrozenKey())
                .Column(c => c.DirectReport, cm => cm.WithName("direct_reports").WithFrozenValue()));
        }

    }
}
