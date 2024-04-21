using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TrelloConverter
{
    public partial class Converter : Form
    {
        private string filePathInput = "";
        private string filePathDir = "";
        private string filePathOutputCSV = "";
        private string filePathOutputMD = "";
        private bool mustEnumerate = false;
        private bool mustGenerateMarkdown = false;
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
            filePathInput = Path.GetFullPath(filePathJSON.Text);
            filePathDir = Path.GetDirectoryName(filePathJSON.Text) ?? string.Empty;
            filePathOutputCSV = Path.Combine(filePathDir, Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted.csv");
            filePathOutputMD = Path.Combine(filePathDir, Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted.md");

            ConversionInputCheck();

            List<string> JsonAsList = ConvertJsonToList(filePathInput);

            if (mustEnumerate)
            {
                File.WriteAllLines(filePathOutputCSV, JsonAsList);
            }

            if (mustGenerateMarkdown)
            {
                List<string> ListMarkdown = FormatListToMarkdown(JsonAsList);
                File.WriteAllLines(filePathOutputMD, ListMarkdown);
            }

            if (!mustEnumerate && !mustGenerateMarkdown)
            {
                File.WriteAllLines(filePathOutputCSV, JsonAsList);
            }

            ConversionOutputCheck();
        }
        private static List<string> ConvertJsonToList(string filePathJson)
        {
            var json = File.ReadAllText(filePathJson);
            var data = JObject.Parse(json);

            // Extract the required information
            var cards = data["cards"]?.ToObject<JArray>();
            var checklists = data["checklists"]?.ToObject<JArray>();
            var lists = data["lists"]?.ToObject<JArray>();

            List<string> result = [];
            if (cards != null)
            {
                result.Add("Card Name,Card Description,Labels,List Name,Checklist,Checklist item");
                foreach (var card in cards)
                {
                    StringBuilder cardBuilder = new StringBuilder();
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

                    string listName = ",";
                    string ifListName = lists.FirstOrDefault(x => x["id"].ToString() == card["idList"].ToString())["name"].ToString();
                    if (ifListName != null)
                    {
                        listName = ifListName;
                    }

                    headerBuilder.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",,,", cardName, cardDesc, cardLabels, listName);
                    cardBuilder.AppendLine(headerBuilder.ToString());

                    foreach (var checklist in checklists)
                    {
                        if (checklist["idCard"].ToString() == card["id"].ToString())
                        {
                            foreach (var checkItem in checklist["checkItems"])
                            {
                                StringBuilder checklistBuilder = new StringBuilder();
                                string checklistName = checklist["name"].ToString();
                                checklistName = checklistName.Replace("\n", "");
                                checklistName = checklistName.Replace("\"", "\'");
                                string checkItemName = checkItem["name"].ToString();
                                checkItemName = checkItemName.Replace("\n", "");
                                checkItemName = checkItemName.Replace("\"", "\'");
                                checklistBuilder.AppendFormat(",,,,\"{0}\",\"{1}\"", checklistName, checkItemName);
                                cardBuilder.AppendLine(checklistBuilder.ToString());
                            }
                        }
                    }
                    result.Add(cardBuilder.ToString());
                }
            }
            if (result.Count < 1)
            {
                MessageBox.Show("No data found in the JSON file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
        public static List<string> FormatListToMarkdown(List<string> listInput)
        {
            List<string> csvList = [];
            if (listInput[0] == "Card Name,Card Description,Labels,List Name,Checklist,Checklist item")
            {
                listInput.RemoveAt(0);
            }
            foreach (var line in listInput)
            {
                // Regex to extract parts of the input
                var regex = new Regex("\"([^\"]+)\",\"([^\"]+)\",\"([^\"]+)\",\"([^\"]+)\",\"Tasks\",\"([^\"]+)\",\"Acceptance Criteria\",\"([^\"]+)\"");
                var match = regex.Match(line);

                if (!match.Success)
                {
                    break;
                }

                // Extracting data
                string titleSection = match.Groups[1].Value;
                string userStory = match.Groups[2].Value;
                string estimate = match.Groups[3].Value.Split(' ')[0].Trim();
                string sprint = match.Groups[4].Value.Split(' ')[1].Trim();
                string tasks = match.Groups[5].Value;
                string acceptanceCriteria = match.Groups[6].Value;

                // Format the title
                var titleRegex = new Regex("US (\\d+) (.*) \\((.*)\\)");
                var titleMatch = titleRegex.Match(titleSection);
                string number = titleMatch.Groups[1].Value;
                string function = titleMatch.Groups[2].Value;
                string role = titleMatch.Groups[3].Value;

                // Prepare output
                string formattedTitle = $"### {number} - {function} ({role})\n";
                string formattedUserStory = $"{userStory}\n\n";
                string formattedTasks = $"**Tasks:**\n- {tasks}\n\n";
                string formattedCriteria = $"**Acceptance Criteria:**\n- {acceptanceCriteria}\n\n";
                string formattedEstimate = $"**Estimate:** {estimate}\n\n";
                string formattedSprint = $"**Sprint:** Sprint {sprint}";

                csvList.Add(formattedTitle + formattedUserStory + formattedTasks + formattedCriteria + formattedEstimate + formattedSprint);
            }
            return csvList;
        }
        private void ConversionInputCheck()
        {
            if (string.IsNullOrEmpty(filePathJSON.Text))
            {
                MessageBox.Show("Please select a file first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (Path.GetExtension(filePathJSON.Text) != ".json")
            {
                MessageBox.Show("Please select a JSON file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (!File.Exists(filePathJSON.Text))
            {
                MessageBox.Show("File does not exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (new FileInfo(filePathJSON.Text).Length == 0)
            {
                MessageBox.Show("File is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (new FileInfo(filePathJSON.Text).Length > 1000000)
            {
                MessageBox.Show("File is too large", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void ConversionOutputCheck()
        {
            if (File.Exists(filePathOutputCSV))
            {
                MessageBox.Show("Conversion completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Conversion failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Enumerate_CheckedChanged(object sender, EventArgs e)
        {
            mustEnumerate = enumerate.Checked;
        }
        private void generateMarkdown_CheckedChanged(object sender, EventArgs e)
        {
            mustGenerateMarkdown = generateMarkdown.Checked;
        }
    }
}
        //private static string ConvertJsonToCSV(string filePathJson)
        //{
        //    var json = File.ReadAllText(filePathJson);
        //    var data = JObject.Parse(json);

        //    // Extract the required information
        //    var cards = data["cards"]?.ToObject<JArray>();
        //    var checklists = data["checklists"]?.ToObject<JArray>();
        //    var lists = data["lists"]?.ToObject<JArray>();

        //    StringBuilder sb = new StringBuilder();
        //    if (cards != null)
        //    {
        //        StringBuilder cardBuilder = new StringBuilder();
        //        cardBuilder.AppendLine("Card Name,Card Description,Labels,List Name,Checklist,Checklist item");
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

        //            string listName = ",";
        //            string ifListName = lists.FirstOrDefault(x => x["id"].ToString() == card["idList"].ToString())["name"].ToString();
        //            if (ifListName != null)
        //            {
        //                listName = ifListName;
        //            }

        //            headerBuilder.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",,,", cardName, cardDesc, cardLabels, listName);
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
        //                            checklistName = checklistName.Replace("\"", "\'");
        //                            string checkItemName = checkItem["name"].ToString();
        //                            checkItemName = checkItemName.Replace("\n", "");
        //                            checkItemName = checkItemName.Replace("\"", "\'");
        //                            checklistBuilder.AppendFormat(",,,,\"{0}\",\"{1}\"", checklistName, checkItemName);
        //                        }
        //                        else
        //                        {
        //                            string checkItemName = checkItem["name"].ToString();
        //                            checkItemName = checkItemName.Replace("\n", "");
        //                            checkItemName = checkItemName.Replace("\"", "\'");
        //                            checklistBuilder.AppendFormat(",,,,,\"{0}\"", checkItemName);
        //                        }
        //                        cardBuilder.AppendLine(checklistBuilder.ToString());
        //                    }
        //                }
        //            }
        //        }
        //        sb.Append(cardBuilder);
        //    }
        //    return sb.ToString();
        //}