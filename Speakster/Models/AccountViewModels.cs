using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Speakster.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [System.Web.Mvc.RemoteAttribute("IsUserExists", "Validation", ErrorMessage = "O email que você digitou já existe.")]
        [EmailAddress(ErrorMessage = "Você não digitou um endereço de email válido.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Idioma")]
        [DefaultValue(1)]
        public int Language_id { get; set; }

        [Display(Name = "Nível de Entendimento")]
        [DefaultValue(1)]
        public int ListeningLevel_id { get; set; }

        [Display(Name = "Nível de Conversação")]
        [DefaultValue(1)]
        public int SpeakingLevel_id { get; set; }

        public string loginProvider { get; set; }

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

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Você não digitou seu nome.")]
        [Display(Name = "Nome")]
        public string First_name { get; set; }

        [Required(ErrorMessage = "Você não digitou seu sobrenome.")]
        [Display(Name = "Sobrenome")]
        public string Last_name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Você não digitou um endereço de email válido.")]
        [System.Web.Mvc.RemoteAttribute("IsUserExists", "Validation", ErrorMessage = "O email que você digitou já existe.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite uma senha.")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} dígitos.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "As senhas digitadas não são iguais.")]
        public string ConfirmPassword { get; set; }

        //foreign keys begin
        [Display(Name = "Idioma")]
        [DefaultValue(1)]
        public int Language_id { get; set; }

        [ForeignKey("Language_id")]
        public virtual Language Language { get; set; }

        [Display(Name = "Nível de Conversação")]
        [DefaultValue(1)]
        public int SpeakingLevel_id { get; set; }

        [ForeignKey("SpeakingLevel_id")]
        public virtual SpeakingLevel SpeakingLevel { get; set; }

        [Display(Name = "Nível de Entendimento")]
        [DefaultValue(1)]
        public int ListeningLevel_id { get; set; }


        [ForeignKey("ListeningLevel_id")]
        public virtual ListeningLevel ListeningLevel { get; set; }

        public bool isTeacher { get; set; }
        //foreign keys end
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} dígitos.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
