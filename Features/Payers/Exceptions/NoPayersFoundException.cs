namespace MineralWaterMonitoring.Features.Payers.Exceptions;

public class NoPayersFoundException : Exception
{
    public NoPayersFoundException() : base("No payers found") { }
}