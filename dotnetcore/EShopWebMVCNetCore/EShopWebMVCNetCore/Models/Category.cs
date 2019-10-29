using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace EShopWebMVCNetCore.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Category name cannot be empty")]
        [MinLength(3, ErrorMessage ="minimum 3 character required")]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
    }
}
