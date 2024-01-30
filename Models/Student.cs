using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHW2Core.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<CourseRegistration> CourseRegistrations { get; set; }
    public ICollection<LectureProgress> LectureProgresses { get; set; }
    public ICollection<WorkshopProgress> WorkshopProgresses { get; set; }
}

