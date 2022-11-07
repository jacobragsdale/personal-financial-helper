namespace PersonalFinancialHelper.Models;

public class LoanModel : BaseModel
{
    public LoanModel(DateTime startDate, DateTime endDate, int purchasePrice, int downPayment, double annualInterestRate) : base(startDate, endDate)
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

        RunModel();
    }

    private int PurchasePrice { get; }
    private int DownPayment { get; }
    private int LoanAmount { get; }
    private double AnnualAnnualInterestRate { get; }
    private double MonthlyInterestRate { get; }
    private IDictionary<DateTime, double> RemainingPrinciple { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalAmountPaid { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalPrinciplePaid { get; } = new Dictionary<DateTime, double>();
    private IDictionary<DateTime, double> TotalInterestPaid { get; } = new Dictionary<DateTime, double>();

    public void Print()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine(date);
            Console.WriteLine("Total Principle Paid:\t" + TotalPrinciplePaid[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Interest Paid:\t" + TotalInterestPaid[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Amount Paid:\t" + TotalAmountPaid[date].ToString("$#,##0.00"));
        }
    }

    public sealed override void RunModel()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            TotalAmountPaid.Add(date, TotalAmountPaid[date.AddMonths(-1)] + CalcMonthlyLoanPayment());
            RemainingPrinciple.Add(date, RemainingPrinciple[date.AddMonths(-1)] - CalcMonthlyPrinciplePayment(date.AddMonths(-1)));
            TotalPrinciplePaid.Add(date, TotalPrinciplePaid[date.AddMonths(-1)] + CalcMonthlyPrinciplePayment(date.AddMonths(-1)));
            TotalInterestPaid.Add(date, TotalInterestPaid[date.AddMonths(-1)] + CalcMonthlyInterestPayment(date.AddMonths(-1)));
        }
    }

    public override double GetTotalGain(DateTime date)
    {
        return 0.0;
    }
    
    public override double GetTotalLoss(DateTime date)
    {
        return CalcMonthlyInterestPayment(date);
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
}