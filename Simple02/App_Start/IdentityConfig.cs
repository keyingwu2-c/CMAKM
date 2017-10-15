using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Simple02.Models;
using System.Web;
using System.Net.Mail;
using System.Data.Entity;
using URE.Common_Code;
using System.Net;
using System.Linq;
using System.Data.Entity.SqlServer.Utilities;

namespace Simple02
{
    public class EmailService : IIdentityMessageService
    {
      
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");

                mail.From = new MailAddress("shilo_wukeying@outlook.com");
                mail.To.Add("shilowu@cmatcl.com");
                //mail.To.Add(message.Destination);
                mail.Subject = message.Subject;
                mail.Body = message.Body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("shilo_wukeying@outlook.com", "1440097110NOBODY");
                //change another account to send mail when finish the functions
                SmtpServer.EnableSsl = true;

                //SmtpServer.Send(mail);
                await SmtpServer.SendMailAsync(mail);
                //MessageBox.Show("mail Send");

            }
            catch (Exception ex)
            {
                
                ex.ToString();                
            }
            //return Task.FromResult(0);


        }
 
        public async Task<IdentityResult> SendAsync01(IdentityMessage message)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                SmtpClient SmtpServer = new SmtpClient("192.168.0.226");

                mail.From = new MailAddress("IT@cmatcl.com");
                mail.To.Add("shilowu@cmatcl.com");
                //mail.To.Add("604192985@qq.com");
                //mail.To.Add(message.Destination);
                mail.Subject = message.Subject;
                mail.Body = message.Body;

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential();
                SmtpServer.EnableSsl = false;

                //SmtpServer.Send(mail);

                //Still can not observe whether mail is successfully sent or not here.
                await SmtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                //ViewBag.Catch = ex.ToString(); 
                return IdentityResult.Failed(ex.ToString());
            }
            return IdentityResult.Success;
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }//will never use this, don't need it!

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser,string>
    {
        //private IUserStore<IdentityUser> userStore;

        public ApplicationUserManager(IUserStore<ApplicationUser,string> store)
            : base(store)
        {
            
        }//IUserStore is a set of methods for user management APIs


       
        public virtual async Task<IdentityResult> SendEmailAsync01(string userId, string subject, string body)
        {
            //ThrowIfDisposed();
            //if (EmailService != null)//what does this condition mean ?
            //{
            var msg = new IdentityMessage
            {
                Destination = "keyingwu2-c@my.cityu.edu.hk",
                //Destination = await GetEmailAsync(userId),
                Subject = subject,
                Body = body,
            };

            EmailService service = new EmailService();

            var result = await service.SendAsync01(msg);
            return result;

            // }
            //return IdentityResult.Failed("EmailService is not valid");
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            /*
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string, 
                ApplicationUserLogin, ApplicationUserRole,
                ApplicationUserClaim>(context.Get<ApplicationDbContext>()));*/
            // Configure validation logic for usernames

            var manager = new ApplicationUserManager(new UserStore<ApplicationUser, ApplicationRole, string,
                ApplicationUserLogin, ApplicationUserRole,
                ApplicationUserClaim>(context.Get<ApplicationDbContext>()));

            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true,
                //Add items here to retrict to cma office mail format !
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }

        
        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            //ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            return ClaimsIdentityFactory.CreateAsync(this, user, authenticationType);
        }
    }

    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(
            IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            return new ApplicationRoleManager(
                new ApplicationRoleStore(context.Get<ApplicationDbContext>()));
        }
    }

    

    // Configure the application sign-in manager which is used in this application.
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public int rolevalue = 0;

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /*
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);//Is UserManager right?
        }*/

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public virtual async Task<SignInStatus> Authenticate(LoginViewModel model)
        {
            //ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            //ManagementObjectCollection collection = searcher.Get();
            //string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];

            NTAuth NewNTAuth = new NTAuth(9091);
            NetworkCredential creds = new NetworkCredential();
            creds.UserName = model.Username;
            creds.Password = model.Password;
            creds.Domain = "cmatcl.com";
            var ADSuccess = NewNTAuth.Authenticate(creds);

            if (true)
            {
                ApplicationDbContext identitygetter = new ApplicationDbContext();
                
                if (identitygetter.Users.Any(x => x.Id.Equals(model.Username)))
                {
                    //ApplicationUser user = identitygetter.Users.Single(x => x.Id.Equals(model.Username));
                    ApplicationUser user = identitygetter.Users.Find(model.Username);

                    if (user.Roles != null)
                    {
                        if (user.Roles.Any(x => x.UserId.Equals(user.Id)))
                            rolevalue = Convert.ToInt32(user.Roles.Last(x => x.UserId.Equals(user.Id)).RoleId);
                    }

                    await SignInAsync(user, model.RememberMe, false).WithCurrentCulture();
                }
                else
                {

                    identitygetter.Users.Add(new ApplicationUser(model.Username));
                    identitygetter.SaveChanges();

                    await SignInAsync(new ApplicationUser(model.Username), model.RememberMe, false).WithCurrentCulture();
                }
                
                return SignInStatus.Success;//auth success
            }
            else
            {
                return SignInStatus.Failure;//The password you entered is not correct. Please check!
            }
        }
    }

    public class ApplicationDbInitializer
    : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current
                .GetOwinContext().GetUserManager<ApplicationUserManager>();

            var roleManager = HttpContext.Current
                .GetOwinContext().Get<ApplicationRoleManager>();

            const string name = "admin@example.com";
            const string password = "Admin@123456";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new ApplicationRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }




}

