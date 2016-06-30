using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [DefaultValue(1)]
        public int Language_id { get; set; }

        [ForeignKey("Language_id")]
        public virtual Language Language { get; set; }


        [DefaultValue(1)]
        public int SpeakingLevel_id { get; set; }

        [ForeignKey("SpeakingLevel_id")]
        public virtual SpeakingLevel SpeakingLevel { get; set; }


        [DefaultValue(1)]
        public int ListeningLevel_id { get; set; }


        [ForeignKey("ListeningLevel_id")]
        public virtual ListeningLevel ListeningLevel { get; set; }


        //public virtual Student Student { get; set; }


        public string ProfilePicture { get; set; }

        public bool Active { get; set; }

        public bool NewMessage { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<WebApplication3.Models.Language> Languages { get; set; }

        public System.Data.Entity.DbSet<WebApplication3.Models.Teacher> Teachers { get; set; }

        public System.Data.Entity.DbSet<WebApplication3.Models.SpeakingLevel> SpeakingLevels { get; set; }

        public System.Data.Entity.DbSet<WebApplication3.Models.ListeningLevel> ListeningLevels { get; set; }

        public System.Data.Entity.DbSet<WebApplication3.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<WebApplication3.Models.UserPayment> UserPayments { get; set; }

        public System.Data.Entity.DbSet<WebApplication3.Models.ChatMessage> ChatMessages { get; set; }



    }

}