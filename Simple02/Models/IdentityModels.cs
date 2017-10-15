using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Simple02.Models
{
    
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUserLogin : IdentityUserLogin<string> { }//may be used when adding third party login
    public class ApplicationUserClaim : IdentityUserClaim<string> { }//may be used when adding third party login
    public class ApplicationUserRole : IdentityUserRole<string> { }//if can, remove this and change the mapping
    
    // Must be expressed in terms of our custom Role and other types:
    public class ApplicationUser
        : IdentityUser<string, ApplicationUserLogin,
        ApplicationUserRole, ApplicationUserClaim>
    {
        [Column("Brief Introduction")]
        public string BriefIntrov
        { get; set; }

        public ApplicationUser()
        {
            //this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationUser(string Id)
        {
            this.Id = Id;
            EmailConfirmed = false;
            PhoneNumberConfirmed = false;
            TwoFactorEnabled = false;
            LockoutEnabled = false;
            AccessFailedCount = 0;
            UserName = Id.ToUpper();
            SecurityStamp = "default stamp";//May be should change the way to generate it 
        }

        public ICollection<Group> Groups { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {

            //ClaimsIdentity userIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, "Name", "ApplicationUser");
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            //the CreateIdentityAsync method returned null ! It should return an object of ClaimsIdentity.
            return userIdentity;
        }
    }


    // Must be expressed in terms of our custom UserRole:
    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public ApplicationRole(string name)
            : this()
        {
            this.Name = name;
        }

        // Add any custom Role properties/code here
        //public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }


    // Must be expressed in terms of our custom types:
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser, ApplicationRole,
        string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationDbContext()
            : base("MyDatabase1")
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        //public DbSet<ApplicationRole> Roles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        //public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        //public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Division> Divsions { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Enquiry> Enquirys { get; set; }
        //public DbSet<File> Files { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Vote> Votes { get; set; }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }*/
    }

    // Most likely won't need to customize these either, but they were needed because we implemented
    // custom versions of all the other types:
    public class ApplicationUserStore
        : UserStore<ApplicationUser, ApplicationRole, string,
            ApplicationUserLogin, ApplicationUserRole,
            ApplicationUserClaim>, IUserStore<ApplicationUser, string>,
        IDisposable
    {
        public ApplicationUserStore()
            : this(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationUserStore(DbContext context)
            : base(context)
        {
        }
    }


    public class ApplicationRoleStore
    : RoleStore<ApplicationRole, string, ApplicationUserRole>,
    IQueryableRoleStore<ApplicationRole, string>,
    IRoleStore<ApplicationRole, string>, IDisposable
    {
        public ApplicationRoleStore()
            : base(new IdentityDbContext())
        {
            base.DisposeContext = true;
        }

        public ApplicationRoleStore(DbContext context)
            : base(context)
        {
        }
    }


}