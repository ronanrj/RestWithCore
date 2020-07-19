using System.Runtime.Serialization;

namespace RestCore.Model
{
    //contrato entre atributos e a estrutura da tabela 
    //[DataContract]
    public class User
    {        
        public int? Id { get; set; }
        public string login { get; set; }
        public string accessKey { get; set; }
    }
}
