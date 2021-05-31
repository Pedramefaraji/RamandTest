using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RamandTestSolution.Domain.Entities
{
    public class Users
    {
        [Required]
        public Guid ID { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "حداکثر 30"), MinLength(4, ErrorMessage = "حداقل 4")]
        public string Username { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "حداکثر 30"), MinLength(8, ErrorMessage = "حداقل 8")]
        public string Password { get; set; }
    }
}
