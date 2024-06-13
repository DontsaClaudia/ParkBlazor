using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkBlazor.Models
{
    public class Users : IdentityUser<int>
    {
        [Key]
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        [ForeignKey("Rules")]
        public int? Rulesid { get; set; }
        public Rules? Rules { get; set; }
        public bool IsConnected { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}