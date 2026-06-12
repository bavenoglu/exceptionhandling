using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Diagnosers;
using ErrorHandling.Benchmarks.Benchmarks;

namespace ErrorHandling.Benchmarks;

class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Select Benchmark");
        Console.WriteLine();
        Console.WriteLine("1. App Errors Benchmarking");
        Console.WriteLine("2. System Errors Benchmarking");
        Console.WriteLine("3. Exit");
        Console.WriteLine();
        Console.Write("Make a selection: ");
        var choice = Console.ReadLine();
        Console.WriteLine();

        var config = DefaultConfig.Instance
            .AddDiagnoser(MemoryDiagnoser.Default)
            .AddExporter(HtmlExporter.Default);

        switch (choice)
        {
            case "1":
                Console.WriteLine("App Benchmark is executing...");
                BenchmarkRunner.Run<App>(config);
                /*App app = new App();
                app.UserId = 2;
                app.App_TraditionalException();
                app.App_TryParse();
                app.App_ResultPattern();
                app.App_ReturnCode();
                app.App_TesterDoer();*/
                break;
            case "2":
                Console.WriteLine("System Benchmark is executing...");
                /*Sys sys = new Sys();
                sys.Divisor = 0;
                sys.Sys_TraditionalException();
                sys.Sys_TryParse();
                sys.Sys_ResultPattern();
                sys.Sys_ReturnCode();
                sys.Sys_TesterDoer();*/
                BenchmarkRunner.Run<Sys>(config);
                break;
            case "3":
                Console.WriteLine("Exiting...");
                return;
            default:
                Console.WriteLine("Undefined benchmark choice...!");
                Console.WriteLine("Program: dotnet run -c Release");
                return;
        }
        Console.WriteLine();
        Console.WriteLine("Benchmark completed!");
        Console.WriteLine("Results in: BenchmarkDotNet.Artifacts/results/");
    }
}