using Newtonsoft.Json;
using Shared.Support.ClassExtensions;
using System;

namespace Shared.Support.Configuration
{
    public class DbConnectionConfig
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string SSPI { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string Password { get; set; }

        [JsonProperty("Password")]
        public string PasswordEncrypt { get => NeedEncrypt(); }

        private string NeedEncrypt()
        {
            try
            {
                Password.Decrypt();

                return Password;
            }
            catch (Exception)
            {
                return Password.Encrypt();
            }
        }

        public string GetConnectionString()
        {
            string password;
            string conn;

            try
            {
                password = Password.Decrypt();
            }
            catch (Exception)
            {
                password = Password;
            }

            conn = string.Format("data source={0}; initial catalog={1}; persist security info=True; user id={2}; password={3}; MultipleActiveResultSets=True; App=EntityFrameworkCore",
                Server, Database, User, password);

            if (!string.IsNullOrEmpty(SSPI))
                if (SSPI.DynamicConvert<bool>())
                    conn += ";Integrated Security=SSPI";

            return conn;
        }

        public bool HaveConnectionString()
        {
            return !string.IsNullOrEmpty(Database);
        }

        public string ToJson()
        {
            string format = "{{ \"{0}\": {1} }}";

            string result = string.Format(format, "DbConnection", JsonConvert.SerializeObject(this));

            return result;
        }
    }
}
