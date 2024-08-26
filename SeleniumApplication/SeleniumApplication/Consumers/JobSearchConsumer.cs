using MassTransit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumApplication.Data;
using SeleniumExtras.WaitHelpers;
using Shared.Dtos.Jobs;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace SeleniumApplication.Consumers;

public class JobSearchConsumer : IConsumer<JobSearchDto>
{
    public Task Consume(ConsumeContext<JobSearchDto> context)
    {
        Console.WriteLine($"JobSearchConsumer => Keyword: {context.Message.KeyWord},JobSearchConsumer => Url: {context.Message.WebUrl} ");
        return Task.CompletedTask;
    }
}


