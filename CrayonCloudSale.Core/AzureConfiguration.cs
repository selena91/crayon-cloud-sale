namespace CrayonCloudSale.Core;

public class AzureConfiguration
{
    public const string Azure = "Azure";

    public string ConnectionString { get; set; } = string.Empty;

    public string CcpGetApiUrl { get; set; } = string.Empty;

    public string CcpOrderApiUrl { get; set; } = string.Empty;
}
