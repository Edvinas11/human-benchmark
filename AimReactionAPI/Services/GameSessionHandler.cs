using AimReactionAPI.Data;
using AimReactionAPI.Models;

namespace AimReactionAPI.Services;

public class GameSessionHandler<TGameType> where TGameType : struct, Enum
{
    private readonly AppDbContext _context;
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
        return session;
    }

    public async Task<TimeSpan> EndSessionAsync(int sessionId)
    {
        var session = await _context.GameSessions.FindAsync(sessionId);

        if (session == null)
        {
            throw new InvalidOperationException("Session no found.");
        }

        session.EndTime = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return session.GetDuration();
    }
}
