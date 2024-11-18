using System.Collections.Concurrent;
using AimReactionAPI.Data;
using AimReactionAPI.Models;

namespace AimReactionAPI.Services;

public class GameSessionHandler<TGameType> where TGameType : struct, Enum
{
    private readonly AppDbContext _context;
    private static readonly ConcurrentDictionary<int, int> ActiveSessions = new();
    public GameSessionHandler(AppDbContext context)
    {
        _context = context;
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
        
        return session;
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
