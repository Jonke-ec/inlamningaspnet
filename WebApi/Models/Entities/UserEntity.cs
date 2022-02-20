using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities {

    [Index(nameof(Email), IsUnique = true)]

    public class UserEntity {

        public UserEntity() {

        }

        public UserEntity(int id) {
            Id = id;
        }

        public UserEntity(string firstName, string lastName, string email) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public UserEntity(string firstName, string lastName, string email, int addressId) {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AddressId = addressId;
        }

        public UserEntity(int id, string email) {
            Id = id;
            Email = email;
        }

        public UserEntity(int id, string firstName, string lastName, string email, int addressId) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            AddressId = addressId;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }

        [Required]
        public int AddressId { get; set; }
        public AddressEntity Address { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }
    }
}
