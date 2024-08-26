using MassTransit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumApplication.Data;
using SeleniumExtras.WaitHelpers;
using Shared.Dtos.Jobs;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace SeleniumApplication.Senders;
public class JobSearchSender
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public JobSearchSender(ISendEndpointProvider sendEndpointProvider) => _sendEndpointProvider = sendEndpointProvider;

    public async Task SendJobSearchMessageAsync(JobSearchDto model)
    {
        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:jobsearch-queue"));
        await sendEndpoint.Send(model);
    }
}


