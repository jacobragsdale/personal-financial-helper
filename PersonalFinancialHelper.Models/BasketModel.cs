namespace PersonalFinancialHelper.Models;

/*
 * What if for every model type we store a list of that model type
 * and each instance is a "state" of the model that has a timestamp
 *
 * We have to figure out how to keep each model in sync with the basket
 * let the basket run over a certain irl timeframe and start each model as we get to its start date
 */
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

    public sealed override double CalcTotalGain()
    {
        return _models.Sum(model => model.CalcTotalGain());
    }

    public sealed override double CalcTotalLoss()
    {
        return _models.Sum(model => model.CalcTotalLoss());
    }
}