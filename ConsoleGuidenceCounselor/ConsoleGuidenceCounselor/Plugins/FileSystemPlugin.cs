// Copyright (c) Microsoft. All rights reserved.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.SemanticKernel;

namespace ConsoleGuidenceCounselor.Plugins;

/// <summary>
/// Class that holds information on students
/// </summary>
public class FileSystemPlugin
{
    private string studentName;

    public List<string> names = new List<string> { "Chad Perry", "Ben Victor", "Michael Tanczos", "Rowan Galiotto", "Rowan Tom" };

    private Dictionary<string, double> gpaData = new Dictionary<string, double>();
    private Random rnd = new Random();

    public FileSystemPlugin()
    {
        foreach(var name in names)
        {
            gpaData.Add(name, rnd.Next(80, 104));
        }

        studentName = names[(int)rnd.Next(0, 4)];

        Console.WriteLine("Welcome - " + studentName + "!!!");
    }


    [KernelFunction, Description("Sets the current student given their name")]
    public string SetName(string name)
    {
        if(names.Contains(name))
        {
            studentName = name;
            return GetCurrentStudent();
        }

        return "Invalid Student Name Given";

    }

    [KernelFunction, Description("Get current student")]
    public string GetCurrentStudent()
    {
        return $"Student set for {studentName}";
    }

    [KernelFunction, Description("Gets the current students gpa value assigned to them, If 0 is returned, the current student does not have access to that gpa. If no name is given the default parameter name is my")]
    public double GetCurrentGPA(string name)
    {
        if (names.Contains(name) && name.Equals(GetCurrentStudent()))
        {
            return gpaData[name];
        }
        else
        {
            if (name == "my")
            {
                return gpaData[studentName];
            }
            else
            {
                return 0.0;
            }
        }
           
    }

}
