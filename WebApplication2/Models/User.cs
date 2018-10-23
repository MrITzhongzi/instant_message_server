using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Tel { get; set; }
        public int Password { get; set; }
    }
}
