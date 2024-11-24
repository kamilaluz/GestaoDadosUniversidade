using System.ComponentModel.DataAnnotations;

namespace DadosUniversitarios.ViewModels
{
    public class AccountLoginViewModel
    {
        [Required(ErrorMessage = "Informe o usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Password { get; set; }
    }
}
