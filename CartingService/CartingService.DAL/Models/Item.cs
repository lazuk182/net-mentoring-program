using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.DAL.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        //[BsonRef("Carts")]
        public Cart Cart { get; set; }
    }
}
