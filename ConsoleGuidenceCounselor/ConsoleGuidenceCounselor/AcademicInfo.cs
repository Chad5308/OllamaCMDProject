using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGuidenceCounselor;

public class AcademicInfo
{
    public double GpaUnweighted { get; set; }
    public double GpaWeighted { get; set; }
    public double ClassRank { get; set; }
    public double CreditsEarned { get; set; }
    public Dictionary<string, double> TestScores { get; set; }
    public Dictionary<string, TimeOnly> Classes { get; set; }

}
