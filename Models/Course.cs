using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleAppHW2Core.Models;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public ICollection<Lecture> Lectures { get; set; }
    public ICollection<Workshop> Workshops { get; set; }
    public ICollection<CourseRegistration> CourseRegistrations { get; set; }
}

