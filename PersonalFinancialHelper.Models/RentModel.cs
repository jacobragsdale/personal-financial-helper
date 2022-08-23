namespace PersonalFinancialHelper.Models;

public class RentModel
{
    private int MonthlyRent { get; set; }
    private int ParkingFee { get; set; }
    private int RentersInsurance { get; set; } //14.00

    private List<int> TotalAmountPaid { get; set; } = new();
    private int LeaseTerm { get; set; } //months

    public RentModel(int monthlyRent, int parkingFee, int rentersInsurance, int leaseTerm)
    {
        MonthlyRent = monthlyRent;
        ParkingFee = parkingFee;
        RentersInsurance = rentersInsurance;
        TotalAmountPaid.Add(monthlyRent);
        LeaseTerm = leaseTerm;
        
        RunModel();
    }
    
    public void Print()
    {
        for (var i = 0; i < LeaseTerm; i++)
        {
            Console.WriteLine("\n============================================\n");
            Console.WriteLine("Month " + i);
            Console.WriteLine("Total Amount Paid:\t" + TotalAmountPaid[i].ToString("$#,##0.00"));
        }
    }

    private void RunModel()
    {
        for (var i = 0; i < LeaseTerm; i++)
        {
            TotalAmountPaid.Add(TotalAmountPaid[^1] + CalcTotalMonthlyPayment());
        }
    }

    private int CalcTotalMonthlyPayment()
    {
        return MonthlyRent + ParkingFee + RentersInsurance;
    }
    
}