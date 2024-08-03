using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HotelRoomDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter room name")]
        public string Name { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Range(1,3000, ErrorMessage ="RegularExpressionAttribute rate must be between 1 and 3000")]
        [Required]
        public double RegularRate { get; set; }
        public string Detail { get; set; }
        public string SqFt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string UpdateedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
