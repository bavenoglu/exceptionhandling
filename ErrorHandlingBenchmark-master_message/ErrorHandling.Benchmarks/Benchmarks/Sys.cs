using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using ErrorHandling.Core.Strategies;
using ErrorHandling.Core.Common;
namespace ErrorHandling.Benchmarks.Benchmarks;
#nullable disable
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Sys
{
    private readonly ErrorHandlingStrategies _strategies = new();
    [Params(1, 0)]
    public int Divisor;
    public string Errormessage = "";
    // ********** Failure Path
    // *** TraditionalException
    // Sys
    [Benchmark(Baseline = true)]
    public int Sys_TradExcep_M()
    {
        try
        {
            return _strategies.TradExcep_DivideByZero_M(1,Divisor);
        }
        catch (DivideByZeroException)
        {
            Errormessage = "Cannot be divided by zero";
            return 0;
        }
    }
    // *** TryParse
    // Sys
    [Benchmark]
    public int Sys_TryParse_M()
    {
        var log = _strategies.TryParse_DivideByZero_M(1, Divisor, out var result);
        if(!log)
            Errormessage = "Cannot be divided by zero";
        return result;
    }
    // *** ResultPattern
    // Sys
    [Benchmark]
    public int Sys_ResultPattern_M()
    {
        var result = _strategies.ResultPattern_DivideByZero_M(1, Divisor);
        if(!result.IsSuccess)
        {
            Errormessage = result.Error;
            return 0;
        }
        return result.Value;
    }
    // *** ReturnCode
    // Sys
    [Benchmark]
    public int Sys_ReturnCode_M()
    {
        var errorCode = _strategies.ReturnCode_DivideByZero_M(1,Divisor, out var result);
        Errormessage = errorCode.ToMessage();
        return result;
    }
    // *** ReturnCode
    // Sys
    [Benchmark]
    public int Sys_TesterDoer_M()
    {
        var res = _strategies.TesterDoer_DivideByZero_M(1,Divisor, out string? message);
        Errormessage = message; 
        return res;
    }
}