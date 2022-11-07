namespace PersonalFinancialHelper.Models;

public abstract class BaseModel
{
    protected DateTime StartDate { get; }
    protected DateTime EndDate { get; }
    protected IDictionary<DateTime, double> TotalGain { get; } = new Dictionary<DateTime, double>();
    protected IDictionary<DateTime, double> TotalLoss { get; } = new Dictionary<DateTime, double>();

    protected BaseModel(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    protected int GetTotalMonths()
    {
        return (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month;
    }

    public abstract void RunModel();
    public abstract double GetTotalGain(DateTime date);
    public abstract double GetTotalLoss(DateTime date);
}