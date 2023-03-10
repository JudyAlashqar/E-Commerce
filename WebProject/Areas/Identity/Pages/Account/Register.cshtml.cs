using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AppDbContext.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AppDbContext.Reops;
using System.IO;

namespace WebProject.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<AspNetRoles> _roleManager;
        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<AspNetRoles> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }
        
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "User Role")]
        [BindProperty]

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            [Display(Name = "User First Name")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            [Display(Name = "User Last Name")]
            public string LastName { get; set; }

            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            [Display(Name = "User Address")]
            public string Address { get; set; }

            [Display(Name = "User Gender")]
            public string Gendre { get; set; }

            [Required]
            [RegularExpression(@"(^09[0-9]{8}$)", ErrorMessage = "Phone Number must be a Syrian Mobile Phone Number (10 digits and the first two are 09)")]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
            [Display(Name = "Phone Number")]
            public string Phone { get; set; }

            [Display(Name = "Personal Photo")]
            public string Image { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                IFormFile file = null;
                foreach (var f in Request.Form.Files)
                {
                    file = f;
                }
                string img = "";
                if (file != null)
                {
                    img = Path.GetFileName(file.FileName);
                }
                var user = new User {Email = Input.Email, Address = Input.Address,
                FirstName = Input.FirstName, LastName = Input.LastName, Gendre = Input.Gendre, Image = img, Phone = Input.Phone, UserName = Input.FirstName + "_" + Input.LastName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (file != null)
                    {
                        if (file.Length > 0)
                        {
                            string file_name = Path.GetFileName(file.FileName);
                            string path = "C:\\Users\\ASUS\\source\\WebProject\\WebProject\\wwwroot\\images\\" + file_name;
                            using (Stream fileStream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                        }
                    }
                    CartItemRepo cartItemRepo = new CartItemRepo(new ProjectContext());
                    int cartId = -1;
                    if (HttpContext.Session.GetInt32("CartId") != null)
                        cartId = (int)HttpContext.Session.GetInt32("CartId");
                    cartItemRepo.MigrateCart(user.Id, cartId);
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Normal User");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
