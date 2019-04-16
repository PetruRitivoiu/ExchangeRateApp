namespace ExchangeRate.ViewModel.ViewObjects
{
    public class RateViewObject
    {
        public string Name { get; set; }
        public double Rate { get; set; }
        public string Description => this.ToString();

        public override string ToString()
        {
            return $"{Name}: {Rate}";
        }
    }
}
