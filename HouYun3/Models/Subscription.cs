using System.ComponentModel.DataAnnotations.Schema;

namespace HouYun3.Models
{
    public class Subscription
    {
        public Guid SubscriptionId { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid ChannelId { get; set; }
        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }
    }
}
