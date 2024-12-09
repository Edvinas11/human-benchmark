using System.Collections.Concurrent;
using AimReactionAPI.Data;
using AimReactionAPI.Models;

namespace AimReactionAPI.Services;

public class GameSessionHandler<TGameType> where TGameType : struct, Enum
{
    private readonly AppDbContext _context;
    private readonly IServiceScopeFactory _scopeFactory;
    private static readonly ConcurrentDictionary<int, int> ActiveSessions = new();
    public GameSessionHandler(AppDbContext context, IServiceScopeFactory scopeFactory)
    {
        _context = context;
        _scopeFactory = scopeFactory;
    }

    

    public async Task<GameSession> StartSessionAsync(int userId, TGameType gameType)
    {
        var session = new GameSession
        {
            UserId = userId,
            StartTime = DateTime.UtcNow,
            GameType = (GameType)(object)gameType,
            
        };

        _context.GameSessions.Add(session);
        await _context.SaveChangesAsync();

        ActiveSessions.TryAdd(session.GameSessionId, userId);

         _ = EndSessionAfterDelayAsync(session.GameSessionId, TimeSpan.FromHours(1));
        
        return session;
    }

    private async Task EndSessionAfterDelayAsync(int sessionId, TimeSpan delay)
    {
        await Task.Delay(delay);

        // Use a new scope to ensure DbContext is valid
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (ActiveSessions.ContainsKey(sessionId))
        {
            try
            {
                var session = await dbContext.GameSessions.FindAsync(sessionId);

                if (session != null)
                {
                    session.EndTime = DateTime.UtcNow;
                    await dbContext.SaveChangesAsync();
                    ActiveSessions.TryRemove(sessionId, out _);
                    Console.WriteLine($"Session {sessionId} automatically ended after {delay.TotalSeconds} seconds.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error automatically ending session {sessionId}: {ex.Message}");
            }
        }
    }


    public async Task<TimeSpan> EndSessionAsync(int sessionId)
    {
        var session = await _context.GameSessions.FindAsync(sessionId);

        if (session == null)
        {
            throw new InvalidOperationException("Session not found.");
        }

        session.EndTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        ActiveSessions.TryRemove(sessionId, out _);

        return session.GetDuration();
    }

    public int GetActiveSessionCount()
    {
        return ActiveSessions.Count;
    }
}
