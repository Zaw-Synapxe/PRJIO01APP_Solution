using System.Text;
using Newtonsoft.Json;

// API key (https://platform.openai.com/account/api-keys)
// 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
// https://gpttools.com/comparisontool
// https://jsoneditoronline.org


class Program
{
    static async System.Threading.Tasks.Task Main(string[] args)
    {

       
        args = new string[1];
        args[0] = "Say Hellow from ChatGPT";

        Console.WriteLine("First argument: {0}", args[0]);
        

        if (args.Length > 0)
        {
            HttpClient client = new HttpClient();
            // "Bearer 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"
            client.DefaultRequestHeaders.Add("authorization", "Bearer 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");

            var content = new StringContent("{\"model\": \"text-davinci-001\", \"prompt\": \"" + args[0] + "\",\"temperature\": 1,\"max_tokens\": 100}",
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/completions", content);

            string responseString = await response.Content.ReadAsStringAsync();
            /*
            {"id":"cmpl-6gVkMRM8WCUNq4rHCYqST7HqPce7O","object":"text_completion","created":1675589830,"model":"text-davinci-001","choices":[{"text":"\n\nHello! Thank you for considering ChatGPT. We are a rapidly growing company that provides a unique and fun way to earn money online by completing simple tasks. Our platform is simple to use and our community is friendly and helpful. We hope you will consider joining us and helping us grow!","index":0,"logprobs":null,"finish_reason":"stop"}],"usage":{"prompt_tokens":7,"completion_tokens":60,"total_tokens":67}}
            */

            Console.WriteLine(responseString);

            try
            {
                //string json = "{\"name\":\"John\",\"age\":30,\"city\":\"New York\"}";
                //dynamic obj = JsonConvert.DeserializeObject<dynamic>(json);

                //Console.WriteLine("Name: " + obj.name);
                //Console.WriteLine("Age: " + obj.age);
                //Console.WriteLine("City: " + obj.city);


                var dyData = JsonConvert.DeserializeObject<dynamic>(responseString);

                string guess = GuessCommand(dyData!.choices[0].text);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"---> My guess at the command prompt is: {guess}");
                Console.ResetColor();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"---> Could not deserialize the JSON: {ex.Message}");
            }

            
        }
        else
        {
            Console.WriteLine("---> You need to provide some input");
        }


    }

    static string GuessCommand(string raw)
    {
        Console.WriteLine("---> GPT-3 API Returned Text:");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(raw);

        var lastIndex = raw.LastIndexOf('\n');

        string guess = raw.Substring(lastIndex + 1);

        Console.ResetColor();

        TextCopy.ClipboardService.SetText(guess);

        return guess;
    }

}


