using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.Models
{
    public class Produto
    {
        [Key]
        public string prd_Codigo { get; set; } = "";
        public string prd_Descri { get; set; } = "";
        public string prd_DesBus { get; set; } = "";
        public decimal prd_CodBar { get; set; }
        public string emp_Codigo { get; set; }
        public string mrc_Codigo { get; set; }
        public string prd_CodTab { get; set; } = "";
        [NotMapped]
        public bool TemAnexo { get; set; }

    }
}
