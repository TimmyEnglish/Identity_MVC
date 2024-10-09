namespace ASP_NET_MVC.Models
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Email must be in the format '.....@.......'")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{3} \d{3} \d{2} \d{2}$", ErrorMessage = "Phone must be in the format '___ ___ __ __'")]
        public string? PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
