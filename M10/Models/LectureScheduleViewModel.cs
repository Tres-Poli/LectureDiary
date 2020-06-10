using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M10.Models
{
    public class LectureScheduleViewModel
    {
        public LectureScheduleViewModel(IEnumerable<Lecture> lectures)
        {
            Lectures = lectures.ToList();
        }
        public List<Lecture> Lectures { get; set; }
    }
}
