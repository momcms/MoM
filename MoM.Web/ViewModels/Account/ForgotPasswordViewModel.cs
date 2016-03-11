using System.ComponentModel.DataAnnotations;

namespace MoM.Web.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
