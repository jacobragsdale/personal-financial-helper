namespace PersonalFinancialHelper.Models;

public class RentModel : BaseModel
{
    public RentModel(DateTime startDate, DateTime endDate, int monthlyRent, int parkingFee, int rentersInsurance)
    {
        StartDate = startDate;
        EndDate = endDate;
        MonthlyRent = monthlyRent;
        ParkingFee = parkingFee;
        RentersInsurance = rentersInsurance;
        TotalAmountPaid.Add(monthlyRent);

        RunModel();
    }

    private int MonthlyRent { get; }
    private int ParkingFee { get; }
    private int RentersInsurance { get; } //14.00
    private List<int> TotalAmountPaid { get; } = new();

    public void Print()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine("Month " + i);
            Console.WriteLine("Total Amount Paid:\t" + TotalAmountPaid[i].ToString("$#,##0.00"));
        }
    }

    public sealed override double CalcTotalGain()
    {
        return 0.0;
    }

    public sealed override double CalcTotalLoss()
    {
        return CalcTotalMonthlyPayment();
    }

    public sealed override void RunModel()
    {
        for (var i = 0; i < GetTotalMonths(); i++)
        {
            TotalAmountPaid.Add(TotalAmountPaid[^1] + CalcTotalMonthlyPayment());
            TotalGain.Add(TotalGain[^1] + CalcTotalGain());
            TotalLoss.Add(TotalLoss[^1] + CalcTotalLoss());
        }
    }

    private int CalcTotalMonthlyPayment()
    {
        return MonthlyRent + ParkingFee + RentersInsurance;
    }
}