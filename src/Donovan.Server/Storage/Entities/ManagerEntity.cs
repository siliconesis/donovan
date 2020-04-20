using System;
using System.Collections.ObjectModel;
using Microsoft.Azure.Cosmos.Table;
using Donovan.Game;
using Donovan.Utilities;

namespace Donovan.Server.Storage.Entities
{
    public class ManagerEntity : TableEntity
    {
        public ManagerEntity()
        {
        }

        public ManagerEntity(Manager model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            this.PartitionKey = Base64Helper.ToBase64(model.Email);
            this.RowKey = Base64Helper.ToBase64(model.Email);

            this.Id = model.Id;
            this.Name = model.Name;
            this.Password = model.Password;
            this.RegisteredOn = model.RegisteredOn;
            this.Roles = JsonHelper.ToJson(model.Roles);
            //this.Teams = JsonHelper.ToJson(model.Teams);
            this.VerificationStatus = model.VerificationStatus;
            this.VerificationCode = model.VerificationCode;
            this.VerificationCodeExpiresOn = model.VerificationCodeExpiresOn;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

        //public string Teams { get; set; }

        public DateTime RegisteredOn { get; set; }

        public string VerificationStatus { get; set; }

        public Guid VerificationCode { get; set; }

        public DateTime VerificationCodeExpiresOn { get; set; }

        public Manager ToManager()
        {
            var manager = new Manager()
            {
                Id = this.Id,
                Name = this.Name,
                Email = Base64Helper.FromBase64(this.RowKey),
                Password = this.Password,
                RegisteredOn = this.RegisteredOn,
                VerificationStatus = this.VerificationStatus,
                VerificationCode = this.VerificationCode,
                VerificationCodeExpiresOn = this.VerificationCodeExpiresOn
            };

            var roles = JsonHelper.FromJson<Collection<string>>(this.Roles);

            foreach (var role in roles)
            {
                manager.Roles.Add(role);
            }

            /*
            var teams = JsonHelper.FromJson<Collection<Team>>(this.Teams);

            foreach (var team in teams)
            {
                manager.Teams.Add(team);
            }
            */

            return manager;
        }
    }
}
