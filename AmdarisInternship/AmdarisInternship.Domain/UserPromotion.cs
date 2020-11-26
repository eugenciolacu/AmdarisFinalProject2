using AmdarisInternship.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmdarisInternship.Domain
{
    public class UserPromotion : BaseEntity
    {
        public int UserId { get; set; }

        public int PromotionId { get; set; }

        public User User { get; set; }

        public Promotion Promotion { get; set; }
    }
}
