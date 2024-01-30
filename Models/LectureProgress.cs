using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHW2Core.Models;

public class LectureProgress
{
    public int Id { get; set; }
    public int LectureId { get; set; }
    public int StudentId { get; set; }

    public Lecture Lecture { get; set; }
    public Student Student { get; set; }
}

