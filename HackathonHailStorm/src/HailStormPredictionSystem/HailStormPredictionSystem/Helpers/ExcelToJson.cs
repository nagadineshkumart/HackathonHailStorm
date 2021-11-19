using ExcelDataReader;
using HailStorm.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HailStormPredictionSystem.Helpers
{
    public static class ExcelToJson
    {
        public static List<HailItem> ExcelToJsonConverter(string filePath)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var inFilePath = string.Format("{0}\\HailData.xlsx", filePath);
            var outFilePath = string.Format("{0}\\HailData.json", filePath);
            try
            {
                using (var inFile = File.Open(inFilePath, FileMode.Open, FileAccess.Read))
                using (var outFile = File.CreateText(outFilePath))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration()
                    { FallbackEncoding = Encoding.GetEncoding(1252) }))
                    using (var writer = new JsonTextWriter(outFile))
                    {
                        writer.Formatting = Formatting.Indented; //I likes it tidy
                        writer.WriteStartArray();
                        reader.Read(); //SKIP FIRST ROW, it's TITLES.
                        do
                        {
                            while (reader.Read())
                            {
                                   //peek ahead? Bail before we start anything so we don't get an empty object
                                   var status = reader.GetString(0);
                                if (string.IsNullOrEmpty(status)) break;

                                writer.WriteStartObject();
                                writer.WritePropertyName("LOCATION");
                                writer.WriteValue(status);
                                writer.WritePropertyName("STATE");
                                writer.WriteValue(Convert.ToString(reader.GetString(1)));
                                writer.WritePropertyName("STATENAME");
                                writer.WriteValue(Convert.ToString(reader.GetString(2)));

                                writer.WritePropertyName("DATE");
                                writer.WriteValue(Convert.ToString(reader.GetString(3)));

                                writer.WritePropertyName("MAGNITUDE");
                                writer.WriteValue(Convert.ToString(reader.GetString(4)));

                                writer.WriteEndObject();
                            }
                        } while (reader.NextResult());
                        writer.WriteEndArray();
                    }
                    string jsonString = File.ReadAllText(outFilePath);
                    var hailItems = JsonConvert.DeserializeObject<List<HailItem>>(jsonString);
                    return hailItems;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
