namespace PersonalFinancialHelper.Models;

public class InvestmentPortfolioModel : BaseModel
{
    private int InitialInvestment { get; }
    private int MonthlyDeposit { get; }
    private double ExpectedAnnualReturn { get; }
    private IDictionary<DateTime, double> TotalValue { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalAmountInvested { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalReturn { get; } = new Dictionary<DateTime, double>();
    
    public InvestmentPortfolioModel(DateTime startDate, DateTime endDate, int initialInvestment, int monthlyDeposit, double expectedAnnualReturn)
    : base(startDate, endDate)
    {
        InitialInvestment = initialInvestment;
        MonthlyDeposit = monthlyDeposit;
        ExpectedAnnualReturn = expectedAnnualReturn;

        TotalValue.Add(StartDate, InitialInvestment);
        TotalAmountInvested.Add(StartDate, InitialInvestment);
        TotalReturn.Add(StartDate, 0.0);
        TotalGain.Add(StartDate, 0.0);
        TotalLoss.Add(StartDate, 0.0);

        RunModel();
    }

    public sealed override void RunModel()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            TotalAmountInvested.Add(date, CalcTotalAmountInvested(date.AddMonths(-1)));
            TotalValue.Add(date, CalcTotalValue(date.AddMonths(-1)));
            TotalReturn.Add(date, CalcTotalReturn(date.AddMonths(-1)));
            TotalGain.Add(date, TotalReturn[date]);
            TotalLoss.Add(date, 0.0);
        }
    }

    private double CalcTotalAmountInvested(DateTime date)
    {
        return TotalAmountInvested[date] + MonthlyDeposit;
    }

    private double CalcTotalValue(DateTime date)
    {
        return TotalValue[date] + TotalValue[date] * (ExpectedAnnualReturn / 12) + MonthlyDeposit;
    }

    private double CalcTotalReturn(DateTime date)
    {
        return TotalValue[date] - TotalAmountInvested[date];
    }
    
    public void Print()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine(date);
            Console.WriteLine("Total Amount Invested:\t" + TotalAmountInvested[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Value:\t" + TotalValue[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Return:\t" + TotalReturn[date].ToString("$#,##0.00"));
        }
    }
}