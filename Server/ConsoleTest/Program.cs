using GameDB;

namespace ConsoleTest
{
    // Create Read Update Delete
    internal class Program
    {
        static void Main(string[] args)
        {
            using (GameDbContext db = new GameDbContext()) 
            {
                TestDb testDb = new TestDb();
                testDb.Name = "BKT2";
                testDb.Scrap = 5;
                testDb.Test2 = 3;

                db.Tests.Add(testDb);

                db.SaveChanges();
            }
        }
    }
}
