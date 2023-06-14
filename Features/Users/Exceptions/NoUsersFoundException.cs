namespace MineralWaterMonitoring.Features.Users.Exceptions;

public class NoUsersFoundException : Exception
{
    public NoUsersFoundException() : base($"No user(s) found"){}
}