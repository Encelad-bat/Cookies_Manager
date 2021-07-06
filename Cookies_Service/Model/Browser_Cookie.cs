using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data.SQLite;
using System.IO;

namespace Cookies_Service
{
    abstract class Browser_Cookie
    {
        abstract public string Path { get; }

    }
}
