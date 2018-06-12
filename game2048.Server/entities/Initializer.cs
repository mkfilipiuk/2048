using game2048.Data;
using game2048.Models;
using System.Linq;

namespace game2048.Data
{
    public static class DbInitializer
    {
        public static void Initialize(Context context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Scores.Any())
            {
                return;   // DB has been seeded
            }

            var scores = new Score[]
            {
            new Score{Nickname="Carson",Points=3542, TypeOfScorer="Human"},
            new Score{Nickname="Benson",Points=1234, TypeOfScorer="AI"},
            new Score{Nickname="Larson",Points=4312, TypeOfScorer="Human"},
            };
            foreach (Score s in scores)
            {
                context.Scores.Add(s);
            }
            context.SaveChanges();
        }
    }
}