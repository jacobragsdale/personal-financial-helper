using PersonalFinancialHelper.Models;

namespace PersonalFinancialHelper;

/*
 * TODO:
     * Confirm all models are working as expected - 
        *  Mortgage can be cleaned up, TotalGain and TotalLoss can replace existing variables
     * Design Basket Model
     * Write changes needed to be made to Base Model
     * Re confirm all models are working
     * Build TUI
 * 
 * Nice to Haves:
     * Look to expand models (Car loan w/ insurance + parking).. Landlord?..  
     * Build API
     * Build Web UI
     * Build Web / Mobile Application 
 */
public static class Program
{
    public static void Main()
    {
        var startDate = new DateTime(2022, 1, 1);
        var endDate = new DateTime(2023, 1, 1);

        var models = new List<BaseModel>
        {
            new MortgageModel(startDate, endDate, 349900, 13000, .04766, 176, 211, 190, 140),
            new RentModel(startDate, endDate, 2000, 0, 14),
            new LoanModel(startDate, endDate, 30000, 15000, .08766),
            new InvestmentPortfolioModel(startDate, endDate, 20000, 2000, 0.10),
        };

        var basket = new BasketModel(startDate, endDate, models);
        Console.WriteLine("\n============================================");
        Console.WriteLine("============================================\n");
        Console.WriteLine("Total Gains:\t" + basket.GetTotalGain(endDate.AddMonths(-1)));
        Console.WriteLine("Total Losses:\t" + basket.GetTotalLoss(endDate.AddMonths(-1)));
    }
}