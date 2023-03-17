using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
