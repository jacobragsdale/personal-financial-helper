namespace PersonalFinancialHelper.Models;

public class RentModel : BaseModel
{
    private int MonthlyRent { get; }
    private int ParkingFee { get; }
    private int RentersInsurance { get; }
    private IDictionary<DateTime, double> TotalAmountPaid { get; } = new Dictionary<DateTime, double>();


    public RentModel(DateTime startDate, DateTime endDate, int monthlyRent, int parkingFee, int rentersInsurance) : 
        base(startDate, endDate)
    {
        MonthlyRent = monthlyRent;
        ParkingFee = parkingFee;
        RentersInsurance = rentersInsurance;
        TotalAmountPaid.Add(StartDate, CalcTotalMonthlyPayment());
        TotalGain.Add(StartDate, 0);
        TotalLoss.Add(StartDate, CalcTotalMonthlyPayment());

        RunModel();
    }
    
    public sealed override void RunModel()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            TotalAmountPaid.Add(date, TotalAmountPaid[date.AddMonths(-1)] + CalcTotalMonthlyPayment());
            TotalGain.Add(date, TotalGain[date.AddMonths(-1)] + GetTotalGain(date.AddMonths(-1)));
            TotalLoss.Add(date, TotalLoss[date.AddMonths(-1)] + GetTotalLoss(date.AddMonths(-1)));
        }
    }

    private int CalcTotalMonthlyPayment()
    {
        return MonthlyRent + ParkingFee + RentersInsurance;
    }
    
    public void Print()
    {
        for (var date = StartDate; date < EndDate; date = date.AddMonths(1))
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine(date);
            Console.WriteLine("Total Gain:\t" + TotalGain[date].ToString("$#,##0.00"));
            Console.WriteLine("Total Loss:\t" + TotalLoss[date.Date].ToString("$#,##0.00"));
        }
    }
}