using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M10.Models
{
    public class ProfileViewModel
    {
        public string UserName { get; set; }
        public List<string> Lectures { get; set; }
        public List<int> Scores { get; set; }
    }
}
