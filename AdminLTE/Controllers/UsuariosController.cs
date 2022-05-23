using AdminLTE.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminLTE.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<UsuarioViewModel> userManager;
        private readonly SignInManager<UsuarioViewModel> signInManager;

        public UsuariosController(UserManager<UsuarioViewModel> userManager, SignInManager<UsuarioViewModel> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel registro)
        {
            if (!ModelState.IsValid)
            {
                return View(registro);
            }

            var usuario = new UsuarioViewModel()
            {
                Nombre = registro.Nombre,
                ApePat = registro.ApePat,
                ApeMat = registro.ApeMat,
                Email = registro.Email,

            };

            var resultado = await userManager.CreateAsync(usuario, password: registro.Password);

            if (resultado.Succeeded)
            {
                await signInManager.SignInAsync(usuario,isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(registro);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var resultado = await signInManager.PasswordSignInAsync(login.Email, login.Password, login.Recuerdame, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email del usuario o contraseña incorrecto.");

                return View(login);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
