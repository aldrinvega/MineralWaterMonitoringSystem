namespace MineralWaterMonitoring.Features.Group.Execptions;

public class GroupIsNotExistException : Exception
{
    public GroupIsNotExistException() : base("Group is not exist") {}
}