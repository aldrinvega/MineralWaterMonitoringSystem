namespace MineralWaterMonitoring.Features.Users;

public interface IUserServices
{
    void AddNewUser(Domain.Users users);
    Task<Domain.Users> GetUserByName(string username);
    Task SaveAsync();
}