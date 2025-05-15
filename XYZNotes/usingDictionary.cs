using System;
using System.Collections.Generic;

public class Class1
{
	public Class1()
	{
        Dictionary<string, int> studentGrades = new Dictionary<string, int>();

        // Adding key-value pairs to the dictionary
        studentGrades["John"] = 90;
        studentGrades["Sarah"] = 85;
        studentGrades["Michael"] = 92;

        // Accessing values using keys
        Console.WriteLine(studentGrades["Sarah"]);  // Output: 85

        // Checking if a key exists
        if (studentGrades.ContainsKey("John"))
        {
            int grade = studentGrades["John"];
            Console.WriteLine("John's grade: " + grade);
        }

        // Iterating over the key-value pairs
        foreach (var pair in studentGrades)
        {
            Console.WriteLine(pair.Key + ": " + pair.Value);
        }

        //
        // Removing an item from the dictionary
        //studentGrades.Remove("Sarah");

    }
}
