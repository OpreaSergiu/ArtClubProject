using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ArtClub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
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
        public DbSet<LocationsModels> LocationsModels { get; set; }
        public DbSet<ReservationsModels> ReservationsModels { get; set; }
        public DbSet<EventsModels> EventsModels { get; set; }
        public DbSet<EventGuestsModels> EventGuestsModels { get; set; }
        public DbSet<CostsModels> CostsModels { get; set; }
        public DbSet<PaymentsModels> PaymentsModels { get; set; }
        public DbSet<ApprovedReservationsModels> ApprovedReservationsModels { get; set; }
        public DbSet<UserRequestsModels> UserRequestsModels { get; set; }
    }
}