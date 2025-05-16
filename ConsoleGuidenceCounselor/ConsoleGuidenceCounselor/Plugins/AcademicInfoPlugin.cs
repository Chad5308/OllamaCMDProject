// Copyright (c) Microsoft. All rights reserved.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.SemanticKernel;

namespace ConsoleGuidenceCounselor.Plugins;

/// <summary>
/// Class that holds information on students
/// </summary>
public class AcademicInfoPlugin
{
    private string email = "tanczosm@eastonsd.org";


    private Dictionary<string, double> gpaData = new Dictionary<string, double>();
    //Such as, gpa, schedule, grad requirments, test scores, aura

    [Description ("Represents a Dictionary list of keys paired to values where the key is the name of a class in the students schedule, and the value is the time the class meets at")]
    private Dictionary<string, TimeOnly> classes = new Dictionary<string, TimeOnly>();

    

    public AcademicInfoPlugin()
    {
        gpaData.Add(email, 2.86);
        classes.Add("Physics 2", new TimeOnly(7, 20, 0));
        classes.Add("Calc 3", new TimeOnly(10, 15, 0));
        classes.Add("Honors Humanities", new TimeOnly(11, 38, 0));

        Console.WriteLine("Welcome - " + email + "!");
    }

    [KernelFunction, Description("Get current email")]
    public string GetCurrentEmail()
    {
        return $"Student set for {email}";
    }


    [KernelFunction, Description("Gets the current students gpa")]
    public double GetCurrentGPA(string email)
    {
        return gpaData[email];
    }

    [KernelFunction, Description("Gets the students schedule in the form of a Dictionary where the key is the name of the class and the value is the time of the class. The classes are in order of when they are scheduled for")]
    public Dictionary<string, TimeOnly> getSchedule()
    {
        return classes;
    }

    [KernelFunction, Description("Gets the class start time given the name if the class. If an empty string is returned respond by saying The class in not in your schedule")]
    public string getClass(string className)
    {
        if(classes.ContainsKey(className))
        {
            return className + " is scheduled for " + classes[className].ToString();
        }else
        {
            return "";
        }
    }

}
