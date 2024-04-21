using Newtonsoft.Json.Linq;
using System.Text;

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
                string pathFull = Path.GetFullPath(filePathJSON.Text);
                string parentDir = Path.GetDirectoryName(pathFull) ?? string.Empty;

                List<string> resultList = TempConvertJsonToList(pathFull);

                string outputFileName = Path.GetFileNameWithoutExtension(pathFull) + "_converted.csv";
                string outputFilePath = Path.Combine(parentDir, outputFileName);
                File.WriteAllLines(outputFilePath, resultList);
                if (File.Exists(outputFilePath))
                {
                    MessageBox.Show("Conversion completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Conversion failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string pathFull = Path.GetFullPath(filePathJSON.Text);
                string parentDir = Path.GetDirectoryName(pathFull) ?? string.Empty;

                string resultString = ConvertJsonToCSV(pathFull);

                string outputFileName = Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted.csv";
                string outputFilePath = Path.Combine(parentDir, outputFileName);
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
        }

        private static string ConvertJsonToCSV(string filePathJson)
        {
            var json = File.ReadAllText(filePathJson);
            var data = JObject.Parse(json);

            // Extract the required information
            var cards = data["cards"]?.ToObject<JArray>();
            var checklists = data["checklists"]?.ToObject<JArray>();
            var lists = data["lists"]?.ToObject<JArray>();

            StringBuilder sb = new StringBuilder();
            if (cards != null)
            {
                StringBuilder cardBuilder = new StringBuilder();
                cardBuilder.AppendLine("Card Name,Card Description,Labels,List Name,Checklist,Checklist item");
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
                                if (checkItem == checklist["checkItems"].First)
                                {
                                    string checklistName = checklist["name"].ToString();
                                    checklistName = checklistName.Replace("\n", "");
                                    checklistName = checklistName.Replace("\"", "\'");
                                    string checkItemName = checkItem["name"].ToString();
                                    checkItemName = checkItemName.Replace("\n", "");
                                    checkItemName = checkItemName.Replace("\"", "\'");
                                    checklistBuilder.AppendFormat(",,,,\"{0}\",\"{1}\"", checklistName, checkItemName);
                                }
                                else
                                {
                                    string checkItemName = checkItem["name"].ToString();
                                    checkItemName = checkItemName.Replace("\n", "");
                                    checkItemName = checkItemName.Replace("\"", "\'");
                                    checklistBuilder.AppendFormat(",,,,,\"{0}\"", checkItemName);
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
        private static List<string> TempConvertJsonToList(string filePathJson)
        {
            var json = File.ReadAllText(filePathJson);
            var data = JObject.Parse(json);

            // Extract the required information
            var cards = data["cards"]?.ToObject<JArray>();
            var checklists = data["checklists"]?.ToObject<JArray>();
            var lists = data["lists"]?.ToObject<JArray>();

            List<string> result = new List<string>();

            StringBuilder sb = new StringBuilder();
            if (cards != null)
            {
                StringBuilder cardBuilder = new StringBuilder();
                cardBuilder.AppendLine("Card Name,Card Description,Labels,List Name,Checklist,Checklist item");
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

                    string listName = ",";
                    string ifListName = lists.FirstOrDefault(x => x["id"].ToString() == card["idList"].ToString())["name"].ToString();
                    if(ifListName != null)
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
                                if (checkItem == checklist["checkItems"].First)
                                {
                                    string checklistName = checklist["name"].ToString();
                                    checklistName = checklistName.Replace("\n", "");
                                    checklistName = checklistName.Replace("\"", "\'");
                                    string checkItemName = checkItem["name"].ToString();
                                    checkItemName = checkItemName.Replace("\n", "");
                                    checkItemName = checkItemName.Replace("\"", "\'");
                                    checklistBuilder.AppendFormat(",,,,\"{0}\",\"{1}\"", checklistName, checkItemName);
                                }
                                else
                                {
                                    string checkItemName = checkItem["name"].ToString();
                                    checkItemName = checkItemName.Replace("\n", "");
                                    checkItemName = checkItemName.Replace("\"", "\'");
                                    checklistBuilder.AppendFormat(",,,,,\"{0}\"", checkItemName);
                                }
                                cardBuilder.AppendLine(checklistBuilder.ToString());
                            }
                        }
                    }
                }
                result.Add(cardBuilder.ToString());
            }
            if (result.Count < 1)
            {
                MessageBox.Show("No data found in the JSON file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}