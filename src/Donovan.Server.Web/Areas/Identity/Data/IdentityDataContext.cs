using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ElCamino.AspNetCore.Identity.AzureTable;
using ElCamino.AspNetCore.Identity.AzureTable.Model;

namespace Donovan.Server.Web.Areas.Identity.Data
{
    public class IdentityDataContext : IdentityCloudContext
    {
        public IdentityDataContext()
            : base()
        {
        }

        public IdentityDataContext(IdentityConfiguration config)
            : base(config)
        {
        }
    }
}
