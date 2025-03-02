using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Game
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(2500)]
        public string Description { get; set; }
        public string Cover { get; set; }
        public int CategoryId { get; set; }
        public Category category { get; set; }
        public ICollection<GameDevice> Devices { get; set; }
    }
}
