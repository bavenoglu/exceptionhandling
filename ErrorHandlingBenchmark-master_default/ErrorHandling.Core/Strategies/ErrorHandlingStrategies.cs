using ErrorHandling.Core.Models;
using ErrorHandling.Core.Common;
using ErrorHandling.Core.Exceptions;

namespace ErrorHandling.Core.Strategies;

public class ErrorHandlingStrategies
{
    private static readonly User ValidUser = 
        new User { Id = 1, Name = "John", Email = "john@test.com" };
    private static readonly Dictionary<int, User> Users = 
        new() { { 1, ValidUser } };

    // ===== STRATEGY 1: EXCEPTION =====
    public User TradExcep_GetUserById_D(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid ID");
        return Users.TryGetValue(id, out var user)
            ? user
            : throw new UserNotFoundException($"User id not found");
    }

    public int TradExcep_DivideByZero_D(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException();
        return a / b;
    }

    // ===== STRATEGY 2: TRY-PARSE PATTERN =====
    public bool TryParse_GetUserById_D(int id, out User? user)
    {
        if (id <= 0)
        {
            user = null;
            return false;
        }
        return Users.TryGetValue(id, out user);
    }

    public bool TryParse_DivideByZero_D(int a, int b, out int result)
    {
        if (b == 0)
        {
            result = 0;
            return false;
        }
        result = a / b;
        return true;
    }

    // ===== STRATEGY 3: RESULT PATTERN =====
    public Result<User> ResultPattern_GetUserById_D(int id)
    {
        if (id <= 0)
            return Result<User>.Failure("Invalid ID");
        return Users.TryGetValue(id, out var user)
            ? Result<User>.Success(user)
            : Result<User>.Failure($"User id not found");
    }

    public Result<int> ResultPattern_DivideByZero_D(int a, int b)
    {
        if (b == 0)
            return Result<int>.Failure("Cannot divide by zero");
        return Result<int>.Success(a / b);
    }

    // ===== STRATEGY 4: RETURN CODE =====
    public ErrorCode ReturnCode_GetUserById_D(int id, out User? user)
    {
        if (id <= 0)
        {
            user = null;
            return ErrorCode.InvalidInput;
        }
        return Users.TryGetValue(id, out user)
            ? ErrorCode.None
            : ErrorCode.NotFound;
    }

    public ErrorCode ReturnCode_DivideByZero_D(int a, int b, out int result)
    {
        if (b == 0)
        {
            result = 0;
            return ErrorCode.Error;
        }
        result = a / b;
        return ErrorCode.None;
    }

    // ===== STRATEGY 4: TESTER-DOER PATTERN =====
    // TESTER
    private bool CanGetUser_D(int id)
    {
        return id > 0 && Users.ContainsKey(id);
    }
    // DOER
    public User? TesterDoer_GetUserById_D(int id)
    {
        return CanGetUser_D(id)
            ? Users[id]
            : null;
    }

    // TESTER
    private bool CanDivide_D(int divisor)
    {
        return divisor != 0;
    }

    // DOER
    public int TesterDoer_DivideByZero_D(int a, int b)
    {
        return CanDivide_D(b)
            ? a / b
            : 0;
    }
}