// UserService.cs

using System;
using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.Extensions.Logging;

// IUserService.cs
public interface IUserService
{
    Task<int> ProcessUsersAsync(CancellationToken cancellationToken = default);
}

// IUserRepository.cs
public interface IUserRepository
{
    Task<List<User>> GetActiveUsersAsync(CancellationToken cancellationToken = default);
}

// User.cs
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly SemaphoreSlim _semaphoreSlim = new(10);

    public UserService(IUserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<int> ProcessUsersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Начинаем обработку пользователей");


        var users = await _userRepository.GetActiveUsersAsync(cancellationToken);
        var tasks = users.Select(user => SendWelcomeEmailWithLogging(user, cancellationToken));
        await Task.WhenAll(tasks);
        // int processedCount = 0;
        int processedCount = tasks.Count(t => t.IsCompletedSuccessfully);


        _logger.LogInformation("Обработано {Count} пользователей", processedCount);
        return processedCount;
    }

    private async Task SendWelcomeEmail(User user, CancellationToken ct = default)
    {
        // Имитация отправки email (в реальности — SmtpClient.Send)
        await Task.Delay(500, ct);
        // Thread.Sleep(500); // ❌ Блокирующая операция!
        using var client = new SmtpClient("smtp.example.com");
        using var message = new MailMessage("from@example.com", user.Email, "Welcome!", "Hello!");
        await client.SendMailAsync(message, ct); // ❌ Синхронная отправка
        return;
    }

    private async Task SendWelcomeEmailWithLogging(User user, CancellationToken ct)
    {
        await _semaphoreSlim.WaitAsync(ct);
        try
        {
            await SendWelcomeEmail(user, ct);
            _logger.LogInformation("Email отправлен пользователю {UserId}", user.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при отправке email пользователю {UserId}", user.Id);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}