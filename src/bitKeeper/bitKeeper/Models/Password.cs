using System;
using System.Collections.Generic;
using System.Text;

namespace bitKeeper.Models
{
    public class Secret
    {
        public Secret() : this(string.Empty)
        { }
        public Secret(string Password)
        {
            this.Password = Password;
        }

        public void SetSecret(Secret secret)
        {
            this.Password = secret.Password;
        }

        public string Password { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Password);
    }
}
