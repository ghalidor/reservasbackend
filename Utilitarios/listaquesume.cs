using Domain;

namespace Utilitarios
{
    public class listaquesume
    {
        //public static List<Reservas> getcombinations(List<Reservas> source)
        //{
        //    for (var i = 0; i < (1 << source.Count()); i++)
        //        yield return source
        //           .Where((t, j) => (i & (1 << j)) != 0)
        //           .ToList();
        //}
    }

    public class OutputNumbers
    {
        public List<int[]> Indexs { get; set; } = new List<int[]>();
        public List<int[]> Numbers { get; set; } = new List<int[]>();
        public int Target { get; set; } = 0;
    }

    public class NumbersCombinations
    {
        public OutputNumbers SumCombinations(List<int> numbers, int targetSum)
        {
            // Create lists
            OutputNumbers outputNumbers = new OutputNumbers();
            List<int[]> listOutputIndexes = new List<int[]>();
            List<int[]> listOutputNumbers = new List<int[]>();

            // Calculate the number of combinations
            int combinations = (int)(Math.Pow(2, numbers.Count) - 1);

            // Loop all combinations
            for (int i = 0; i < combinations; i++)
            {
                // Create subset lists
                List<int> subset = new List<int>();
                List<int> subindexes = new List<int>();

                // Loop all numbers
                for (int j = 0; j < numbers.Count; j++)
                {
                    // Add the number and the index
                    if (((i & (1 << j)) >> j) == 1)
                    {
                        subset.Add(numbers[j]);
                        subindexes.Add(j);
                    }
                }

                // Check if the target sum has been reached
                if (subset.Sum() == targetSum)
                {
                    // Add a combination
                    listOutputIndexes.Add(subindexes.ToArray());
                    listOutputNumbers.Add(subset.ToArray());

                    // Set OutputNumbers
                    outputNumbers.Indexs = listOutputIndexes;
                    outputNumbers.Numbers = listOutputNumbers;
                    outputNumbers.Target = targetSum;
                }
            }

            return outputNumbers;
        }
    }
}