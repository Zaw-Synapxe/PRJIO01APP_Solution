using System.Text.Json;
using System.Text.Json.Nodes;

namespace JsonNodeXYZ.ConsoleApp
{
    class Program
    {
        public static DateTime[]? DatesAvailable { get; set; }

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

            #region "Deserialize subsections of a JSON payload"
            ////// ------------------------------------------------
            ////string jsonString = """
            ////{
            ////  "Date": "2024-11-01T00:00:00",
            ////  "Temperature": 25,
            ////  "Summary": "Hot",
            ////  "DatesAvailable": [
            ////    "2024-11-01T00:00:00",
            ////    "2024-11-02T00:00:00"
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

            ////// Parse all of the JSON.
            ////JsonNode forecastNode = JsonNode.Parse(jsonString)!;

            ////// Get a single value
            ////int hotHigh = forecastNode["TemperatureRanges"]!["Hot"]!["High"]!.GetValue<int>();
            ////Console.WriteLine($"Hot.High={hotHigh}");
            ////// Get a subsection and deserialize it into a custom type.
            ////JsonObject temperatureRangesObject = forecastNode!["TemperatureRanges"]!.AsObject();
            ////using var stream = new MemoryStream();
            ////using var writer = new Utf8JsonWriter(stream);
            ////temperatureRangesObject.WriteTo(writer);
            ////writer.Flush();
            ////TemperatureRanges? temperatureRanges =
            ////    JsonSerializer.Deserialize<TemperatureRanges>(stream.ToArray());
            ////Console.WriteLine($"Cold.Low={temperatureRanges!["Cold"].Low}, Hot.High={temperatureRanges["Hot"].High}");
            ////// Get a subsection and deserialize it into an array.
            ////JsonArray datesAvailable = forecastNode!["DatesAvailable"]!.AsArray()!;
            ////Console.WriteLine($"DatesAvailable[0]={datesAvailable[0]}");
            ////// ------------------------------------------------
            #endregion

            #region "JsonNode average grade example"
            ////// ------------------------------------------------
            ////string jsonString = """
            ////{
            ////  "Class Name": "Science",
            ////  "Teacher\u0027s Name": "Jane",
            ////  "Semester": "2019-01-01",
            ////  "Students": [
            ////    {
            ////      "Name": "John",
            ////      "Grade": 94.3
            ////    },
            ////    {
            ////      "Name": "James",
            ////      "Grade": 81.0
            ////    },
            ////    {
            ////      "Name": "Julia",
            ////      "Grade": 91.9
            ////    },
            ////    {
            ////      "Name": "Jessica",
            ////      "Grade": 72.4
            ////    },
            ////    {
            ////      "Name": "Johnathan"
            ////    }
            ////  ],
            ////  "Final": true
            ////}
            ////""";
            ////double sum = 0;
            ////JsonNode document = JsonNode.Parse(jsonString)!;

            ////JsonNode root = document.Root;
            ////JsonArray studentsArray = root["Students"]!.AsArray();

            ////int count = studentsArray.Count;
            ////foreach (JsonNode? student in studentsArray)
            ////{
            ////    if (student?["Grade"] is JsonNode gradeNode)
            ////    {
            ////        sum += (double)gradeNode;
            ////    }
            ////    else
            ////    {
            ////        sum += 70;
            ////    }
            ////}

            ////double average = sum / count;
            ////Console.WriteLine($"Average grade : {average}");
            ////// ------------------------------------------------
            #endregion

            #region "JsonNode with JsonSerializerOptions"
            ////// ------------------------------------------------
            Person person = new() { Name = "Nancy" };

            // Default serialization - Address property included with null token.
            // Output: {"Name":"Nancy","Address":null}
            string personJsonWithNull = JsonSerializer.Serialize(person);
            Console.WriteLine(personJsonWithNull);



            ////// ------------------------------------------------
            #endregion

            #region "333"
            ////// ------------------------------------------------

            ////// ------------------------------------------------
            #endregion

            #region "444"
            ////// ------------------------------------------------

            ////// ------------------------------------------------
            #endregion

            await Task.Delay(500);
            Console.WriteLine("... Program End ...");
            //
        }
    }

    public class TemperatureRanges : Dictionary<string, HighLowTemps>
    {
    }

    public class HighLowTemps
    {
        public int High { get; set; }
        public int Low { get; set; }
    }

    public class Person
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }


    //
}