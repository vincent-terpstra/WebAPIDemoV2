using System.Collections;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemoV2.Tests.Expressions;

public class Expressions
{
    [Fact]
    public void Expression_Tree_Decomposition()
    {
        List<TestObject> filterList = new()
        {
            new() {Id = 1, Id2 = 3, Value = "One"},
            new() {Id = 2, Id2 = 4, Value = "Two"},
            new() {Id = 3, Id2 = 5, Value = "Three"}
        };

        var list = FilterOnList(filterList, t => t.Id);

        Assert.NotEmpty(list);
        var filtered = FilterOnContains(filterList, 
            t => t.Id, 
            t => t.Id2);
        
    }

    public IEnumerable<TKey> FilterOnList<TInner, TKey>(List<TInner> objects, Func<TInner, TKey> expression)
    {
        var selectMethod =
            typeof(Enumerable).GetMethods()
                .Where(method => method.Name == "Select" && 
                                 method.GetParameters()[1].ParameterType.Name == "Func`2");
        
        Assert.NotNull(selectMethod);
        Assert.Single(selectMethod);

        return objects.Select(expression);
    }
    public IEnumerable<TInner> FilterOnContains<TInner, TKey>(
        List<TInner> objects, 
        Expression<Func<TInner, TKey>> keySelector, 
        Expression<Func<TInner, TKey>> innerSelector)

    {
        var keys = objects.Select(keySelector.Compile());
        //var filtered = objects.Where(t => keys.Contains());

        return null;

    }    
}

public class TestObject
{
    public int Id { get; set; }
    public int Id2 { get; set; }
    public string Value { get; set; }
}

public class ExpressionTestObject
{
    public string Value { get; set; }
    public Guid Id { get; set; }
}