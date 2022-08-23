using PersonalFinancialHelper.Models;

namespace PersonalFinancialHelper;

public class Program
{
    public static void Main()
    {
        var mortgage = new MortgageModel(349900, 13000, 360, .04766, 176, 211, 190, 140);
        // mortgage.Print();
        var rent = new RentModel(2700, 0, 14, 12);
        // rent.Print();
        
        var loan = new LoanModel(349900, 13000, 360, .04766);
        // loan.Print();

        var portfolio = new InvestmentPortfolioModel(20000, 2000, 0.10, 36);
        portfolio.Print();
    }
}