namespace MineralWaterMonitoring.Features.Role.Exceptions;

public class NoRolesFoundException : Exception
{
    public NoRolesFoundException() : base("No role found"){}
}