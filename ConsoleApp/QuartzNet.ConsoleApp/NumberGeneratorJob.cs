using Quartz;

namespace QuartzNet.ConsoleApp
{
    public class NumberGeneratorJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Your # is: {RandomNumber(1000, 9999)}");
            await Task.CompletedTask;
            //throw new NotImplementedException();
        }

        //private int RandomNumber(int min, int max)
        //{
        //    Random random = new Random();
        //    return random.Next(min, max);
        //}
        private int RandomNumber(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }
    }
}
