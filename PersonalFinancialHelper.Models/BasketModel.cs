namespace PersonalFinancialHelper.Models;

public class BasketModel : BaseModel
{
    private readonly List<BaseModel> _models = new();

    public BasketModel(DateTime startDate, DateTime endDate, IEnumerable<BaseModel> models) : base(startDate, endDate)
    {
        _models.AddRange(models);
    }

    public sealed override void RunModel()
    {
        throw new NotImplementedException();
    }

    public new double GetTotalGain(DateTime date)
    {
        return _models.Sum(model => model.GetTotalGain(date));
    }

    public new double GetTotalLoss(DateTime date)
    {
        return _models.Sum(model => model.GetTotalLoss(date));
    }

    public override void Print()
    {
        throw new NotImplementedException();
    }
}