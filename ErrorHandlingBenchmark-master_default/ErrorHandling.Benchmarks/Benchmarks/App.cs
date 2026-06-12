using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using ErrorHandling.Core.Strategies;
using ErrorHandling.Core.Models;
using ErrorHandling.Core.Exceptions;
namespace ErrorHandling.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class App
{
    private readonly ErrorHandlingStrategies _strategies = new();
    [Params(-1, 1, 2)]
    public int UserId;
    // ********** Failure Path
    // *** TraditionalException
    // App
    [Benchmark(Baseline = true)]
    public User? App_TradExcep_D()
    {
        try
        {
            return _strategies.TradExcep_GetUserById_D(UserId);
        }
        catch (ArgumentException)
        {
            return null;
        }
        catch (UserNotFoundException)
        {
            return null;
        }
    }
    // *** TryParse
    // App
    [Benchmark]
    public User? App_TryParse_D()
    {
        _strategies.TryParse_GetUserById_D(UserId, out var user);
        return user;
    }
    // *** ResultPattern
    // App
    [Benchmark]
    public User? App_ResultPattern_D()
    {
        var result = _strategies.ResultPattern_GetUserById_D(UserId);
        return result.IsSuccess ? result.Value : null;
    }
    // *** ReturnCode
    // App
    [Benchmark]
    public User? App_ReturnCode_D()
    {
        _strategies.ReturnCode_GetUserById_D(UserId, out var user);
        return user;
    }
    // *** Tester-Doer
    // App
    [Benchmark]
    public User? App_TesterDoer_D()
    {
        return _strategies.TesterDoer_GetUserById_D(UserId);
    }
}