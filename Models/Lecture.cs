﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHW2Core.Models;

public class Lecture
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int CourseId { get; set; }

    public Course Course { get; set; }
    public ICollection<LectureProgress> LectureProgresses { get; set; }
}


