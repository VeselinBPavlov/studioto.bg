namespace Studio.User.WebApp.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private const string EmailRequiredMessage = "Полето \"Email\" е задължително!";
        private const string FirstNameRequiredMessage = "Полето \"Име\" е задължително!";
        private const string LastNameRequiredMessage = "Полето \"Фамилия\" е задължително!";
        private const string PhoneRequiredMessage = "Полето \"Телефон\" е задължително!";
        private const string PasswordRequiredMessage = "Полето \"Парола\" е задължително!";
        private const string EmailFormatMessage = "Невалиден Email формат!";
        private const string PhoneFormatMessage = "Невалиден телефонен формат!";
        private const string UnconfirmedPasswordMessage = "Паролите не съвпадат!";
        private const string LenghtRequirementMessage = "Полето \"{0}\" трябва да е с дължина от {2} до {1} символа.";



        private readonly SignInManager<StudioUser> _signInManager;
        private readonly UserManager<StudioUser> _userManager;
        private readonly RoleManager<StudioRole> _roleManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<StudioUser> userManager,
            SignInManager<StudioUser> signInManager,
            RoleManager<StudioRole> roleManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = FirstNameRequiredMessage)]
            [StringLength(50, ErrorMessage = LenghtRequirementMessage, MinimumLength = 2)]
            [MinLength(2), MaxLength(50)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = LastNameRequiredMessage)]
            [StringLength(50, ErrorMessage = LenghtRequirementMessage, MinimumLength = 2)]
            [MinLength(2), MaxLength(50)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required(ErrorMessage = PhoneRequiredMessage)]
            [RegularExpression(@"^(\+359|0)(\d{9})$", ErrorMessage = PhoneFormatMessage)]
            [Display(Name = "Phone")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = EmailRequiredMessage)]
            [EmailAddress(ErrorMessage = EmailFormatMessage)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = PasswordRequiredMessage)]
            [StringLength(100, ErrorMessage = LenghtRequirementMessage, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = UnconfirmedPasswordMessage)]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new StudioUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, PhoneNumber = Input.PhoneNumber };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (_userManager.Users.Count() == 1) 
                {
                   await _userManager.AddToRoleAsync(user, "Administrator");
                }   
                else 
                {
                   await _userManager.AddToRoleAsync(user, "User");
                }                   

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");                    

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
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
