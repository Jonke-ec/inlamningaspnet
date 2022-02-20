using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.Entities {

    
    public class ProductEntity {

        public ProductEntity() {

        }

        public ProductEntity(int id, string articleNumber, string productName, decimal price) {
            Id = id;
            ArticleNumber = articleNumber;
            ProductName = productName;
            Price = price;
        }

        public ProductEntity(int id, string articleNumber, string productName, string description, decimal price, CategoryEntity category) {
            Id = id;
            ArticleNumber = articleNumber;
            ProductName = productName;
            Description = description;
            Price = price;
            Category = category;
        }

        public ProductEntity(int id, string articleNumber, string productName, string description, decimal price) {
            Id = id;
            ArticleNumber = articleNumber;
            ProductName = productName;
            Description = description;
            Price = price;
        }

        public ProductEntity(int id) {
            Id = id;
        }

        public ProductEntity(int id, string productName) {
            Id = id;
            ProductName = productName;
        }

        public ProductEntity(string articleNumber, string productName, string description, decimal price) {
            ArticleNumber = articleNumber;
            ProductName = productName;
            Description = description;
            Price = price;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ArticleNumber { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public CategoryEntity Category { get; set; }

        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}
