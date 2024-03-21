using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HouYun.Models
{
    public class User : IdentityUser
    {
        [Key]
        public override string Id { get; set; }

        [EmailAddress]
        public override string Email { get; set; }

        [StringLength(50)]
        public override string UserName { get; set; }

        public Application Application { get; set; }
        public Channel Channel { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }

        public User()
        {
            Subscriptions = new List<Subscription>();
            Channel = new Channel();
            Application = new Application();
        }
    }
}
