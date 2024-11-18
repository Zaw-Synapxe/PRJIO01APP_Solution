using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonNodeXYZ.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // 
            Console.WriteLine("... Program Started ...");
            //Console.WriteLine($"{args[0]} {args[1]}");

            ////// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/use-dom
            
            #region "Use JsonNode"
            ////// ------------------------------------------------
            ////string jsonString = """
            ////{
            ////  "Date": "2024-08-01T00:00:00",
            ////  "Temperature": 25,
            ////  "Summary": "Hot",
            ////  "DatesAvailable": [
            ////    "2024-08-01T00:00:00",
            ////    "2024-08-02T00:00:00"
            ////  ],
            ////  "TemperatureRanges": {
            ////      "Cold": {
            ////          "High": 20,
            ////          "Low": -10
            ////      },
            ////      "Hot": {
            ////          "High": 60,
            ////          "Low": 20
            ////      }
            ////  }
            ////}
            ////""";

            ////// Create a JsonNode DOM from a JSON string.
            ////JsonNode forecastNode = JsonNode.Parse(jsonString)!;

            ////// Write JSON from a JsonNode
            ////var options = new JsonSerializerOptions { WriteIndented = true };
            ////Console.WriteLine(forecastNode!.ToJsonString(options));

            ////// Get value from a JsonNode.
            ////JsonNode temperatureNode = forecastNode!["Temperature"]!;
            ////Console.WriteLine($"Type={temperatureNode.GetType()}");
            ////Console.WriteLine($"JSON={temperatureNode.ToJsonString()}");
            ////// Get a typed value from a JsonNode.
            ////int temperatureInt = (int)forecastNode!["Temperature"]!;
            ////Console.WriteLine($"Value={temperatureInt}");
            ////// Get a typed value from a JsonNode by using GetValue<T>.
            ////temperatureInt = forecastNode!["Temperature"]!.GetValue<int>();
            ////Console.WriteLine($"TemperatureInt={temperatureInt}");
            ////// Get a JSON object from a JsonNode.
            ////JsonNode temperatureRanges = forecastNode!["TemperatureRanges"]!;
            ////Console.WriteLine($"Type={temperatureRanges.GetType()}");
            ////Console.WriteLine($"JSON={temperatureRanges.ToJsonString()}");
            ////// Get a JSON array from a JsonNode.
            ////JsonNode datesAvailable = forecastNode!["DatesAvailable"]!;
            ////Console.WriteLine($"Type={datesAvailable.GetType()}");
            ////Console.WriteLine($"JSON={datesAvailable.ToJsonString()}");
            ////// Get an array element value from a JsonArray.
            ////JsonNode firstDateAvailable = datesAvailable[0]!;
            ////Console.WriteLine($"Type={firstDateAvailable.GetType()}");
            ////Console.WriteLine($"JSON={firstDateAvailable.ToJsonString()}");
            ////// Get a typed value by chaining references.
            ////int coldHighTemperature = (int)forecastNode["TemperatureRanges"]!["Cold"]!["High"]!;
            ////Console.WriteLine($"TemperatureRanges.Cold.High={coldHighTemperature}");
            ////// Parse a JSON array
            ////var datesNode = JsonNode.Parse(@"[""2024-08-01T00:00:00"",""2024-08-02T00:00:00""]");
            ////JsonNode firstDate = datesNode![0]!.GetValue<DateTime>();
            ////Console.WriteLine($"firstDate={firstDate}");
            ////// ------------------------------------------------
            #endregion

            #region "Create a JsonNode DOM with object"
            ////// ------------------------------------------------
            ////// Create a new JsonObject using object initializers.
            ////var forecastObject = new JsonObject
            ////{
            ////    ["Date"] = new DateTime(2024, 10, 29),
            ////    ["Temperature"] = 25,
            ////    ["Summary"] = "Hot",
            ////    ["DatesAvailable"] = new JsonArray(
            ////        new DateTime(2024, 11, 1), new DateTime(2024, 11, 2)),
            ////    ["TemperatureRanges"] = new JsonObject
            ////    {
            ////        ["Cold"] = new JsonObject
            ////        {
            ////            ["High"] = 20,
            ////            ["Low"] = -10
            ////        }
            ////    },
            ////    ["SummaryWords"] = new JsonArray("Cool", "Windy", "Humid")
            ////};

            ////// Add an object.
            ////forecastObject!["TemperatureRanges"]!["Hot"] =
            ////    new JsonObject { ["High"] = 60, ["Low"] = 20 };

            ////// Remove a property.
            ////////forecastObject.Remove("SummaryWords");

            ////// Change the value of a property.
            ////forecastObject["Date"] = new DateTime(2024, 11, 1);

            ////var options = new JsonSerializerOptions { WriteIndented = true };
            ////Console.WriteLine(forecastObject.ToJsonString(options));
            ////// ------------------------------------------------
            #endregion




            await Task.Delay(500);
            Console.WriteLine("... Program End ...");
            //
        }
    }
}