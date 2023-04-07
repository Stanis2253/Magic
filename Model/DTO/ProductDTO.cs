using MagicModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicModel.DTO
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDesc { get; set; }
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

    }
}
