namespace PersonalFinancialHelper.Models;

public abstract class BaseModel
{
    protected DateTime StartDate { get; init; }
    protected DateTime EndDate { get; init; }
    protected List<DateTime> CurrentDate { get; } = new();
    protected List<double> TotalGain { get; } = new();
    protected List<double> TotalLoss { get; } = new();

    protected BaseModel(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
        CurrentDate.Add(startDate);
    }

    protected int GetTotalMonths()
    {
        return (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month;
    }

    protected void IncrementCurrentDate()
    {
        CurrentDate.Add(CurrentDate[^1].AddMonths(1));
    }

    public abstract void RunModel();
    public abstract double CalcTotalGain();
    public abstract double CalcTotalLoss();
}