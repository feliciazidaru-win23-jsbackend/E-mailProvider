using Azure.Messaging.ServiceBus;
using E_mailProvider.Models;

namespace E_mailProvider.Services;

    public interface IEmailService
    {
        bool SendEmail(EmailRequest emailRequest);
        EmailRequest UnpackEmailRequest(ServiceBusReceivedMessage message);
    }
