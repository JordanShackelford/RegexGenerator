using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class RegexGenerator
{
    private List<List<Char>> PivotStrings(List<String> strings)
    {
        //pivot the columns of chars in the strings to rows
        //ABC, 123 => A1, B2, C3
        var pivotedArray = new List<List<Char>>();

        //below logic requires ascending string length
        strings = strings.OrderBy(x => x.Length).ToList();

        foreach(var current in strings) 
        {
            for (int j=0; j < current.Length; j++) 
            {
                //ensure an array exists for each column of chars
                if (j + 1 > pivotedArray.Count)
                    pivotedArray.Add(new List<Char>());

                pivotedArray[j].Add(current[j]);
            }
        }

        return pivotedArray;
    }
}