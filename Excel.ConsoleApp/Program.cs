using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Text;

class Program
{
    static void Main(string[] args)
    {


        string xSheetName = "MyTestSheet";
        List<string> xOrgColumn = new List<string>();
        List<string> xColumn = new List<string>();

        // Read from Excel
        // Lets open the existing excel file and read through its content . Open the excel using openxml sdk
        using (SpreadsheetDocument doc = SpreadsheetDocument.Open("testdataorg.xlsx", false))
        {
            //create the object for workbook part  
            WorkbookPart workbookPart = doc.WorkbookPart;
            Sheets thesheetcollection = workbookPart.Workbook.GetFirstChild<Sheets>();
            StringBuilder excelResult = new StringBuilder();

            //using for each loop to get the sheet from the sheetcollection
            foreach (Sheet thesheet in thesheetcollection)
            {

                if (xSheetName == thesheet.Name)
                {
                    excelResult.AppendLine("Excel file Orginal");
                    excelResult.AppendLine("Excel Sheet Name : " + thesheet.Name);
                    excelResult.AppendLine("----------------------------------------------- ");

                    //statement to get the worksheet object by using the sheet id  
                    Worksheet theWorksheet = ((WorksheetPart)workbookPart.GetPartById(thesheet.Id)).Worksheet;

                    SheetData thesheetdata = (SheetData)theWorksheet.GetFirstChild<SheetData>();
                    foreach (Row thecurrentrow in thesheetdata)
                    {
                        foreach (Cell thecurrentcell in thecurrentrow)
                        {
                            //statement to take the integer value  
                            string currentcellvalue = string.Empty;
                            if (thecurrentcell.DataType != null)
                            {
                                if (thecurrentcell.DataType == CellValues.SharedString)
                                {
                                    int id;
                                    if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                    {
                                        SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                        if (item.Text != null)
                                        {
                                            //code to take the string value  
                                            excelResult.Append(item.Text.Text + " ");

                                            // add to List the columns for compare
                                            xOrgColumn.Add(item.Text.Text.ToString());
                                        }
                                    }
                                    ////if (Int32.TryParse(thecurrentcell.InnerText, out id))
                                    ////{
                                    ////    SharedStringItem item = workbookPart.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                    ////    if (item.Text != null)
                                    ////    {
                                    ////        //code to take the string value  
                                    ////        excelResult.Append(item.Text.Text + " ");
                                    ////    }
                                    ////    else if (item.InnerText != null)
                                    ////    {
                                    ////        currentcellvalue = item.InnerText;
                                    ////    }
                                    ////    else if (item.InnerXml != null)
                                    ////    {
                                    ////        currentcellvalue = item.InnerXml;
                                    ////    }
                                    ////}
                                }
                            }
                            else
                            {
                                excelResult.Append(Convert.ToInt16(thecurrentcell.InnerText) + " ");
                            }
                        }

                        excelResult.AppendLine();
                    }


                    excelResult.Append("");
                    Console.WriteLine(excelResult.ToString());
                    //Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Invalid Sheet Name !");
                }

            }
            //
        }

        // if orginal excel have columns, read second excel file and compare column with first excel file
        if (xOrgColumn.Count > 0)
        {
            // Second excel file for checking
            using (SpreadsheetDocument doc2 = SpreadsheetDocument.Open("testdata.xlsx", false))
            {
                //create the object for workbook part  
                WorkbookPart workbookPart2 = doc2.WorkbookPart;
                Sheets thesheetcollection2 = workbookPart2.Workbook.GetFirstChild<Sheets>();
                StringBuilder excelResult2 = new StringBuilder();

                //using for each loop to get the sheet from the sheetcollection
                foreach (Sheet thesheet2 in thesheetcollection2)
                {
                    // checking sheet name
                    if (xSheetName == thesheet2.Name)
                    {
                        excelResult2.AppendLine("Excel file Data (second file)");
                        excelResult2.AppendLine("Excel Sheet Name : " + thesheet2.Name);
                        excelResult2.AppendLine("----------------------------------------------- ");

                        //statement to get the worksheet object by using the sheet id  
                        Worksheet theWorksheet2 = ((WorksheetPart)workbookPart2.GetPartById(thesheet2.Id)).Worksheet;

                        SheetData thesheetdata2 = (SheetData)theWorksheet2.GetFirstChild<SheetData>();

                        int xfirstrow = 0;
                        foreach (Row thecurrentrow2 in thesheetdata2)
                        {
                            if (xfirstrow == 1)
                            {
                                // compare columns

                                // Create the query. Note that method syntax must be used here.  
                                IEnumerable<string> differenceQuery = xOrgColumn.Except(xColumn);

                                // Execute the query.  
                                Console.WriteLine("The following lines are in (xOrgColumn) but not (xColumn)");
                                if (differenceQuery.Count() != 0)
                                {
                                    foreach (string s in differenceQuery)
                                        Console.WriteLine(s);

                                    // exit foreach loop ...
                                    break;
                                }

                            }

                            foreach (Cell thecurrentcell2 in thecurrentrow2)
                            {
                                //statement to take the integer value  
                                string currentcellvalue2 = string.Empty;
                                if (thecurrentcell2.DataType != null)
                                {
                                    if (thecurrentcell2.DataType == CellValues.SharedString)
                                    {
                                        int id;
                                        if (Int32.TryParse(thecurrentcell2.InnerText, out id))
                                        {
                                            SharedStringItem item = workbookPart2.SharedStringTablePart.SharedStringTable.Elements<SharedStringItem>().ElementAt(id);
                                            if (item.Text != null)
                                            {
                                                //code to take the string value  
                                                excelResult2.Append(item.Text.Text + " ");

                                                if (xfirstrow == 0)
                                                {
                                                    // add to List the columns for compare
                                                    xColumn.Add(item.Text.Text.ToString());
                                                }

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    excelResult2.Append(Convert.ToInt16(thecurrentcell2.InnerText) + " ");
                                }
                            }

                            xfirstrow += 1;
                            excelResult2.AppendLine();
                        }


                        excelResult2.Append("");
                        Console.WriteLine(excelResult2.ToString());
                        //Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Sheet Name from (second sheet) !");
                    }

                }
                //
            }
            //
        }

        //
        Console.ReadLine();





       ////// Write to Excel File
       ////// List<xDetails> xData = new List<xDetails>()
       ////// {
       //////     new xDetails() {ColumnABC="1001", ColumnDEF="ABCD", ColumnGHI ="City1", ColumnXYZ="USA"},
       //////     new xDetails() {ColumnABC="1002", ColumnDEF="PQRS", ColumnGHI ="City2", ColumnXYZ="INDIA"},
       //////     new xDetails() {ColumnABC="1003", ColumnDEF="XYZZ", ColumnGHI ="City3", ColumnXYZ="CHINA"},
       //////     new xDetails() {ColumnABC="1004", ColumnDEF="LMNO", ColumnGHI ="City4", ColumnXYZ="UK"},
       ////// };

       ////// Lets converts our object data to Datatable for a simplified logic.
       ////// Datatable is most easy way to deal with complex datatypes for easy reading and formatting.

       //////DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(xData), (typeof(DataTable)));




        Console.Read();



        //////////// ----------------------------------------------
        ////////var json = @"
        ////////[
        ////////        { make: ""Chevy"", model: ""2500"",  location: ""lot""},
        ////////        { make: ""Ford"", model: ""Edge"",  location: ""lot""},
        ////////        { make: ""Chevy"", model: ""1500"",  location: ""lot""},
        ////////        { make: ""Chevy"", model: ""Econoline"",  location: ""back lot""},
        ////////        { make: ""GMC"", model: ""Acadia"",  location: ""back lot""},
        ////////        { make: ""GMC"", model: ""Terrain"",  location: ""in transit""},
        ////////        { make: ""Chevy"", model: ""Cruze"",  location: ""in transit""},
        ////////        { make: ""Ford"", model: ""F150"",  location: ""ordered""}
        ////////    ]";
        ////////    var structure = new[] { new { make = "", model = "", location = "" } };
        ////////var data = JsonConvert.DeserializeAnonymousType(json, structure).ToArray();
        ////////var tree = data.GroupBy(x => x.make)
        ////////    .Select(g => new
        ////////    {
        ////////        text = g.Key,
        ////////        nodes = g.GroupBy(xx => xx.location)
        ////////                 .Select(gg => new
        ////////                 {
        ////////                     text = gg.Key,
        ////////                     nodes = gg.Select(xxx => new { text = xxx.model })
        ////////                 })
        ////////                 .ToArray()
        ////////    }).ToArray();
        ////////Console.WriteLine(JsonConvert.SerializeObject(tree, Formatting.Indented));
        ////////Console.ReadLine();


        ////////var weatherForecast = new WeatherForecast
        ////////{
        ////////    Date = DateTime.Parse("2019-08-01"),
        ////////    TemperatureCelsius = 25,
        ////////    Summary = "Hot",
        ////////    SummaryField = "Hot",
        ////////    DatesAvailable = new List<DateTimeOffset>()
        ////////            { DateTime.Parse("2019-08-01"), DateTime.Parse("2019-08-02") },
        ////////    TemperatureRanges = new Dictionary<string, HighLowTemps>
        ////////    {
        ////////        ["Cold"] = new HighLowTemps { High = 20, Low = -10 },
        ////////        ["Hot"] = new HighLowTemps { High = 60, Low = 20 }
        ////////    },
        ////////    SummaryWords = new[] { "Cool", "Windy", "Humid" }
        ////////};

        ////////string jsonString = JsonConvert.SerializeObject(weatherForecast, Formatting.Indented);

        ////////Console.WriteLine(jsonString);
        ////////Console.ReadLine();

        //////////// -------------------------------------------------------------
    }

}


//////public class xDetails
//////{
//////    public string ColumnABC { get; set; }
//////    public string ColumnDEF { get; set; }
//////    public string ColumnGHI { get; set; }
//////    public string ColumnXYZ { get; set; }

//////}


////////public class WeatherForecast
////////{
////////    public DateTimeOffset Date { get; set; }
////////    public int TemperatureCelsius { get; set; }
////////    public string Summary { get; set; }
////////    public string SummaryField;
////////    public IList<DateTimeOffset> DatesAvailable { get; set; }
////////    public Dictionary<string, HighLowTemps> TemperatureRanges { get; set; }
////////    public string[] SummaryWords { get; set; }
////////}

////////public class HighLowTemps
////////{
////////    public int High { get; set; }
////////    public int Low { get; set; }
////////}