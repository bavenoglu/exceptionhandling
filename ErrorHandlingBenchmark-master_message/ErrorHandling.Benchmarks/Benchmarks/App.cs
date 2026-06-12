using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using ErrorHandling.Core.Common;
using ErrorHandling.Core.Strategies;
using ErrorHandling.Core.Models;
using ErrorHandling.Core.Exceptions;
namespace ErrorHandling.Benchmarks.Benchmarks;
#nullable disable
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class App
{
    private readonly ErrorHandlingStrategies _strategies = new();
    [Params(-1, 1, 2)]
    public int UserId;
    public string Errormessage = "";
    // ********** Failure Path
    // *** TraditionalException
    // App
    [Benchmark(Baseline = true)]
    public User? App_TradExcep_M()
    {
        try
        {
            var user = _strategies.TradExcep_GetUserById_M(UserId);
            Errormessage = "";
            return user;
        }
        catch (ArgumentException ae)
        {
            Errormessage = ae.Message;
            return null;
        }
        catch (UserNotFoundException unfe)
        {
            Errormessage = unfe.Message;
            return null;
        }
    }
    // *** TryParse
    // App
    [Benchmark]
    public User? App_TryParse_M()
    {
        _strategies.TryParse_GetUserById_M(UserId, out var user, out string? message);
        Errormessage = message;
        return user;
    }
    // *** ResultPattern
    // App
    [Benchmark]
    public User? App_ResultPattern_M()
    {
        var result = _strategies.ResultPattern_GetUserById_M(UserId);
        Errormessage = result.Error;
        return result.IsSuccess ? result.Value : null;
    }
    // *** ReturnCode
    // App
    [Benchmark]
    public User? App_ReturnCode_M()
    {
        var errorCode = _strategies.ReturnCode_GetUserById_M(UserId, out var user);
        Errormessage = errorCode.ToMessage();
        return user;
    }
    // *** Tester-Doer
    // App
    [Benchmark]
    public User? App_TesterDoer_M()
    {
        var user = _strategies.TesterDoer_GetUserById_M(UserId, out string? message);
        Errormessage = message;
        return user;
    }
}