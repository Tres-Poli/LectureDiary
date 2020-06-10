using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M10.Models
{
    public class LecturerProfileViewModel
    {
        public List<string> Lectures { get; set; }
        public Dictionary<string, List<int>> AllStudentScores { get; set; }
    }
}
