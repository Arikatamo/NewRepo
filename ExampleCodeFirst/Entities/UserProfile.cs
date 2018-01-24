using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCodeFirst.Entities
{
    [Table("tblUserProfiles")]
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:250)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 128)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Telephone { get; set; }
        public virtual Cart CartOf { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
