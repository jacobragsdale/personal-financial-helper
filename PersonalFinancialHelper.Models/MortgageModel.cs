namespace PersonalFinancialHelper.Models;

//TODO:
// Calc Opportunity Costs
// Make some constants
//

public class MortgageModel : BaseModel
{
    public MortgageModel(DateTime startDate, DateTime endDate, int purchasePrice, int downPayment,
        double annualInterestRate, int propertyTax,
        int homeInsurance, int mortgageInsurance, int homeOwnersAssociationFees) : base(startDate, endDate)
    {
        PurchasePrice = purchasePrice;
        DownPayment = downPayment;
        LoanAmount = PurchasePrice - DownPayment;
        AnnualAnnualInterestRate = annualInterestRate;
        MonthlyInterestRate = AnnualAnnualInterestRate / 12;

        RemainingPrinciple.Add(PurchasePrice - DownPayment);
        TotalAmountPaid.Add(DownPayment);
        TotalPrinciplePaid.Add(DownPayment);
        TotalInterestPaid.Add(0.0);
        PropertyTax = propertyTax;
        HomeInsurance = homeInsurance;
        MortgageInsurance = mortgageInsurance;
        HomeOwnersAssociationFees = homeOwnersAssociationFees;

        TotalFeesPaid.Add(0.0);
        TotalAmountLost.Add(0.0);

        RunModel();
    }

    private List<double> TotalInterestPaid { get; } = new();
    private int PurchasePrice { get; init; }
    private int DownPayment { get; init; }
    private double LoanAmount { get; init; }
    private double AnnualAnnualInterestRate { get; init; }
    private double MonthlyInterestRate { get; init; }
    private List<double> RemainingPrinciple { get; } = new();
    private List<double> TotalPrinciplePaid { get; } = new();
    private List<double> TotalAmountPaid { get; } = new();
    private int PropertyTax { get; } //monthly
    private int HomeInsurance { get; } //monthly
    private int MortgageInsurance { get; } //monthly
    private int HomeOwnersAssociationFees { get; } //monthly
    private List<double> TotalAmountLost { get; } = new();
    private List<double> TotalFeesPaid { get; } = new();

    public sealed override void RunModel()
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

    public override double GetTotalGain(DateTime date)
    {
        throw new NotImplementedException();
    }

    public override double GetTotalLoss(DateTime date)
    {
        throw new NotImplementedException();
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

    private double CalcMonthlyInterestPayment()
    {
        return RemainingPrinciple[^1] * MonthlyInterestRate;
    }

    private double CalcMonthlyPrinciplePayment()
    {
        return CalcMonthlyLoanPayment() - CalcMonthlyInterestPayment();
    }

    private double CalcMonthlyLoanPayment()
    {
        return LoanAmount * MonthlyInterestRate * Math.Pow(1 + MonthlyInterestRate, GetTotalMonths()) /
               (Math.Pow(1 + MonthlyInterestRate, GetTotalMonths()) - 1);
    }

    public void Print()
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