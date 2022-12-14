namespace PersonalFinancialHelper.Models;

public class RentModel : BaseModel
{
    private int MonthlyRent { get; }
    private int ParkingFee { get; }
    private int RentersInsurance { get; }

    public RentModel(DateTime startDate, DateTime endDate, int monthlyRent, int parkingFee, int rentersInsurance) : 
        base(startDate, endDate)
    {
        MonthlyRent = monthlyRent;
        ParkingFee = parkingFee;
        RentersInsurance = rentersInsurance;
        TotalGain.Add(StartDate, 0);
        TotalLoss.Add(StartDate, CalcTotalMonthlyPayment());

        RunModel();
    }
    
    public sealed override void RunModel()
    {
        for (var date = StartDate.AddMonths(1); date < EndDate; date = date.AddMonths(1))
        {
            TotalGain.Add(date, 0.0);
            TotalLoss.Add(date, TotalLoss[date.AddMonths(-1)] + CalcTotalMonthlyPayment());
        }
    }

    private int CalcTotalMonthlyPayment()
    {
        return MonthlyRent + ParkingFee + RentersInsurance;
    }
    
    public override void Print()
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