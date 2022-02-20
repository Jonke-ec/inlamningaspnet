namespace WebApi.Models {
    public class UserOutputModel {

        public UserOutputModel() {

        }

        public UserOutputModel(int id) {
            Id = id;
        }

        public UserOutputModel(int id, string firstName, string lastName, string email, AddressModel address) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Address = address;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }
    }
}
