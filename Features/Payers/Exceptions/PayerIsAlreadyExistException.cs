namespace MineralWaterMonitoring.Features.Payers.Exceptions;

public class PayerIsAlreadyExistException : Exception
{
    public PayerIsAlreadyExistException(string name) : base($"{name} is already exist"){ }
}