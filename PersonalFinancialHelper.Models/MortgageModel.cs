namespace PersonalFinancialHelper.Models;

public class MortgageModel : BaseModel
{
    private IDictionary<DateTime, double> TotalInterestPaid { get; } = new Dictionary<DateTime, double>();
    private int PurchasePrice { get; }
    private int DownPayment { get; }
    private double LoanAmount { get; }
    private double AnnualAnnualInterestRate { get; }
    private double MonthlyInterestRate { get; }
    private IDictionary<DateTime, double> RemainingPrinciple { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalPrinciplePaid { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalAmountPaid { get; } = new Dictionary<DateTime, double>();
    private int PropertyTax { get; } //monthly
    private int HomeInsurance { get; } //monthly
    private int MortgageInsurance { get; } //monthly
    private int HomeOwnersAssociationFees { get; } //monthly
    private IDictionary<DateTime, double> TotalAmountLost { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalFeesPaid { get; } = new Dictionary<DateTime, double>();
    
    public MortgageModel(DateTime startDate, DateTime endDate, int purchasePrice, int downPayment,
        double annualInterestRate, int propertyTax,
        int homeInsurance, int mortgageInsurance, int homeOwnersAssociationFees) : base(startDate, endDate)
    {
        PurchasePrice = purchasePrice;
        DownPayment = downPayment;
        LoanAmount = PurchasePrice - DownPayment;
        AnnualAnnualInterestRate = annualInterestRate;
        MonthlyInterestRate = AnnualAnnualInterestRate / 12;

        RemainingPrinciple.Add(StartDate, PurchasePrice - DownPayment);
        TotalAmountPaid.Add(StartDate, DownPayment);
        TotalPrinciplePaid.Add(StartDate, DownPayment);
        TotalInterestPaid.Add(StartDate, 0.0);
        PropertyTax = propertyTax;
        HomeInsurance = homeInsurance;
        MortgageInsurance = mortgageInsurance;
        HomeOwnersAssociationFees = homeOwnersAssociationFees;

        TotalFeesPaid.Add(StartDate, 0.0);
        TotalAmountLost.Add(StartDate, 0.0);
        TotalGain.Add(StartDate, 0.0);
        TotalLoss.Add(StartDate, 0.0);

        RunModel();
    }
    
    public sealed override void RunModel()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            RemainingPrinciple.Add(date, RemainingPrinciple[date.AddMonths(-1)] - CalcMonthlyPrinciplePayment(date.AddMonths(-1)));
            TotalInterestPaid.Add(date, TotalInterestPaid[date.AddMonths(-1)] + CalcMonthlyInterestPayment(date.AddMonths(-1)));
            TotalPrinciplePaid.Add(date, CalcMonthlyPrinciplePayment(date.AddMonths(-1)));
            TotalFeesPaid.Add(date, TotalFeesPaid[date.AddMonths(-1)] + CalcMonthlyFees());
            TotalAmountPaid.Add(date, TotalAmountPaid[date.AddMonths(-1)] + CalcTotalMonthlyPayment());
            TotalAmountLost.Add(date, TotalAmountLost[date.AddMonths(-1)] + CalcAmountLost(date.AddMonths(-1)));
            TotalGain.Add(date, 0.0);
            TotalLoss.Add(date, TotalAmountLost[date]);
        }
    }

    private double CalcTotalMonthlyPayment()
    {
        return CalcMonthlyLoanPayment() + CalcMonthlyMaintenanceCosts() + HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance;
    }

    private double CalcAmountLost(DateTime date)
    {
        return CalcMonthlyMaintenanceCosts() + HomeInsurance + PropertyTax +
               HomeOwnersAssociationFees + MortgageInsurance + CalcMonthlyInterestPayment(date);
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

    private double CalcMonthlyInterestPayment(DateTime date)
    {
        return RemainingPrinciple[date] * MonthlyInterestRate;
    }

    private double CalcMonthlyPrinciplePayment(DateTime date)
    {
        return CalcMonthlyLoanPayment() - CalcMonthlyInterestPayment(date);
    }

    private double CalcMonthlyLoanPayment()
    {
        return LoanAmount * MonthlyInterestRate * Math.Pow(1 + MonthlyInterestRate, GetTotalMonths()) /
               (Math.Pow(1 + MonthlyInterestRate, GetTotalMonths()) - 1);
    }

    public void Print()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine(date);
            Console.WriteLine("Total Principle Paid:\t" + TotalPrinciplePaid[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Interest Paid:\t" + TotalInterestPaid[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Fees Paid:\t" + TotalFeesPaid[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Amount Paid:\t" + TotalAmountPaid[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Amount Lost:\t" + TotalAmountLost[date].ToString("$#,##0.00"));
        }
    }
}