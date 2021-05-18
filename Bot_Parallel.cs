using System;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {
            // TODO: распараллелить запуск SearchBestMove

            if (this.threadsCount == 2)
            {
                var task1 = Task.Factory.StartNew(() => 
                    SearchBestMove(rocket, new Random(random.Next()), iterationsCount / 2));                
                var task2 = Task.Factory.StartNew(() => 
                    SearchBestMove(rocket, new Random(random.Next()), iterationsCount / 2));
                return (task1.Result.Item2 > task2.Result.Item2) ?
                    rocket.Move(task1.Result.Item1, level) : rocket.Move(task2.Result.Item1, level);
            }

            var bestMove = SearchBestMove(rocket, new Random(random.Next()), iterationsCount);

            return rocket.Move(bestMove.Item1, level);
        }
    }
}