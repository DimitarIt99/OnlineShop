namespace ProductShop.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;
    using ProductShop.Data.Models;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private const string regexPhone = @"^(?<countryCode>\+[0-9]{1,3})[^\S]{0,1}(?<firstNumberPart>[0-9]{3})[^\S]{0,1}(?<secondNumberPart>[0-9]{4})$";

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Please enter a valid email.")]
            [EmailAddress(ErrorMessage = "Please enter a valid email.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Please enter a password.")]
            [StringLength(32, ErrorMessage = "The Password must be at least 6 and at max 32 characters long.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "Please enter your first name.")]
            [Display(Name = "First name")]
            [MaxLength(40, ErrorMessage = "Your first name must be less then 40 symbols.")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Please enter your last name.")]
            [Display(Name = "Last name")]
            [MaxLength(40, ErrorMessage = "Your last name must be less then 40 symbols.")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Please enter your phone number.")]
            [Display(Name = "Phone number")]
            [RegularExpression(regexPhone, ErrorMessage = "Please enter your phone number.")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Please enter your username.")]
            [MaxLength(30, ErrorMessage = "Your username must be between 4 and 30 symbols.")]
            [MinLength(4, ErrorMessage = "Your username must be between 4 and 30 symbols.")]
            public string Username { get; set; }

            [Range(1, 31, ErrorMessage = "Please enter a valid day.")]
            public int Day { get; set; }

            [Range(1, 12, ErrorMessage = "Please enter a valid month.")]
            public int Month { get; set; }

            [Range(1900, 2020, ErrorMessage = "Please enter a valid year.")]
            public int Year { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var birthday = new DateTime(Input.Year, Input.Month, Input.Day);
                var phoneNumber = string.Join(string.Empty, Input.Phone.Split(" "));
                var user = new ApplicationUser
                {
                    UserName = Input.Username,
                    Email = Input.Email,
                    PhoneNumber = phoneNumber,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    BirthDay = birthday,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
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
