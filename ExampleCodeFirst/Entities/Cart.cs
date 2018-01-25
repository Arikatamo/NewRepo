using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCodeFirst.Entities
{
    [Table("tblCart")]
    public class Cart
    {
        [Key,ForeignKey("UserProfileOf")]
        public int UserProfileId { get; set; }
        public DateTime DateCreate { get; set; }
        public virtual UserProfile UserProfileOf { get; set; }
   
    }
}
