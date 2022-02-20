namespace WebApi.Models {
    public class OrderUpdateModel {

        public OrderUpdateModel() {

        }

        public OrderUpdateModel(int id, int quantity) {
            Id = id;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
