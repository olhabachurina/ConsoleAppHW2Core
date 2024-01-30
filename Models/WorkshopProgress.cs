using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppHW2Core.Models;

public class WorkshopProgress
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int WorkshopId { get; set; }

    public Workshop Workshop { get; set; }
    public Student Student { get; set; }
}
