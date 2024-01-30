using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHW2Core.Models;

public class CourseRegistration
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int StudentId { get; set; }

    public Course Course { get; set; }
    public Student Student { get; set; }
}
