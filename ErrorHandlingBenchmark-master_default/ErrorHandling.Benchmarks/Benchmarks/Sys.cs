using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using ErrorHandling.Core.Strategies;

namespace ErrorHandling.Benchmarks.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Sys
{
    private readonly ErrorHandlingStrategies _strategies = new();

    [Params(1, 0)] public int Divisor;

    // ********** Failure Path
    // *** TraditionalException
    // Sys
    [Benchmark(Baseline = true)]
    public int Sys_TradExcep_D()
    {
        try
        {
            return _strategies.TradExcep_DivideByZero_D(1, Divisor);
        }
        catch (DivideByZeroException)
        {
            return 0;
        }
    }

    // *** TryParse
    // Sys
    [Benchmark]
    public int Sys_TryParse_D()
    {
        _strategies.TryParse_DivideByZero_D(1, Divisor, out var result);
        return result;
    }

    // *** ResultPattern
    // Sys
    [Benchmark]
    public int Sys_ResultPattern_D()
    {
        var result = _strategies.ResultPattern_DivideByZero_D(1, Divisor);
        return result.IsSuccess ? result.Value : 0;
    }

    // *** ReturnCode
    // Sys
    [Benchmark]
    public int Sys_ReturnCode_D()
    {
        _strategies.ReturnCode_DivideByZero_D(1, Divisor, out var result);
        return result;
    }

    // *** ReturnCode
    // Sys
    [Benchmark]
    public int Sys_TesterDoer_D()
    {
        return _strategies.TesterDoer_DivideByZero_D(1, Divisor);
    }
}