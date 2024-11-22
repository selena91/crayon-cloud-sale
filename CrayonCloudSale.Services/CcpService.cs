using CrayonCloudSale.Core;
using CrayonCloudSale.Core.DTOs;
using CrayonCloudSale.Infrastructure.Data.Models;
using CrayonCloudSale.Infrastructure.UnitOfWork;
using CrayonCloudSale.Services.Interfaces;
using System.Net.Http.Json;

namespace CrayonCloudSale.Services;

public class CcpService:ICcpService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AzureConfiguration _azureConfiguration;
    private readonly IUnitOfWork _unitOfWork;

    public static readonly List<Software> _softwareList = new()
    {
        new Software(1, "Microsoft Office 365", "A suite of productivity tools that includes Word, Excel, PowerPoint, Outlook, and more, with cloud-based collaboration and storage."),
        new Software(2, "Adobe Creative Cloud", "A collection of design, video editing, and photography software including Photoshop, Illustrator, Premiere Pro, After Effects, and more."),
        new Software(3, "Autodesk AutoCAD", "A software for 2D and 3D design and drafting, commonly used in architecture, engineering, and construction."),
        new Software(4, "IntelliJ IDEA (JetBrains)", "An integrated development environment (IDE) used primarily for Java development, but also supports a wide range of other languages."),
        new Software(5, "Salesforce", "A customer relationship management (CRM) platform that provides cloud-based solutions for sales, marketing, and customer service."),
        new Software(6, "Norton 360", "A comprehensive security suite that includes antivirus protection, a VPN, identity theft protection, and more."),
        new Software(7, "Zoom", "A video conferencing software used for meetings, webinars, and collaboration, offering cloud-based video communication."),
        new Software(8, "QuickBooks", "Accounting software that helps manage finances, track expenses, and generate invoices, primarily for small to medium-sized businesses."),
        new Software(9, "VMware vSphere", "A virtualization platform used to build, manage, and run virtual machines across multiple physical servers."),
        new Software(10, "GitHub Enterprise", "A platform for version control and collaboration on code repositories, designed for enterprises with advanced security, compliance, and collaboration features.")
    };

    public CcpService(IHttpClientFactory httpClientFactory, AzureConfiguration azureConfiguration, IUnitOfWork unitOfWork)
    {
        _httpClientFactory = httpClientFactory;
        _azureConfiguration = azureConfiguration;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Software>> GetSoftwareServices() {
        //var client = _httpClientFactory.CreateClient(_azureConfiguration.CcpClientName);
        //var response = await client.GetAsync(_azureConfiguration.CcpGetApiUrl);

        //var parsedResponse = await response.Content.ReadFromJsonAsync<List<Software>>();

        return _softwareList;
    }

    public async Task OrderSoftware(int accountId, string serviceName, int quantity)
    {
        //var client = _httpClientFactory.CreateClient(_azureConfiguration.CcpClientName);
        //var response = await client.GetAsync(_azureConfiguration.CcpOrderApiUrl);
        //perform http post method and include params in request body

        var account = (await _unitOfWork.AccountRepository.GetAsyncWithoutTracking(a => a.Id == accountId, null, a => a.PurchasedSoftwares)).FirstOrDefault();

        if (account == null)
        {
            throw new ArgumentException($"Account with id {accountId} not found.");            
        }

        if (account.PurchasedSoftwares.Any(x => x.Name == serviceName))
        {
            throw new InvalidCastException($"Account with id {accountId} already purchased license for {serviceName}");
        }

        account.PurchasedSoftwares.Add(new PurchasedSoftware { Name = serviceName, Quantity = quantity, State = State.Active, CreateDate = DateTime.Now, ChangeDate = DateTime.Now });

        _unitOfWork.AccountRepository.Update(account);
    }
}
