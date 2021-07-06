using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookies_Manager.Model
{
    [Table("cookies")]
    class Edge_Cookie : Browser_Cookie
    {

        public override string Path { get; } = $"C:\\Users\\Kneven\\AppData\\Local\\Microsoft\\Edge\\User Data\\Default";

        public Int64 creation_utc { get; set; }

        public string host_key { get; set; }

        public string name { get; set; }

        public string value { get; set; }

        public string path { get; set; }

        public Int64 expires_utc { get; set; }

        public int is_secure { get; set; }

        public int is_httponly { get; set; }

        public Int64 last_access_utc { get; set; }

        public int has_expires { get; set; }

        public int is_persistent { get; set; }

        public int priority { get; set; }

        public Byte[] encrypted_value { get; set; }

        public int samesite { get; set; }

        public int source_scheme { get; set; }

        public int source_port { get; set; }

        public int is_same_party { get; set; }

        public int is_edgelegacycookie { get; set; }

        public int browser_provenance { get; set; }

        public override string ToString()
        {
            return $"{this.creation_utc}|{this.host_key}|{this.name}|{this.value}|{this.path}|{this.expires_utc}|{this.is_secure}|{this.is_httponly}|{this.last_access_utc}|{this.has_expires}|{this.is_persistent}|{this.priority}|{this.encrypted_value}|{this.samesite}|{this.source_scheme}|{this.source_port}|{this.is_same_party}|{this.is_edgelegacycookie}|{this.browser_provenance}";
        }

    }
}
