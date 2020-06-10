using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace M10.Models
{
    public class StudentScores
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public int Lec1Scores { get; set; } = 0;
        public int Lec2Scores { get; set; } = 0;
        public int Lec3Scores { get; set; } = 0;
        public int Lec4Scores { get; set; } = 0;

        public IEnumerable<int> GetScoresAsCollection()
        {
            var result = new List<int>();
            result.Add(Lec1Scores);
            result.Add(Lec2Scores);
            result.Add(Lec3Scores);
            result.Add(Lec4Scores);

            return result;
        }
    }
}
