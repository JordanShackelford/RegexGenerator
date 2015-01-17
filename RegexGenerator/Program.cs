using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        var test = new List<String> { "a2c", "123" };

        var logic = new RegexGenerator();

        var regex = logic.GetRegex(test);

        Console.WriteLine(regex);

        Console.ReadLine();
    }
}
