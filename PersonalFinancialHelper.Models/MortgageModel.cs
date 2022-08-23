namespace PersonalFinancialHelper.Models;

//TODO:
// Calc Opportunity Costs
// Make some constants

public class MortgageModel : LoanModel
{
    private int PropertyTax { get; set; } //monthly
    private int HomeInsurance { get; set; } //monthly
    private int MortgageInsurance { get; set; } //monthly
    private int HomeOwnersAssociationFees { get; set; } //monthly
    private List<double> TotalAmountLost { get; set; } = new();
    private List<double> TotalFeesPaid { get; set; } = new();
    
    public MortgageModel(int purchasePrice, int downPayment, int loanTerm, double annualInterestRate, int propertyTax,
        int homeInsurance, int mortgageInsurance, int homeOwnersAssociationFees)
    {
        PurchasePrice = purchasePrice;
        DownPayment = downPayment;
        LoanAmount = PurchasePrice - DownPayment;
        LoanTerm = loanTerm;
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
    
    public new void Print()
    {
        for (var i = 0; i < LoanTerm; i++)
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
    
    private void RunModel()
    {
        for (var i = 0; i < LoanTerm; i++)
        {
            TotalPrinciplePaid.Add(TotalPrinciplePaid[^1] + CalcMonthlyPrinciplePayment());
            TotalInterestPaid.Add(TotalInterestPaid[^1] + CalcMonthlyInterestPayment());
            TotalFeesPaid.Add(TotalFeesPaid[^1] + CalcMonthlyFees());
            TotalAmountPaid.Add(TotalAmountPaid[^1] + CalcTotalMonthlyPayment());
            RemainingPrinciple.Add(RemainingPrinciple[^1] - CalcMonthlyPrinciplePayment());
            TotalAmountLost.Add(TotalAmountLost[^1] + CalcAmountLost());
            CurrentMonth += 1;
        }
    }

    public double CalcTotalMonthlyPayment()
    {
        return CalcMonthlyLoanPayment() + CalcMonthlyMaintenanceCosts() + HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance;
    }

    public double CalcAmountLost()
    {
        return CalcMonthlyMaintenanceCosts() + HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance + CalcMonthlyInterestPayment();
    }

    public double CalcMonthlyFees()
    {
        return HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance;
    }

    private double CalcMonthlyMaintenanceCosts()
    {
        // return PurchasePrice * 0.01 / 12;
        return 0;
    }
}