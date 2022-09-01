using PersonalFinancialHelper.Models;

namespace PersonalFinancialHelper;

public static class Program
{
    public static void Main()
    {
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2025, 1, 1);
        
        var mortgage = new MortgageModel(startDate, endDate, 349900, 13000,  .04766, 176, 211, 190, 140);
        // mortgage.Print();
        var rent = new RentModel(startDate, endDate, 2700, 0, 14);
        // rent.Print();

        var loan = new LoanModel(startDate, endDate, 349900, 13000, .04766);
        // loan.Print();

        var portfolio = new InvestmentPortfolioModel(startDate, endDate, 20000, 2000, 0.10);
        portfolio.Print();
    }
}