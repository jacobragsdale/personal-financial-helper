namespace PersonalFinancialHelper.Models;

public abstract class BaseModel
{
    protected DateTime StartDate { get; init; }
    protected DateTime EndDate { get; init; }

    protected List<double> TotalGain { get; } = new();
    protected List<double> TotalLoss { get; } = new();

    protected int GetTotalMonths()
    {
        return (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month;
    }

    public abstract void RunModel();
    public abstract double CalcTotalGain();
    public abstract double CalcTotalLoss();
}