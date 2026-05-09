using System.Runtime.CompilerServices;

namespace PlayLib.Application.Interfaces;

public interface IEmailSender 
{
    void SendEmail(string recipient, string subject, string message);

}
