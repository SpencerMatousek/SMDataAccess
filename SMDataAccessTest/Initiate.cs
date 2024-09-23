using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDataAccessTest;

public static class Initiate
{
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true);
        return config.Build();
    }
}
