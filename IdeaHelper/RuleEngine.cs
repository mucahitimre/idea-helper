namespace IdeaHelper;

public class RuleEngine<TModel>
    where TModel : class, new()
{
    private readonly Dictionary<Predicate<TModel>, Action<TModel>> _rules;

    public RuleEngine()
    {
        _rules = new Dictionary<Predicate<TModel>, Action<TModel>>();
    }

    public void Add(Predicate<TModel> predicate, Action<TModel> action)
    {
        _rules.Add(predicate, action);
    }

    public void RunRule(TModel model)
    {
        foreach (var item in _rules)
        {
            if (item.Key(model))
            {
                item.Value(model);
            }
        }
    }
}