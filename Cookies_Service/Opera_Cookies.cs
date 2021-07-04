using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookies_Service
{
    class Opera_Cookies : Browser_Cookies
    {

        public override string Path { get; } = @"C:\Users\Kneven\AppData\Roaming\Opera Software\Opera Stable";

    }
}
