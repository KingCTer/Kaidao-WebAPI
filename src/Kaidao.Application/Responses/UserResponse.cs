namespace Kaidao.Application.Responses
{
    public class UserResponse
    {
        public UserResponse(string id, string userName, string email, string phoneNumber)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}