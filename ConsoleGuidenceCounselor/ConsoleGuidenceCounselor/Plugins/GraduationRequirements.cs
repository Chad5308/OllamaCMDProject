using Microsoft.SemanticKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGuidenceCounselor.Plugins;


public class GraduationRequirements
{
    public string path;

    [Description("Pennslyvania Graduation requirements data")]
    private string graduationPathways;
    public GraduationRequirements()
    {
        path = Path.Combine(Environment.CurrentDirectory, "GraduationRequirements.txt");
        graduationPathways = File.ReadAllText(path);
    }


    [KernelFunction, Description("This method returns the graduation pathway requirment data given by the Pennslyvania Department of Education. Use this method to search this text when asked questions about graduation requirments")]
    public string getRequirements()
    {
        return graduationPathways;
    }


}