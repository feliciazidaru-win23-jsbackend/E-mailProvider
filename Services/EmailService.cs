﻿using Azure;
using Azure.Communication.Email;
using Azure.Messaging.ServiceBus;
using E_mailProvider.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_mailProvider.Services;

public class EmailService(EmailClient emailClient, ILogger<EmailService> logger) : IEmailService
{
    private readonly EmailClient _emailClient = emailClient;
    private readonly ILogger<EmailService> _logger = logger;

    public EmailRequest UnpackEmailRequest(ServiceBusReceivedMessage message)
    {
        try
        {
            var emailRequest = JsonConvert.DeserializeObject<EmailRequest>(message.Body.ToString());
            if (emailRequest != null)
                return emailRequest;
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmailSender.UnpackEmailRequest() :: {ex.Message}");

        }
        return null!;
    }

    public bool SendEmail(EmailRequest emailRequest)
    {
        try
        {
            var result = _emailClient.Send(
            WaitUntil.Completed,
            senderAddress: Environment.GetEnvironmentVariable("SenderAddress"),
            recipientAddress: emailRequest.To,
            subject: emailRequest.Subject,
            htmlContent: emailRequest.HtmlBody,
            plainTextContent: emailRequest.PlainText);

            if (result.HasCompleted)
                return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"ERROR : EmailSender.SendEmail() :: {ex.Message}");

        }
        return false;
    }

}
