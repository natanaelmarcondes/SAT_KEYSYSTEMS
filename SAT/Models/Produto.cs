using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.Models
{
    public class Produto
    {
        [Key]
        public int prd_Codigo { get; set; }
        public string prd_Descri { get; set; } = "";
        public string prd_DesBus { get; set; } = "";
        public string prd_NomImg { get; set; } = "";
        [NotMapped]
        public bool TemAnexo { get; set; }

    }
}
