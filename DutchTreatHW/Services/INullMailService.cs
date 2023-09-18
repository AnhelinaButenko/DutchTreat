using Microsoft.Extensions.Logging;

namespace DutchTreatHW.Services;

public interface INullMailService
{
    ILogger<NullMailService> Logger { get; }

    void SendMessage(string to, string subject, string body);
}