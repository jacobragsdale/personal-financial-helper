namespace PersonalFinancialHelper.Models;

public abstract class BaseModel
{
    protected DateTime StartDate { get; set; }
    protected DateTime EndDate { get; set; }

    protected List<double> TotalGain { get; set; } = new();
    protected List<double> TotalLoss { get; set; } = new();

    protected int GetTotalMonths()
    {
        return (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month;;
    }

    public abstract void RunModel();
    public abstract double CalcTotalGain();
    public abstract double CalcTotalLoss();
}