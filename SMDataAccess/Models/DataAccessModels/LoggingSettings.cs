using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMDataAccess.Models.DataAccessModels;
public class LoggingSettings
{
    public bool LogSqlScripts { get; set; } = false;
    public bool LogParameters { get; set; } = false;
}