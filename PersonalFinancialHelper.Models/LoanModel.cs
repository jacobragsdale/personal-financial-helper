namespace PersonalFinancialHelper.Models;

public class LoanModel : BaseModel
{
    protected LoanModel()
    {
    }

    public LoanModel(DateTime startDate, DateTime endDate, int purchasePrice, int downPayment, double annualInterestRate)
    {
        StartDate = startDate;
        EndDate = endDate;
        PurchasePrice = purchasePrice;
        DownPayment = downPayment;
        LoanAmount = PurchasePrice - DownPayment;
        AnnualAnnualInterestRate = annualInterestRate;
        MonthlyInterestRate = AnnualAnnualInterestRate / 12;

        RemainingPrinciple.Add(PurchasePrice - DownPayment);
        TotalAmountPaid.Add(DownPayment);
        TotalPrinciplePaid.Add(DownPayment);
        TotalInterestPaid.Add(0.0);

        RunModel();
    }

    protected int PurchasePrice { get; set; }
    protected int DownPayment { get; set; }
    protected int LoanAmount { get; set; }
    protected double AnnualAnnualInterestRate { get; set; }
    protected double MonthlyInterestRate { get; set; }
    protected List<double> RemainingPrinciple { get; } = new();
    protected List<double> TotalAmountPaid { get; } = new();
    protected List<double> TotalPrinciplePaid { get; } = new();
    protected List<double> TotalInterestPaid { get; } = new();

    public void Print()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine("Month " + i);
            Console.WriteLine("Total Principle Paid:\t" + TotalPrinciplePaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Interest Paid:\t" + TotalInterestPaid[i].ToString("$#,##0.00"));
            Console.WriteLine("Total Amount Paid:\t" + TotalAmountPaid[i].ToString("$#,##0.00"));
        }
    }

    public override void RunModel()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            TotalAmountPaid.Add(TotalAmountPaid[^1] + CalcMonthlyLoanPayment());
            TotalPrinciplePaid.Add(TotalPrinciplePaid[^1] + CalcMonthlyPrinciplePayment());
            TotalInterestPaid.Add(TotalInterestPaid[^1] + CalcMonthlyInterestPayment());
            RemainingPrinciple.Add(RemainingPrinciple[^1] - CalcMonthlyPrinciplePayment());
        }
    }

    public override double CalcTotalGain()
    {
        throw new NotImplementedException();
    }

    public override double CalcTotalLoss()
    {
        throw new NotImplementedException();
    }

    protected double CalcMonthlyInterestPayment()
    {
        return RemainingPrinciple[^1] * MonthlyInterestRate;
    }

    protected double CalcMonthlyPrinciplePayment()
    {
        return CalcMonthlyLoanPayment() - CalcMonthlyInterestPayment();
    }

    protected double CalcMonthlyLoanPayment()
    {
        return LoanAmount * MonthlyInterestRate * Math.Pow(1 + MonthlyInterestRate, GetTotalMonths()) /
               (Math.Pow(1 + MonthlyInterestRate, GetTotalMonths()) - 1);
    }
}