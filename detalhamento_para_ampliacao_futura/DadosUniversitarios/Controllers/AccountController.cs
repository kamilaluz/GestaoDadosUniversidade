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
        public async Task<IActionResult> Login(AccountLoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var usuarioLogado = await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

            if (usuarioLogado.Succeeded)
            {
                return RedirectToAction("Index", "Eventos");
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
            // Verificar se o e-mail já está cadastrado            
            var existingUser = await _userManager.FindByEmailAsync(register.Email);


            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "Você já está registrado no sistema.");
            }

            // Verificar se o e-mail já está cadastrado na tabela Usuario
            var emailCadastrado = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == register.Email);


            if (emailCadastrado == null)
            {
                var novoUsuario = new Usuario()
                {
                    Nome = register.Nome,
                    Email = register.Email,
                    Senha = register.Senha,
                    ConfirmacaoSenha = register.ConfirmacaoSenha,                    
                };
                _context.Add(novoUsuario);
            }
            else
            {
                // Atualizar usuário existente
                emailCadastrado.Nome = register.Nome;
                emailCadastrado.Email = register.Email;
                emailCadastrado.Senha = register.Senha;
                emailCadastrado.ConfirmacaoSenha = register.ConfirmacaoSenha;

            }

            await _context.SaveChangesAsync();

            // Registrar no sistema Identity
            var user = new IdentityUser()
            {
                UserName = register.Nome,
                Email = register.Email
            };
            await _userManager.CreateAsync(user, register.Senha);
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Eventos");

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }



    }
}
