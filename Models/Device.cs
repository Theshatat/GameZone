using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Device
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public string Icon { get; set; }
        public ICollection<GameDevice> Game { get; set; }

    }
}
