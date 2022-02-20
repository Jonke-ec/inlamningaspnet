using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities {

    public class OrderEntity {

        public OrderEntity() {

        }

        public OrderEntity(int quantity) {
            Quantity = quantity;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Created { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Updated { get; set; } = DateTime.Now;

        [Required]
        public int Quantity { get; set; }


        public int UserId { get; set; }
        public int ProductId { get; set; }


        public UserEntity User { get; set; }
        public ProductEntity Product { get; set; }

    }
}
