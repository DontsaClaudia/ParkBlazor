using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkBlazor.Models
{
    public class Computers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public bool Connected { get; set; } = true;
        public bool Disconnected { get; set; } = false;
        [ForeignKey("Mapping")]
        public int MappingId { get; set; }
        /*public Mapping? position { get; set; }
        [ForeignKey("Rooms")]
        public int RoomsId { get; set; }
        public Rooms? Room { get; set; }*/
        public string Maker { get; set; } = string.Empty;
        public string System { get; set; } = string.Empty;
        public DateTime Created_at { get; set; } = DateTime.Now;
    }
}
