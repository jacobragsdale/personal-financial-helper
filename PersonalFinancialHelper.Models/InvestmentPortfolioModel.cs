namespace PersonalFinancialHelper.Models;

public class InvestmentPortfolioModel : BaseModel
{
    public InvestmentPortfolioModel(DateTime startDate, DateTime endDate, int initialInvestment, int monthlyDeposit, double expectedAnnualReturn)
    {
        StartDate = startDate;
        EndDate = endDate;
        InitialInvestment = initialInvestment;
        MonthlyDeposit = monthlyDeposit;
        ExpectedAnnualReturn = expectedAnnualReturn;

        TotalValue.Add(InitialInvestment);
        TotalAmountInvested.Add(InitialInvestment);
        TotalReturn.Add(0.0);

        RunModel();
    }

    private int InitialInvestment { get; }
    private int MonthlyDeposit { get; }
    private double ExpectedAnnualReturn { get; }
    private List<double> TotalValue { get; } = new();
    private List<double> TotalAmountInvested { get; } = new();
    private List<double> TotalReturn { get; } = new();
    
    public sealed override double CalcTotalGain()
    {
        return 0.0;
    }

    public sealed override double CalcTotalLoss()
    {
        return CalcTotalValue();
    }

    public void Print()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine("Month " + i);
            Console.WriteLine("Total Amount Invested:\t" + TotalAmountInvested[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Value:\t" + TotalValue[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Return:\t" + TotalReturn[i].ToString("$#,##0.00"));
        }
    }

    public sealed override void RunModel()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            TotalAmountInvested.Add(CalcTotalAmountInvested());
            TotalValue.Add(CalcTotalValue());
            TotalReturn.Add(CalcTotalReturn());
        }
    }

    private double CalcTotalAmountInvested()
    {
        return TotalAmountInvested[^1] + MonthlyDeposit;
    }

    private double CalcTotalValue()
    {
        return TotalValue[^1] + TotalValue[^1] * (ExpectedAnnualReturn / 12) + MonthlyDeposit;
    }

    private double CalcTotalReturn()
    {
        return TotalValue[^1] - TotalAmountInvested[^1];
    }
}