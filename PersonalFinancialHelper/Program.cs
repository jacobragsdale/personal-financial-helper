using PersonalFinancialHelper.Models;

namespace PersonalFinancialHelper;

/*
 * TODO:
     * Confirm all models are working as expected
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
        var endDate = new DateTime(2028, 1, 1);
        
        var mortgage = new MortgageModel(startDate, endDate, 349900, 13000,  .04766, 176, 211, 190, 140);
        // mortgage.Print();
        var rent = new RentModel(startDate, endDate, 2000, 0, 14);
        rent.Print();

        var loan = new LoanModel(startDate, endDate, 30000, 15000, .08766);
        // loan.Print();

        var portfolio = new InvestmentPortfolioModel(startDate, endDate, 20000, 2000, 0.10);
        // portfolio.Print();
    }
}