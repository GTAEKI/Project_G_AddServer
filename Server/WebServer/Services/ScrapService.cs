using GameDB;

namespace WebServer.Services
{
    public class ScrapService
    {
        GameDbContext _dbContext;

        public ScrapService(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void InputScrapByPlayerId(string userId, int scrap) 
        {
            PlayerDb? findDb = _dbContext.Players.FirstOrDefault(t => t.UserId == userId);
            if(findDb != null) 
            {
                findDb.Scrap = scrap;
                _dbContext.SaveChanges();
            }
            else 
            {
                PlayerDb playerDb = new PlayerDb();
                playerDb.UserId = userId;
                playerDb.Scrap = scrap;

                _dbContext.Players.Add(playerDb);

                _dbContext.SaveChanges();
            }
        }

        public int GetScrapByPlayerId(string userId)
        {
            int scrap = 0;

            PlayerDb? findDb = _dbContext.Players.FirstOrDefault(t => t.UserId == userId);
            if (findDb != null)
            {
                scrap = findDb.Scrap;
            }

            return scrap;
        }
    }
}
