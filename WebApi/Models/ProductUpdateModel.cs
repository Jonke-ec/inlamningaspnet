namespace WebApi.Models {
    public class ProductUpdateModel {

        public ProductUpdateModel() {

        }

        public ProductUpdateModel(int id, string articleNumber, string productName, string description, decimal price) {
            Id = id;
            ArticleNumber = articleNumber;
            ProductName = productName;
            Description = description;
            Price = price;
        }

        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
