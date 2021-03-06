﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCodeFirst.Entities
{
    [Table("tblProducts")]
   public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(maximumLength:250)]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        public float Price { get; set; }
        public DateTime DateCreate { get; set; }
        public ICollection<Filters> Filters { get; set; }
        [ForeignKey("CategoriesID")]
        public int CategoryId { get; set; }
        public virtual Categories CategoriesID { get; set; }

    }
}
