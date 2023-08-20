
using Utilitarios.escogerMesa;

namespace Utilitarios
{
    public class seleccionMesa
    {
        public Combination mesaEscoger(List<int> numbersInput, int target)
        {
            Logic logic = new Logic();
            Combination mesa = new Combination();
            List<Number> numbers = new List<Number>();
            List<Combination> combinations = new List<Combination>();

            numbers = numbersInput
        .Select((value, index) => new Number { Value = value, Index = index })
        .OrderBy(x => x.Value)
        .ToList();

            if(logic.SumIsLessThanTarget(numbersInput, target))
            {
                return mesa;
            }

            combinations = logic.VerificationSmallNumberItem(numbersInput, target);

            if(combinations.Count > 0)
            {
                combinations = combinations.OrderBy(x => x.Numbers.Count).ThenBy(x => x.TotalSum).ThenBy(x => x.TotalDiference).ToList();
                mesa = combinations.FirstOrDefault() ?? new Combination();
                return mesa;
            }

            numbers = numbers
        .Where(number => number.Value <= target)
        .ToList();

            combinations = logic.FindAllExactSumCombinations(numbers, target);
            if (combinations.Count == 0)
            {
                combinations = logic.FindAllCloseOrGreaterSumCombinations(numbers, target);
            }

            combinations = combinations
                .OrderBy(x => x.Numbers.Count)
                .ThenBy(x => x.TotalSum)
                .ThenBy(x => x.TotalDiference)
                .ToList();

            //combinations = combinations.OrderBy(x => x.Numbers.Count).ThenBy(x => x.TotalSum).ThenBy(x => x.TotalDiference).ToList();
            mesa = combinations.FirstOrDefault() ?? new Combination();
            return mesa;
        }
    }
}
