using System.ComponentModel.DataAnnotations;

namespace Company.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Hãy nhập username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Hãy nhập password")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}