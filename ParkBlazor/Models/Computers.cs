using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkBlazor.Models
{
    public class Computers
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Model")]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "Connected")]
        public bool Connected { get; set; } = true;

        [Display(Name = "Disconnected")]
        public bool Disconnected { get; set; } = false;

        [ForeignKey("Mapping")]
        public int MappingId { get; set; }
        // public Mapping? position { get; set; }

        [Display(Name = "Maker")]
        public string Maker { get; set; } = string.Empty;

        [Display(Name = "Operating System")]
        public string System { get; set; } = string.Empty;

        [Display(Name = "Created At")]
        public DateTime Created_at { get; set; } = DateTime.Now;

        public Computers()
        {

        }
    }

    
}