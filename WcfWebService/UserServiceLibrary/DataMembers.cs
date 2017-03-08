using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfWebService
{
    [DataContract(Namespace = "http://VimleshS/")]
    public class User
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public IDictionary<string, Address> Addresses { get; set; }
        [DataMember]
        public IEnumerable<FullName> DirectReport { get; set; }
        [DataMember]
        public FullName Name { get; set; }
    }

    public class Address
    {
        [DataMember]
        public string Street { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int ZipCode { get; set; }
        [DataMember]
        public IEnumerable<string> Phones { get; set; }
    }

    public class FullName
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
    }

}