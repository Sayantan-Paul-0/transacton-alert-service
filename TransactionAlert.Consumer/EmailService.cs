using MailKit.Net.Smtp;
using MimeKit;

namespace TransactionAlert.Consumer;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAlertAsync(string toEmail, string subject, string body)
    {
        var from = _config["Email:From"]!;
        var appPassword = _config["Email:AppPassword"]!;

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Transaction Alert", from));
        message.To.Add(new MailboxAddress("", toEmail));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = body };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(from, appPassword);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}