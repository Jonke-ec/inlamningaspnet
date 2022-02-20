using WebApi.Models.Entities;

namespace WebApi.Models {
    public class OrderOutputModel {

        public OrderOutputModel() {

        }

        public OrderOutputModel(int id, DateTime created, int quantity, UserEntity user, ProductEntity product) {
            Id = id;
            Created = created;
            Quantity = quantity;
            User = user;
            Product = product;
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int Quantity { get; set; }

        public UserEntity User { get; set; }
        public ProductEntity Product { get; set; }

    }
}
