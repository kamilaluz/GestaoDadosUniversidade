using DadosUniversitarios.Models;
using DadosUniversitarios.Services;
using DadosUniversitarios.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DadosUniversitarios.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Usuario login)
        {
            
            var usuarioLogado = await _signInManager.PasswordSignInAsync(login.Nome, login.Senha, false, false);

            if (usuarioLogado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Login ou senha inválidos.");
            return View(login);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario register)
        {
            if (register.Senha != register.ConfirmacaoSenha)
            {
                ModelState.AddModelError(string.Empty, "As senhas não coincidem.");
                return View(register);
            }

            // Verificar se o e-mail já está cadastrado
            var existingUser = await _userManager.FindByEmailAsync(register.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Você já está registrado no sistema.");
                return View(register);
            }

            // Criar o usuário no sistema Identity
            var user = new IdentityUser
            {
                UserName = register.Nome, // Usando o email como nome de usuário
                Email = register.Email
            };

            // Criar o usuário com a senha criptografada
            var result = await _userManager.CreateAsync(user, register.Senha);

            if (result.Succeeded)
            {
                // Login automático após o registro
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(register);

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }



    }
}
