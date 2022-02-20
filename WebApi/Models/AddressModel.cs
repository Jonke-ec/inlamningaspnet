namespace WebApi.Models {
    public class AddressModel {

        public AddressModel() {

        }

        public AddressModel(string streetName, string postalCode, string city) {
            StreetName = streetName;
            PostalCode = postalCode;
            City = city;
        }

        public string StreetName { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
    }
}
