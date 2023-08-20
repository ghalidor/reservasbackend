
namespace Utilitarios.escogerMesa
{
    public class Logic
    {
        public List<Combination> FindAllCombinations(List<Number> numbers)
        {
            List<Combination> combinations = new List<Combination>();

            // Calculate the number of combinations
            int combinationsQuantity = (int)(Math.Pow(2, numbers.Count));

            // Loop all combinations
            for(int i = 1; i < combinationsQuantity; i++)
            {
                List<Number> numberList = new List<Number>();

                // Loop all numbers
                for(int j = 0; j < numbers.Count; j++)
                {
                    // Add the number and the index
                    if(((i & (1 << j)) >> j) == 1)
                    {
                        numberList.Add(numbers[j]);
                    }
                }

                combinations.Add(new Combination()
                {
                    Numbers = numberList
                });
            }
            return combinations;
        }

        public List<Combination> FindAllCloseOrGreaterSumCombinations(List<Number> numbers, int target)
        {
            List<Combination> allSubsets = new List<Combination>();
            List<Number> currentSubset = new List<Number>();

            void SubsetSum(int currentIndex, int currentSum)
            {
                if(currentSum >= target)
                {
                    allSubsets.Add(new Combination { Numbers = new List<Number>(currentSubset) });
                }

                if(currentIndex >= numbers.Count || currentSum > target)
                {
                    return;
                }

                currentSubset.Add(numbers[currentIndex]);
                SubsetSum(currentIndex + 1, currentSum + numbers[currentIndex].Value);

                currentSubset.RemoveAt(currentSubset.Count - 1);
                SubsetSum(currentIndex + 1, currentSum);
            }

            SubsetSum(0, 0);
            return allSubsets;
        }

        public List<Combination> FindAllExactSumCombinations(List<Number> numbers, int target)
        {
            List<Combination> allSubsets = new List<Combination>();
            List<Number> currentSubset = new List<Number>();

            void SubsetSum(int currentIndex, int currentSum)
            {
                if(currentSum == target)
                {
                    allSubsets.Add(new Combination { Numbers = new List<Number>(currentSubset) });
                    return;
                }

                if(currentIndex >= numbers.Count || currentSum > target)
                {
                    return;
                }

                currentSubset.Add(numbers[currentIndex]);
                SubsetSum(currentIndex + 1, currentSum + numbers[currentIndex].Value);

                currentSubset.RemoveAt(currentSubset.Count - 1);
                SubsetSum(currentIndex + 1, currentSum);
            }

            SubsetSum(0, 0);
            return allSubsets;
        }

        public List<Combination> VerificationSmallNumberItem(List<int> numbers, int target)
        {

            List<Combination> combinations = new List<Combination>();

            bool hasSmallerNumbers = numbers.Any(x => x <= target);

            if(!hasSmallerNumbers)
            {

                var number = numbers.Select((value, index) => new Number { Value = value, Index = index }).OrderBy(x => x.Value).FirstOrDefault() ?? new Number();
                combinations.Add(new Combination()
                {
                    Numbers = new List<Number>() { number }
                });
            }

            return combinations;
        }

        public bool SumIsLessThanTarget(List<int> numbers, int target)
        {
            return numbers.Sum() < target;
        }
    }
}
