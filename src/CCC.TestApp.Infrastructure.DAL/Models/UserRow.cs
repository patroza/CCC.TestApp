using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC.TestApp.Infrastructure.DAL.Models
{
    [Table("Users")]
    class UserRow
    {
        [Column, Required]
        public Guid Id { get; set; }

        [Column]
        public string Password { get; set; }

        [Column, Required]
        public string UserName { get; set; }
    }
}