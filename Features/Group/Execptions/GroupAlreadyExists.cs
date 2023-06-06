namespace MineralWaterMonitoring.Features.Group.Execptions;

public class GroupAlreadyExists : Exception
{
    public GroupAlreadyExists(string groupName) : base($"{groupName} is already exist"){}
}