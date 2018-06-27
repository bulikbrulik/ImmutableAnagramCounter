using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecursiveAnagramCounter
{
    class Program
    {
        public static int NumberOfChangesToAnagram(string a, string b)
        {
            if (a.Length != b.Length) return -1;

            Dictionary<char, int> A = a.GroupBy(c => c).Select(t => new { Character = t.Key, Count = t.Count() }).ToDictionary(t => t.Character, t => t.Count);
            Dictionary<char, int> B = b.GroupBy(c => c).Select(t => new { Character = t.Key, Count = t.Count() }).ToDictionary(t => t.Character, t => t.Count);

            return CalculateChanges(A, B, 0);
        }

        public static int CalculateChanges(Dictionary<char, int> A, Dictionary<char, int> B, int i)
        {
            if (i == A.Count()) return 0;

            int countA, countB;
            countA = A[A.Keys.ElementAt(i)];
            if (B.TryGetValue(A.Keys.ElementAt(i), out countB) && countA <= countB)
                return CalculateChanges(A, B, i + 1);
            else
            {
                return (CalculateChanges(A, B, i + 1) + (countA-countB));
            }
        }

        public static int[] ProcessInputs(ReadOnlyCollection<Tuple<string, string>> inputs)
        {
            return inputs.Select(t => NumberOfChangesToAnagram(t.Item1, t.Item2)).ToArray();
        }

        public static void PrintOutputs(int[] outputs, ReadOnlyCollection<Tuple<string, string>> inputs, int i)
        {
            if (i == outputs.Count()) return;
            Console.WriteLine(inputs[i].Item1 + " : " + inputs[i].Item2 + " = " + outputs[i]);
            PrintOutputs(outputs, inputs, i + 1);
        }

        public static void Main(string[] args)
        {
            Tuple<string, string>[] inputs = {
                Tuple.Create("aab", "abca")
                ,Tuple.Create("aaba", "abca")
                ,Tuple.Create("justin", "jjjjjj")
                ,Tuple.Create("jjjjjj", "jjaaaa")
                ,Tuple.Create("jjjjjj", "aaaaaa")};

            var readOnlyInputs = new ReadOnlyCollection<Tuple<string, string>>(inputs);

            int[] outputs = ProcessInputs(readOnlyInputs);

            PrintOutputs(outputs, readOnlyInputs, 0);

            Console.ReadLine();
        }
    }
}
