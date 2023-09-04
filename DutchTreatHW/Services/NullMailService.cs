using Microsoft.Extensions.Logging;

namespace DutchTreatHW.Services
{
    // for testing or debugging project
    public class NullMailService : INullMailService
    {
        private readonly ILogger<NullMailService> _logger;

        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }

        public ILogger<NullMailService> Logger { get; }

        public void SendMessage(string to, string subject, string body)
        {
            // Log the message, not send
            _logger.LogInformation($"To: {to} Subject: {subject}, Body: {body}");
        }
    }
}
