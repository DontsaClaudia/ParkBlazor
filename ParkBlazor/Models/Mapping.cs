using System.ComponentModel.DataAnnotations;

namespace ParkBlazor.Models
{
    public class Mapping
    {
        [Key]
        public int Id { get; set; }
        public int Position { get; set; } = 0;
    }
}
