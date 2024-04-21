using CsvHelper;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using Ducktape.Trelloconverter.Models;
using System.Collections.Generic;

namespace TrelloConverter
{
    public partial class Converter : Form
    {
        public Converter()
        {
            InitializeComponent();
        }
        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JSON files (*.json)|*.json";
            openFileDialog1.Title = "Select a JSON file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePathJSON.Text = $"{openFileDialog1.FileName}";
            }
        }
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePathJSON.Text))
            {
                MessageBox.Show("Please select a file first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string pathFull = Path.GetFullPath(filePathJSON.Text);
            string resultString = ConvertJsonToCSV(pathFull);

            if (string.IsNullOrEmpty(resultString))
            {
                MessageBox.Show("No data found in the JSON file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string outputFileName = Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted.csv";
            string outputFilePath = Path.Combine(Path.GetDirectoryName(filePathJSON.Text), outputFileName);
            File.WriteAllText(outputFilePath, resultString);
            if (File.Exists(outputFilePath))
            {
                MessageBox.Show("Conversion completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Conversion failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string ConvertJsonToCSV(string filePathJson)
        {
            var json = File.ReadAllText(filePathJson);
            var data = JObject.Parse(json);

            // Extract the required information
            var cards = data["cards"]?.ToObject<JArray>();
            var checklists = data["checklists"]?.ToObject<JArray>();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Card Name,Card Description,Labels,Checklist,Checklist item");
            if (cards != null)
            {
                StringBuilder cardBuilder = new StringBuilder();
                foreach (var card in cards)
                {
                    StringBuilder headerBuilder = new StringBuilder();
                    StringBuilder labelBuilder = new StringBuilder();
                    foreach (var label in card["labels"])
                    {
                        labelBuilder.AppendFormat("{0} ({1})", label["name"], label["color"]);
                        if (label != card["labels"].Last)
                        {
                            labelBuilder.Append(' ');
                        }
                    }
                    string cardName = card["name"].ToString();
                    cardName = cardName.Replace("\n", "");
                    string cardDesc = card["desc"].ToString();
                    cardDesc = cardDesc.Replace("\n", "");
                    string cardLabels = labelBuilder.ToString();
                    headerBuilder.AppendFormat("\"{0}\",\"{1}\",\"{2}\",,", cardName, cardDesc, cardLabels);
                    cardBuilder.AppendLine(headerBuilder.ToString());

                    foreach (var checklist in checklists)
                    {
                        if (checklist["idCard"].ToString() == card["id"].ToString())
                        {
                            foreach (var checkItem in checklist["checkItems"])
                            {
                                StringBuilder checklistBuilder = new StringBuilder();
                                if (checkItem == checklist["checkItems"].First)
                                {
                                    string checklistName = checklist["name"].ToString();
                                    checklistName = checklistName.Replace("\n", "");
                                    checklistName = checklistName.Replace("\"", "\'");
                                    string checkItemName = checkItem["name"].ToString();
                                    checkItemName = checkItemName.Replace("\n", "");
                                    checkItemName = checkItemName.Replace("\"", "\'");
                                    checklistBuilder.AppendFormat(",,,\"{0}\",\"{1}\"", checklistName, checkItemName);
                                }
                                else
                                {
                                    string checkItemName = checkItem["name"].ToString();
                                    checkItemName = checkItemName.Replace("\n", "");
                                    checkItemName = checkItemName.Replace("\"", "\'");
                                    checklistBuilder.AppendFormat(",,,,\"{0}\"", checkItemName);
                                }
                                cardBuilder.AppendLine(checklistBuilder.ToString());
                            }
                        }
                    }
                }
                sb.Append(cardBuilder);
            }
            return sb.ToString();
        }
    }
}

//private void ConvertTwo()
//{
//    string jsonPath = filePathJSON.Text;
//    var json = File.ReadAllText(jsonPath);
//    var data = JObject.Parse(json);

//    // Extract the required information
//    var cards = data["cards"]?.ToObject<JArray>();
//    var labels = data["labels"]?.ToObject<JObject>();
//    var checklists = data["checklists"]?.ToObject<JArray>();

//    StringBuilder sb = new StringBuilder();
//    sb.AppendLine("Card Name,Card Description,Labels,Checklist,Checklist item");
//    if (cards != null)
//    {
//        StringBuilder cardBuilder = new StringBuilder();
//        foreach (var card in cards)
//        {
//            StringBuilder headerBuilder = new StringBuilder();
//            StringBuilder labelBuilder = new StringBuilder();
//            foreach (var label in card["labels"])
//            {
//                labelBuilder.AppendFormat("{0} ({1})", label["name"], label["color"]);
//                if (label != card["labels"].Last)
//                {
//                    labelBuilder.Append(' ');
//                }
//            }
//            string cardName = card["name"].ToString();
//            cardName = cardName.Replace("\n", "");
//            string cardDesc = card["desc"].ToString();
//            cardDesc = cardDesc.Replace("\n", "");
//            string cardLabels = labelBuilder.ToString();
//            headerBuilder.AppendFormat("\"{0}\",\"{1}\",\"{2}\",,", cardName, cardDesc, cardLabels);
//            cardBuilder.AppendLine(headerBuilder.ToString());

//            foreach (var checklist in checklists)
//            {
//                if (checklist["idCard"].ToString() == card["id"].ToString())
//                {
//                    foreach (var checkItem in checklist["checkItems"])
//                    {
//                        StringBuilder checklistBuilder = new StringBuilder();
//                        if (checkItem == checklist["checkItems"].First)
//                        {
//                            string checklistName = checklist["name"].ToString();
//                            checklistName = checklistName.Replace("\n", "");
//                            string checkItemName = checkItem["name"].ToString();
//                            checkItemName = checkItemName.Replace("\n", "");
//                            checklistBuilder.AppendFormat(",,,\"{0}\",\"{1}\"", checklistName, checkItemName);
//                        }
//                        else
//                        {
//                            string checkItemName = checkItem["name"].ToString();
//                            checkItemName = checkItemName.Replace("\n", "");
//                            checklistBuilder.AppendFormat(",,,,\"{0}\"", checkItemName);
//                        }
//                        cardBuilder.AppendLine(checklistBuilder.ToString());
//                    }
//                }
//            }
//        }
//        sb.Append(cardBuilder);
//    }
//    string result = sb.ToString();
//    Console.WriteLine(result);
//}