using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMSProject.Configuration
{
    public class IConfiguration
    {
        String OrderEmail { get; set; }

        String EmailApiKey { get; set; }

        String DomainForApiKey { get; set; }

        String FromName { get; set; }

        String FromEmail { get; set; }

        List<String> CreditCardType { get; set; }
    }
}