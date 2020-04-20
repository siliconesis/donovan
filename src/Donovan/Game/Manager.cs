using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Donovan.Game
{
    public class Manager
    {
        public Manager()
        {
            Roles = new Collection<string>();
            //Teams = new Collection<Team>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime RegisteredOn { get; set; }

        public ICollection<string> Roles { get; }

        //public ICollection<Team> Teams { get; }

        public string VerificationStatus { get; set; }

        public Guid VerificationCode { get; set; }

        public DateTime VerificationCodeExpiresOn { get; set; }
    }
}
