namespace WebApi.Models {
    public class ProductOutputModel {

        public ProductOutputModel() {

        }

        public ProductOutputModel(string productName) {
            ProductName = productName;
        }

        public ProductOutputModel(int id) {
            Id = id;
        }

        public ProductOutputModel(int id, string articleNumber, string productName, string description, decimal price, CategoryModel category) {
            Id = id;
            ArticleNumber = articleNumber;
            ProductName = productName;
            Description = description;
            Price = price;
            Category = category;
        }

        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CategoryModel Category { get; set; }
    }
}
