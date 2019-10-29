using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EShopWebMVCNetCore.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Product name cannot be empty")]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage ="Price cannot be empty")]
        [Range(1,Int32.MaxValue, ErrorMessage ="Invalid price value")]
        public double Price { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Quantity cannot be empty")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Invalid quantity value")]
        public int Quantity { get; set; }

        //[Required(ErrorMessage ="Brand name cannot be empty")]
        //public string Brand { get; set; }

        //[Required(ErrorMessage ="Manufacturing date cannot be empty")]
        //public DateTime ManufacturingDate { get; set; }

        [Display(Name ="Category")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
