using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dreamy.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public User User { get; set; }
    }
}
