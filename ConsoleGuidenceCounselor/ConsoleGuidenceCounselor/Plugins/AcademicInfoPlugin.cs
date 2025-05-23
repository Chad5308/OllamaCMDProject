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


    private Dictionary<string, AcademicInfo> students = new Dictionary<string, AcademicInfo>();



    public AcademicInfoPlugin()
    {
        //gpaData.Add(email, 2.86);
        students.Add(email, new AcademicInfo()
        {
            GpaUnweighted = 2,
            GpaWeighted = 4,
            ClassRank = 565,
            CreditsEarned = 4.25,
            TestScores = new Dictionary<string, double>()
            {
                {"ACT", 12.5 },
                {"SAT", 875.2},
                {"Algaebra Keystone", 900.28}
            },
            Classes = new Dictionary<string, TimeOnly>()
            {
                {"Physics 2", new TimeOnly(7, 20, 0)},
                {"Calc 3", new TimeOnly(10, 15, 0)},
                {"Honors Humanities", new TimeOnly(11, 38, 0) }
            }
        });

        Console.WriteLine("Welcome - " + email + "!");
    }

    [KernelFunction, Description("Get current email")]
    public string GetCurrentEmail()
    {
        return $"Student set for {email}";
    }


    [KernelFunction, Description("Gets the current students wighted gpa")]
    public double GetWeightedGPA(string email)
    {
        return students[email].GpaWeighted;
    }

    [KernelFunction, Description("Gets the current students unwighted gpa")]
    public double GetUnweightedGPA(string email)
    {
        return students[email].GpaUnweighted;
    }

    [KernelFunction, Description("Gets the students schedule in the form of a Dictionary where the key is the name of the class and the value is the time of the class. The classes are in order of when they are scheduled for")]
    public Dictionary<string, TimeOnly> getSchedule()
    {
        return students[email].Classes;
    }

    [KernelFunction, Description("Gets the class start time given the name if the class. If an empty string is returned respond by saying The class in not in your schedule")]
    public string getClass(string className)
    {
        if(students[email].Classes.ContainsKey(className))
        {
            return className + " is scheduled for " + students[email].Classes[className].ToString();
        }else 
        {
            return "";
        }
    }

}
