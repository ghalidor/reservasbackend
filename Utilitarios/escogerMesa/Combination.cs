
namespace Utilitarios.escogerMesa
{
    public class Combination
    {
        public List<Number> Numbers { get; set; } = new List<Number>();
        public int TotalSum { get => Numbers.Sum(x => x.Value); }
        public int TotalDiference { get => CalculateDifferencesSum(); }

        int CalculateDifferencesSum()
        {
            int sum = 0;

            for(int i = 0; i < Numbers.Count - 1; i++)
            {
                sum += Math.Abs(Numbers[i + 1].Value - Numbers[i].Value);
            }

            return sum;
        }
    }
}
