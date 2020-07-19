using System.Runtime.Serialization;

namespace RestCore.Model.Base
{
    //contrato entre atributos e a estrutura da tabela 
    //[DataContract]
    public class BaseEntity
    {        
        public int? Id { get; set; }
    }
}
