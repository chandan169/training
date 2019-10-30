using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Fullname cannot be Empty")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Username cannot be Empty")]
        [MinLength(6,ErrorMessage ="Minimum 6 character is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Username cannot be Empty")]
        [MinLength(8, ErrorMessage = "Minimum 8 character is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Email cannot be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Role { get; set; }
    }
}
