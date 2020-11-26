using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace AmdarisInternship.Domain.Auth
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Skype { get; set; }

        public string AvatarExtension { get; set; }

        public string AvatarName { get; set; }

        public byte[] Avatar { get; set; }

        public Nullable<int> MentroId { get; set; }

        public List<Exam> Exams { get; set; }

        public List<Grade> Grades { get; set; }

        public List<UserPromotion> UserPromotions { get; set; }

        public List<Lesson> Lessons { get; set; }
    }
}
