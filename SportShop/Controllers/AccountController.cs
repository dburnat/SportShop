using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportShop.Views;
using SportShop.Models;

namespace SportShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserValidator<AppUser> _userValidator;
        private readonly IPasswordValidator<AppUser> _passwordValidator;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signIn, IUserValidator<AppUser> userValidator, IPasswordValidator<AppUser> passwordValidator, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userMgr;
            _signInManager = signIn;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;

            //IdentitySeedData.EnsurePopulated(userMgr).Wait();
        }
        //TODO
        //Seperate logging in
        public ViewResult Index() => View(_userManager.Users);

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                    Name = model.Name,
                    Password = model.Password
                };

                IdentityResult result = await _userManager.CreateAsync(user, user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError identityError in result.Errors)
                    {
                        ModelState.AddModelError("",identityError.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika");
            }

            return View("Index", _userManager.Users);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityUser user =  await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id,  string password, string email, string userName)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                user.Name = userName;
                IdentityResult validEmail = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPassword = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPassword = await _passwordValidator.ValidateAsync(_userManager, user, password);
                    if (validPassword.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                        //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPassword);
                    }
                }


                if ((validEmail.Succeeded && validPassword== null) || (validEmail.Succeeded && password != string.Empty && validPassword.Succeeded))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie znaleziono użytkownika");
                }

                
            }
            return View(user);
        }
        [AllowAnonymous]
        public ViewResult Login(string returnUrl)
        {
            return View(new AppUser
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AppUser loginModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(loginModel.Name);
                if (user != null)
                {
                    if ((await _signInManager.PasswordSignInAsync(user,loginModel.Password,false,false)).Succeeded)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }
            ModelState.AddModelError("LoginError", "Nieprawidłowe dane logowania");
            return View(loginModel);
        }


        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}
