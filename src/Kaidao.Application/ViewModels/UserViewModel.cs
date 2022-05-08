namespace Kaidao.Application.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string id, string userName, string email, string phoneNumber, bool emailConfirmed, string firstName, string lastName, DateTime dob)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            EmailConfirmed = emailConfirmed;
            FirstName = firstName;
            LastName = lastName;
            Dob = dob;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }
    }
}