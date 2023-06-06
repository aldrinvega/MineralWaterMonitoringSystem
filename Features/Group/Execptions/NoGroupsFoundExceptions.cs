namespace MineralWaterMonitoring.Features.Group.Execptions;

public class NoGroupsFoundExceptions : Exception
{
    public NoGroupsFoundExceptions() : base("No groups found"){}
}