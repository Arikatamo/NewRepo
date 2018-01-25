using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCodeFirst.Entities
{
    [Table("tblFilters")]
    public class Filters
    {
        /// <summary>
        /// Ключ назви фільтру для продукту
        /// </summary>
        [Key, ForeignKey("FilterNameOf"), Column(Order=0)]
        public int FilterNameId { get; set; }
        public virtual FilterName FilterNameOf { get; set; }
        /// <summary>
        /// Ключ значень для Обраного фільтру
        /// </summary>
        [Key, ForeignKey("FilterValueOf"), Column(Order = 1)]
        public int FilterValueId { get; set; }
        public virtual FilterValue FilterValueOf { get; set; }
        /// <summary>
        /// Ключ для продукту
        /// </summary>
        [Key, ForeignKey("ProductOf"), Column(Order = 2)]
        public int ProductId { get; set; }
        public virtual Product ProductOf { get; set; }
    }
}
