namespace PersonalFinancialHelper.Models;

//TODO:
// Calc Opportunity Costs
// Make some constants
// 

public class MortgageModel : LoanModel
{
    public MortgageModel(DateTime startDate, DateTime endDate, int purchasePrice, int downPayment, double annualInterestRate, int propertyTax,
        int homeInsurance, int mortgageInsurance, int homeOwnersAssociationFees)
    {
        StartDate = startDate;
        EndDate = endDate;
        PurchasePrice = purchasePrice;
        DownPayment = downPayment;
        LoanAmount = PurchasePrice - DownPayment;
        AnnualAnnualInterestRate = annualInterestRate;
        MonthlyInterestRate = AnnualAnnualInterestRate / 12;
        PropertyTax = propertyTax;
        HomeInsurance = homeInsurance;
        MortgageInsurance = mortgageInsurance;
        HomeOwnersAssociationFees = homeOwnersAssociationFees;

        RemainingPrinciple.Add(PurchasePrice - DownPayment);
        TotalAmountPaid.Add(DownPayment);
        TotalPrinciplePaid.Add(DownPayment);
        TotalFeesPaid.Add(0.0);
        TotalInterestPaid.Add(0.0);
        TotalAmountLost.Add(0.0);

        RunModel();
    }

    private int PropertyTax { get; } //monthly
    private int HomeInsurance { get; } //monthly
    private int MortgageInsurance { get; } //monthly
    private int HomeOwnersAssociationFees { get; } //monthly
    private List<double> TotalAmountLost { get; } = new();
    private List<double> TotalFeesPaid { get; } = new();

    private void RunModel()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            TotalPrinciplePaid.Add(CalcMonthlyPrinciplePayment());
            TotalInterestPaid.Add(TotalInterestPaid[^1] + CalcMonthlyInterestPayment());
            TotalFeesPaid.Add(TotalFeesPaid[^1] + CalcMonthlyFees());
            TotalAmountPaid.Add(TotalAmountPaid[^1] + CalcTotalMonthlyPayment());
            RemainingPrinciple.Add(RemainingPrinciple[^1] - CalcMonthlyPrinciplePayment());
            TotalAmountLost.Add(TotalAmountLost[^1] + CalcAmountLost());
        }
    }

    private double CalcTotalMonthlyPayment()
    {
        return CalcMonthlyLoanPayment() + CalcMonthlyMaintenanceCosts() + HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance;
    }

    private double CalcAmountLost()
    {
        return CalcMonthlyMaintenanceCosts() + HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance + CalcMonthlyInterestPayment();
    }

    private double CalcMonthlyFees()
    {
        return HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance;
    }

    private double CalcMonthlyMaintenanceCosts()
    {
        return PurchasePrice * 0.01 / 12;
    }
    
    public new void Print()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine("Month " + i);
            Console.WriteLine("Total Principle Paid:\t" + TotalPrinciplePaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Interest Paid:\t" + TotalInterestPaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Fees Paid:\t" + TotalFeesPaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Amount Paid:\t" + TotalAmountPaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Amount Lost:\t" + TotalAmountLost[i].ToString("$#,##0.00"));
        }
    }
}