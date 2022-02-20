using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities {
    public class AddressEntity {

        public AddressEntity() {

        }

        public AddressEntity(string streetName, string postalCode, string city) {
            StreetName = streetName;
            PostalCode = postalCode;
            City = city;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string StreetName { get; set; }

        [Required]
        [Column(TypeName = "char(5)")]
        public string PostalCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string City { get; set; }

        public ICollection<UserEntity> Users { get; set; }
    }
}
