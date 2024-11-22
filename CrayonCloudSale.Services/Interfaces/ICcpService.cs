using CrayonCloudSale.Core.DTOs;

namespace CrayonCloudSale.Services.Interfaces;

public interface ICcpService
{
    Task<List<Software>> GetSoftwareServices();
    Task OrderSoftware(int accountId, string serviceName, int quantity);
}
