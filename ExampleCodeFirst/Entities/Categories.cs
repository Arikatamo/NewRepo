using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCodeFirst.Entities
{
   [Table("tblCategories")]
   public class Categories
    {
        [Key,Column(Order = 0)]
        public int Id { get; set; }
        [Required,StringLength(maximumLength:250)]
        public string Name { get; set; }
        [ForeignKey("Parent"), Column(Order = 1)]
        public int? ParentID { get; set; }
        public bool IsHead { get; set; }
        public ICollection<Categories> Parent { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
