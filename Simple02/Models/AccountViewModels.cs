using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Simple02.Models
{
    /*
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    */
    public class ApplyRoleViewModel
    {
        [Required]
        [Display(Name ="CMA Email Address")]
        public string Email { get; set; }// only input string before @cmatcl !

        [Required]
        [Display(Name = "Requested Role")]
        public string RequestedRole { get; set; }

        //public IEnumerable<Customer> Customers { get; set; }
    }

    public class LoginViewModel
    {
        
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }


    }
    /*
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Official Mail")]
        public string Email { get; set; }
        //Additional retrictions are needed on this email input

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm")]
        [Compare("Password", ErrorMessage = "It is not consistant with the password.")]
        public string PasswordConfirm { get; set; }


     

        [Required]
        [Display(Name = "Role")]
        public string ReqRole { get; set; }


        //IEnumerable items = System.Enum.GetValues(typeof(Testing));
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    //Use when forgot password

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
    */

}
