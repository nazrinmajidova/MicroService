﻿namespace NotificationServer.Configurations;

public class EmailConfiguration
{
    public string? From { get; set; } = null!;
    public string? SmtpServer  { get; set; } = null!;
    public string? Username    { get; set; } = null!;
    public string? Password    { get; set; } = null!;
    public string? DisplayName { get; set; } = null!;
    public int Port            { get; set; }

}
