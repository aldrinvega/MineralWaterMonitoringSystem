namespace MineralWaterMonitoring.Features.Payers.Exceptions;

public class GroupIsNotExistException : Exception
{
    public GroupIsNotExistException() : base("Group is not exist") {}
}