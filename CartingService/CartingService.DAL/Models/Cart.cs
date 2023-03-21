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
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BsonId]
        public int Id { get; set; }
        [BsonRef("Items")]
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}