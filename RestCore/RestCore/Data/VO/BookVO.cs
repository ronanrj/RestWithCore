using System;
using System.Runtime.Serialization;

namespace RestCore.Data.VO
{
    [DataContract]
    public class BookVO
    {
        [DataMember(Order = 1,Name ="codigo")]
        public int? Id { get; set; }
        [DataMember(Order = 2)]
        public string Author { get; set; }
        [DataMember(Order = 3)]
        public DateTime LaunchDate { get; set; }
        [DataMember(Order = 5)]
        public decimal Price { get; set; }
        [DataMember(Order = 4)]
        public string Title { get; set; }       
        
        
    }
}
