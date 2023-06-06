namespace MineralWaterMonitoring.Features.Exceptions;

public class UserAlreadyExistExceptions : Exception
{
    public UserAlreadyExistExceptions(string username) : base($"Username: {username} already exist"){}
}