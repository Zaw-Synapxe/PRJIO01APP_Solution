using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace LoopExamples.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Deserialize JSON data into a list of points
            List<Point> coords = GenerateLatLongs(1000);

            // Original json data
            string jsonData = JsonSerializer.Serialize(coords);

            // Compress JSON data using GZip
            byte[] compressedJsonData = CompressJsonData(jsonData);

            // Simplify the list of points using Douglas-Peucker algorithm
            List<Point> simplifiedPoints = DouglasPeuckerSimplification(coords, 0, coords.Count - 1, 45.0);
            string simplifiedJsonData = JsonSerializer.Serialize(simplifiedPoints);

            // Compressed peucker
            byte[] compressedPeucker = CompressJsonData(simplifiedJsonData);

            Console.WriteLine($"Original: {jsonData.Length} Bytes");
            Console.WriteLine($"Peucker: {simplifiedJsonData.Length} Bytes");
            Console.WriteLine($"Compressed (GZip): {compressedJsonData.Length} Bytes");
            Console.WriteLine($"Compressed Peucker: {compressedPeucker.Length} Bytes");
        }

        private static byte[] CompressJsonData(string jsonData)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);

            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(byteArray, 0, byteArray.Length);
                }
                return memoryStream.ToArray();
            }
        }

        private static List<Point> DouglasPeuckerSimplification(List<Point> points, int firstIndex, int lastIndex, double epsilon)
        {
            double dmax = 0.0;
            int index = 0;

            for (int i = firstIndex + 1; i < lastIndex; i++)
            {
                double d = PerpendicularDistance(points[i], points[firstIndex], points[lastIndex]);
                if (d > dmax)
                {
                    index = i;
                    dmax = d;
                }
            }

            List<Point> result = new List<Point>();

            if (dmax > epsilon)
            {
                List<Point> recResults1 = DouglasPeuckerSimplification(points, firstIndex, index, epsilon);
                List<Point> recResults2 = DouglasPeuckerSimplification(points, index, lastIndex, epsilon);

                result.AddRange(recResults1);
                result.RemoveAt(result.Count - 1);
                result.AddRange(recResults2);
            }
            else
            {
                result.Add(points[firstIndex]);
                result.Add(points[lastIndex]);
            }

            return result;
        }

        private static double PerpendicularDistance(Point point, Point start, Point end)
        {
            double area = Math.Abs(0.5 * (start.X * end.Y + end.X * point.Y + point.X * start.Y - end.X * start.Y - point.X * end.Y - start.X * point.Y));
            double bottom = Math.Sqrt(Math.Pow(end.Y - start.Y, 2) + Math.Pow(end.X - start.X, 2));
            double height = area / bottom * 2;

            return height;
        }

        private static List<Point> GenerateLatLongs(int count)
        {
            List<Point> latLongs = new List<Point>();
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                double latitude = random.NextDouble() * 180 - 90;
                double longitude = random.NextDouble() * 360 - 180;

                latLongs.Add(new Point() { X = latitude, Y = longitude });
            }

            return latLongs;
        }
    }

    internal class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
