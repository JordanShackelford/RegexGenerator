using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class RegexGenerator
    {
        public String GetRegex(List<String> matches)
        {
            var regex = new StringBuilder();

            var characterSets = ExtractCharacterSets(matches);

            var isOptional = false;

            for (int i = 0; i < characterSets.Count; i++ )
            {
                var regexForCharacterSet = RegexForCharacterSet(characterSets[i]);

                regex.Append(regexForCharacterSet);

                //if a set ever came from a string longer than the previous, all further
                //regex chars are optional
                if (i != 0)
                {
                    if (characterSets[i].Count < characterSets[i - 1].Count)
                        isOptional = true;
                }

                if (isOptional)
                    regex.Append("?");
            }

            return regex.ToString();
        }

        private List<List<Char>> ExtractCharacterSets(List<String> matches)
        {
            //pivot the columns of chars in the strings to rows
            //ABC, 123 => { {A, 1} ; {B, 2} ; {C, 3} }
            var characterSets = new List<List<Char>>();

            foreach (var match in matches)
            {
                for (var i = 0; i < match.Length; i++)
                {
                    //ensure a list exists for each set of chars
                    if (i + 1 > characterSets.Count)
                        characterSets.Add(new List<Char>());

                    characterSets[i].Add(match[i]);
                }
            }

            return characterSets;
        }

        private String RegexForCharacterSet(List<Char> characterSet)
        {
            //all the same
            if(characterSet.All(x => x == characterSet.First()))
                return characterSet.First().ToString();

            else if (characterSet.All(Char.IsDigit))
                return @"\d";

            else if (characterSet.All(Char.IsLetter))
                return @"[A-Za-z]";

            else if (characterSet.All(Char.IsLetterOrDigit))
                return @"\w";

            else if (characterSet.All(Char.IsSymbol))
                return SymbolsGroup(characterSet);

            else
                return ".";
        }

        private String SymbolsGroup(List<Char> characterSet)
        {
            var group = new StringBuilder();

            group.Append("[");

            foreach (var character in characterSet)
                group.Append(character);
        
            group.Append("]");

            //dashes have to be escaped in character grouping
            //or it will think we mean "through"
            group.Replace("-", @"\-");

            return group.ToString();
        }
    }