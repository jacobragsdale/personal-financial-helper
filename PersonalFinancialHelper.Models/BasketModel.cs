namespace PersonalFinancialHelper.Models;

public class BasketModel : BaseModel
{
    private readonly List<BaseModel> _models = new();

    public BasketModel(DateTime startDate, DateTime endDate, IEnumerable<BaseModel> models) : base(startDate, endDate)
    {
        _models.AddRange(models);
        RunModel();
    }
    
    public void AddToBasket(BaseModel model)
    {
        _models.Add(model);
    }

    public sealed override void RunModel()
    {
        foreach (var item in _models)
        {
            item.RunModel();
        }
    }

    public sealed override double GetTotalGain(DateTime date)
    {
        return _models.Sum(model => model.GetTotalGain(date));
    }

    public sealed override double GetTotalLoss(DateTime date)
    {
        return _models.Sum(model => model.GetTotalLoss(date));
    }
}