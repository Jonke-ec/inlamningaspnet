namespace WebApi.Models {
    public class CategoryModel {

        public CategoryModel() {

        }
        public CategoryModel(string categoryName) {
            CategoryName = categoryName;
        }

        public string CategoryName { get; set; }

    }
}
