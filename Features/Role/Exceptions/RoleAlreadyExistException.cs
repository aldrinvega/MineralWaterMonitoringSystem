namespace MineralWaterMonitoring.Features.Role.Exceptions;

public class RoleAlreadyExistException : Exception
{
    public RoleAlreadyExistException(string roleName) : base($"{roleName} is already exist"){}
}