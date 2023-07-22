using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    [Table("BillDetails")]
    public class BillDetail
    {
        public int Id { get; set; }
        public int BillId { get; set; }
        public int VariantProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double Tax { get; set; }
        public string Note { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
