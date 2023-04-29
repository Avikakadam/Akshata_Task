using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akshata_Task.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}