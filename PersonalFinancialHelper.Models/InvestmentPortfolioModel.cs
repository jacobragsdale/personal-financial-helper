namespace PersonalFinancialHelper.Models;

public class InvestmentPortfolioModel
{
    private int InitialInvestment { get; set; }
    private int MonthlyDeposit { get; set; }
    private double ExpectedReturnRate { get; set; }
    private int MonthsToHold { get; set; }
    private List<double> TotalValue { get; set; } = new();
    private List<double> TotalAmountInvested { get; set; } = new();
    private List<double> TotalReturn { get; set; } = new();


    public InvestmentPortfolioModel(int initialInvestment, int monthlyDeposit, double expectedReturnRate,
        int monthsToHold)
    {
        InitialInvestment = initialInvestment;
        MonthlyDeposit = monthlyDeposit;
        ExpectedReturnRate = expectedReturnRate;
        MonthsToHold = monthsToHold;
        
        TotalValue.Add(InitialInvestment);
        TotalAmountInvested.Add(InitialInvestment);
        TotalReturn.Add(0.0);
        
        RunModel();
    }

    public void Print()
    {
        for (var i = 0; i < MonthsToHold; i++)
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine("Month " + i);
            Console.WriteLine("Total Amount Invested:\t" + TotalAmountInvested[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Value:\t" + TotalValue[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Return:\t" + TotalReturn[i].ToString("$#,##0.00"));
        }
    }

    private void RunModel()
    {
        for (var i = 0; i < MonthsToHold; i++)
        {
            TotalAmountInvested.Add(TotalAmountInvested[^1] + MonthlyDeposit);
            TotalValue.Add(TotalValue[^1] * ExpectedReturnRate + TotalAmountInvested[^1] + MonthlyDeposit);
            TotalReturn.Add(TotalValue[^1] - TotalAmountInvested[^1]);
        }
    }

}