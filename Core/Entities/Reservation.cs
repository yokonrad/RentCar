using Core.ValidationAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public int CarId { get; set; }
        public Car? Car { get; set; }

        [Required]
        public int LocationFromId { get; set; }
        [DisplayName("Location From")]
        public Location? LocationFrom { get; set; }

        [Required]
        public int LocationToId { get; set; }
        [DisplayName("Location To")]
        public Location? LocationTo { get; set; }

        [DateFromToValidator]
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date From")]
        public DateTime DateFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date To")]
        public DateTime DateTo { get; set; }
    }
}
