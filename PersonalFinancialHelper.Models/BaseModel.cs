namespace PersonalFinancialHelper.Models;

public class BaseModel
{
    protected DateTime StartDate { get; set; }
    protected DateTime EndDate { get; set; }

    protected int GetTotalMonths()
    {
        return (EndDate.Year - StartDate.Year) * 12 + EndDate.Month - StartDate.Month;;
    }
}