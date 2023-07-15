// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

class Program
{
    static void Main(string[] args)
    {
        try
        {
            MathUtils mathUtils = new MathUtils();
            int result = mathUtils.Add(2, 3);
            Console.WriteLine(result);

            var prompt = @"
            using Moq;

            var mock = new Mock<MathUtils>();
            var mathUtils = mock.Object;

            // Generate unit test 
            ";

            OpenAi.OpenAi.Connect("Bearer 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");
            var chat = OpenAi.OpenAi.CreateCompletion(prompt: prompt, model: "text-davinci-003", maxTokens: 100).GetAwaiter().GetResult();
            Console.WriteLine(chat.Choices[0].Text);
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
            throw;
        }
    }
}



public class MathUtils
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}