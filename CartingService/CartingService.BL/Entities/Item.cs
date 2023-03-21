using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.BLL.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
