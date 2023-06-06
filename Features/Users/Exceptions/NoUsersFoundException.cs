namespace MineralWaterMonitoring.Features.Users.Exceptions;

public class NoUsersFoundException : Exception
{
    public NoUsersFoundException() : base($"No users found"){}
}