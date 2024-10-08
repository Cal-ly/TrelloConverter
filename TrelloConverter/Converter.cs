﻿using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TrelloConverter.Models;

namespace TrelloConverter
{
    /// <summary>
    /// The Converter class handles the conversion of Trello data from JSON format to various formats such as CSV, Markdown, and LaTeX.
    /// </summary>
    public partial class Converter : Form
    {
        private string filePathInput = "";
        private string filePathDir = "";
        private string filePathOutputCSV = "";
        private string filePathOutputCSVrev = "";
        private string filePathOutputMD = "";
        private string filePathOutputLATEX = "";
        private bool mustKeepFormat = false;
        private bool mustDeEnumerate = false;
        private bool mustEnumerate = true;
        private bool mustGenerateMarkdown = true;
        private bool mustGenerateLATEX = true;
        private bool mustReverseOrder = true;
        private bool mustCloseAfterConv = true;
        private const string patternUS = @"^US(-\d{3}|\s+\d+)\s+";

        /// <summary>
        /// Initializes a new instance of the <see cref="Converter"/> class.
        /// </summary>
        public Converter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event handler for the ChooseFileButton click event. Opens a file dialog for the user to select a JSON file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void ChooseFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JSON files (*.json)|*.json";
            openFileDialog1.Title = "Select a JSON file";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePathJSON.Text = $"{openFileDialog1.FileName}";
            }
        }

        /// <summary>
        /// Event handler for the ConvertButton click event. Initiates the conversion process based on user selections.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            filePathInput = Path.GetFullPath(filePathJSON.Text);
            filePathDir = Path.GetDirectoryName(filePathJSON.Text) ?? string.Empty;
            filePathOutputCSV = Path.Combine(filePathDir, Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted.csv");
            filePathOutputCSVrev = Path.Combine(filePathDir, Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted_reversed.csv");
            filePathOutputMD = Path.Combine(filePathDir, Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted.md");
            filePathOutputLATEX = Path.Combine(filePathDir, Path.GetFileNameWithoutExtension(filePathJSON.Text) + "_converted.tex");

            ConversionInputCheck();

            List<Card> jsonList = LoadJsonToObject(filePathInput);

            if (mustKeepFormat)
            {
                WriteCSVtoFile(jsonList, filePathOutputCSV);
                Process.Start("explorer.exe", $"/select,\"{filePathOutputCSV}\"");
                return;
            }

            jsonList = jsonList.OrderBy(x => x.ListName).ThenBy(x => x.ReorderPosition).ToList();
            jsonList = PrepForConversion(jsonList);

            if (mustDeEnumerate && !mustEnumerate)
            {
                RemoveUsPrefix(jsonList);
            }

            if (mustEnumerate || (mustEnumerate && mustDeEnumerate))
            {
                RemoveUsPrefix(jsonList);
                AddUsPrefix(jsonList);
            }

            if (mustReverseOrder)
            {
                WriteCSVtoFile(ReverseOrder(jsonList), filePathOutputCSVrev);
            }

            if (mustGenerateMarkdown)
            {
                WriteMarkdownToFile(jsonList, filePathOutputMD);
            }

            if (mustGenerateLATEX)
            {
                WriteLATEXToFile(jsonList, filePathOutputLATEX);
            }

            WriteCSVtoFile(jsonList, filePathOutputCSV);

            if (ConversionOutputCheck())
            {
                Process.Start("explorer.exe", $"/select,\"{filePathOutputCSV}\"");
                if (mustCloseAfterConv)
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Loads JSON data from a file and converts it into a list of <see cref="Card"/> objects.
        /// </summary>
        /// <param name="filePathJson">The path to the JSON file to load.</param>
        /// <returns>A list of <see cref="Card"/> objects representing the data in the JSON file.</returns>
        private static List<Card> LoadJsonToObject(string filePathJson)
        {
            List<Card> cards = new List<Card>();
            var trelloData = new TrelloData();
            using (StreamReader file = File.OpenText(filePathJson))
            {
                JsonSerializer serializer = new JsonSerializer();
                trelloData = (TrelloData?)serializer.Deserialize(file, typeof(TrelloData));
            }

            if (trelloData?.Cards == null)
            {
                return cards;
            }

            int cardIndexer = 1;
            foreach (var card in trelloData.Cards)
            {
                if (card.Closed)
                {
                    continue;
                }
                Card newCard = new();
                newCard.Name = card.Name ?? string.Empty;
                newCard.Desc = card.Desc ?? string.Empty;
                newCard.Labels = new List<Models.Label>();
                newCard.ListName = trelloData.Lists?.FirstOrDefault(x => x.Id == card.IdList)?.Name ?? string.Empty;
                foreach (var label in card.Labels ?? new List<Models.Label>())
                {
                    Models.Label newLabel = new();
                    newLabel.Name = label.Name;
                    newLabel.Color = label.Color;
                    newCard.Labels.Add(newLabel);
                }
                foreach (var checklist in trelloData.Checklists ?? Enumerable.Empty<Checklist>())
                {
                    if (checklist.IdCard == card.Id)
                    {
                        if (newCard.Checklists == null)
                        {
                            newCard.Checklists = new List<Checklist>();
                            Checklist newChecklist = new();
                            newChecklist.Name = checklist.Name;
                            newChecklist.CheckItems = new List<CheckItem>();
                            foreach (var checkItem in checklist.CheckItems ?? Enumerable.Empty<CheckItem>())
                            {
                                CheckItem newCheckItem = new();
                                newCheckItem.Name = checkItem.Name;
                                newChecklist.CheckItems.Add(newCheckItem);
                            }
                            newCard.Checklists.Add(newChecklist);
                        }
                        else if (newCard.Checklists.Any(x => x.Id == checklist.Id))
                        {
                            foreach (var cardChecklist in newCard.Checklists)
                            {
                                if (cardChecklist.Name == checklist.Name)
                                {
                                    foreach (var checkItem in checklist.CheckItems ?? Enumerable.Empty<CheckItem>())
                                    {
                                        CheckItem newCheckItem = new();
                                        newCheckItem.Name = checkItem.Name;
                                        cardChecklist.CheckItems?.Add(newCheckItem);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Checklist newChecklist = new();
                            newChecklist.Name = checklist.Name;
                            newChecklist.CheckItems = new List<CheckItem>();
                            foreach (var checkItem in checklist.CheckItems ?? Enumerable.Empty<CheckItem>())
                            {
                                CheckItem newCheckItem = new();
                                newCheckItem.Name = checkItem.Name;
                                newChecklist.CheckItems.Add(newCheckItem);
                            }
                            newCard.Checklists.Add(newChecklist);
                        }
                    }
                }
                newCard.ReorderPosition = cardIndexer++;
                cards.Add(newCard);
            }
            return cards;
        }

        /// <summary>
        /// Writes a list of <see cref="Card"/> objects to a CSV file.
        /// </summary>
        /// <param name="cards">The list of cards to write.</param>
        /// <param name="filePathOutput">The path to the output CSV file.</param>
        private static void WriteCSVtoFile(List<Card> cards, string filePathOutput)
        {
            using StreamWriter file = new StreamWriter(filePathOutput, append: false);
            file.WriteLine("Card Name,Card Description,Labels,List Name,Checklist,Checklist item");
            foreach (var newCard in cards)
            {
                string labelJoin = string.Empty;
                foreach (var label in newCard.Labels ?? Enumerable.Empty<Models.Label>())
                {
                    labelJoin += $"{label.Name} ({label.Color})";
                    if (newCard.Labels != null && newCard.Labels.Count > 1 && label != newCard.Labels.Last())
                    {
                        labelJoin += ",";
                    }
                }
                if (string.IsNullOrEmpty(newCard.ListName))
                {
                    file.WriteLine("\"{0}\",\"{1}\",\"{2}\",,,,", newCard.Name, newCard.Desc, labelJoin);
                }
                else
                {
                    file.WriteLine("\"{0}\",\"{1}\",\"{2}\",\"{3}\",,,", newCard.Name, newCard.Desc, labelJoin, newCard.ListName);
                }
                if (newCard.Checklists != null)
                {
                    foreach (var checklist in newCard.Checklists)
                    {
                        foreach (var checkItem in checklist.CheckItems ?? Enumerable.Empty<CheckItem>())
                        {
                            file.WriteLine(",,,,\"{0}\",\"{1}\"", checklist.Name, checkItem.Name);
                        }
                    }
                }
            }
            file.Close();
        }

        /// <summary>
        /// Writes a list of <see cref="Card"/> objects to a Markdown file.
        /// </summary>
        /// <param name="cards">The list of cards to write.</param>
        /// <param name="filePathOutput">The path to the output Markdown file.</param>
        private static void WriteMarkdownToFile(List<Card> cards, string filePathOutput)
        {
            using StreamWriter file = new StreamWriter(filePathOutput, append: false);

            foreach (var card in cards)
            {
                file.WriteLine($"### {card.Name}");
                file.WriteLine($"{card.Desc}\n");
                if (card.Checklists != null)
                {
                    foreach (var checklist in card.Checklists)
                    {
                        int indexer = 1;
                        file.WriteLine($"**{checklist.Name}:**");
                        foreach (var checkItem in checklist.CheckItems ?? Enumerable.Empty<CheckItem>())
                        {
                            file.WriteLine($"{indexer}. {checkItem.Name}");
                            indexer++;
                        }
                        file.WriteLine("");
                    }
                }
                string labelDescriptions = string.Join(", ", card.Labels?.Select(label => $"{label.Name} ({label.Color})") ?? Enumerable.Empty<string>());
                file.WriteLine($"**Estimate:** {labelDescriptions}");
                file.WriteLine();
                file.WriteLine($"**Sprint:** {card.ListName}\n");
                file.WriteLine();
                file.WriteLine("---");
                file.WriteLine();
            }

            file.Close();
        }

        /// <summary>
        /// Writes a list of <see cref="Card"/> objects to a LaTeX file.
        /// </summary>
        /// <param name="cards">The list of cards to write.</param>
        /// <param name="filePathOutput">The path to the output LaTeX file.</param>
        private static void WriteLATEXToFile(List<Card> cards, string filePathOutput)
        {
            List<Card> latexList = new(cards);
            RemoveUsPrefix(latexList);
            AddUsPrefix(latexList);
            using StreamWriter file = new StreamWriter(filePathOutput, append: false);
            file.WriteLine(@"\section{Alle User Stories}");
            file.WriteLine(@"\label{sec:all-user-stories}");

            foreach (var card in latexList)
            {
                file.WriteLine("");
                file.WriteLine($@"\subsection{{{card.Name}}}");
                string userStoryLabel = card.Name ?? string.Empty;
                userStoryLabel = userStoryLabel.Split(" ")[0] ?? string.Empty;
                if (!string.IsNullOrEmpty(userStoryLabel))
                {
                    userStoryLabel = userStoryLabel.Trim();
                    file.WriteLine($@"\label{{sec:{userStoryLabel}}}");
                }
                file.WriteLine($@"\textit{{{card.Desc}}}");

                if (card.Checklists != null && card.Checklists.Count > 0)
                {
                    foreach (var checklist in card.Checklists)
                    {
                        file.WriteLine($@"\subsubsection*{{\textbf{{{checklist.Name}}}}}");
                        file.WriteLine(@"\begin{enumerate}");
                        foreach (var item in checklist.CheckItems ?? Enumerable.Empty<CheckItem>())
                        {
                            file.WriteLine($@"  \item {item.Name}");
                        }
                        file.WriteLine(@"\end{enumerate}");
                    }
                }

                if (card.Labels != null && card.Labels.Count > 0)
                {
                    string stringLabels = string.Empty;
                    foreach (var label in card.Labels)
                    {
                        stringLabels += $@" \colorbox{{{label.CorrectedColor}}}{{{label.Name} ({label.Color})}}";
                        if (card.Labels.Count > 1 && label != card.Labels.Last())
                        {
                            stringLabels += ",";
                        }
                    }
                    file.Write($@"\textbf{{Estimate:}}");
                    file.WriteLine(stringLabels);
                }
                file.WriteLine($@"\textbf{{Placed: {card.ListName}}}");
                file.WriteLine(@"\par\noindent\dotfill");
            }
            file.Close();
        }

        /// <summary>
        /// Prepares a list of <see cref="Card"/> objects for conversion by cleaning their data.
        /// </summary>
        /// <param name="cards">The list of cards to prepare.</param>
        /// <returns>A list of prepared <see cref="Card"/> objects.</returns>
        private static List<Card> PrepForConversion(List<Card> cards)
        {
            List<Card> prepList = new(cards);
            foreach (var card in prepList)
            {
                card.Name = ScourString(card.Name ?? string.Empty);
                card.Desc = ScourString(card.Desc ?? string.Empty);
                if (card.Checklists != null)
                {
                    foreach (Checklist checklist in card.Checklists)
                    {
                        checklist.Name = ScourString(checklist.Name ?? string.Empty);
                        if (checklist.CheckItems != null)
                        {
                            foreach (CheckItem checkItem in checklist.CheckItems)
                            {
                                checkItem.Name = ScourString(checkItem.Name ?? string.Empty);
                            }
                        }
                    }
                }
                if (card.Labels != null)
                {
                    foreach (var label in card.Labels)
                    {
                        switch (label.Name)
                        {
                            case "1":
                                label.CorrectedColor = "green";
                                label.Color = "green";
                                break;
                            case "2":
                                label.CorrectedColor = "lime";
                                label.Color = "lime";
                                break;
                            case "3":
                                label.CorrectedColor = "yellow";
                                label.Color = "yellow";
                                break;
                            case "5":
                                label.CorrectedColor = "orange";
                                label.Color = "orange";
                                break;
                            case "8":
                                label.CorrectedColor = "red";
                                label.Color = "red";
                                break;
                            default:
                                label.CorrectedColor = "white";
                                break;
                        }
                    }
                }
            }
            return prepList;
        }

        /// <summary>
        /// Reverses the order of a list of <see cref="Card"/> objects.
        /// </summary>
        /// <param name="cards">The list of cards to reverse.</param>
        /// <returns>A list of <see cref="Card"/> objects in reversed order.</returns>
        private static List<Card> ReverseOrder(List<Card> cards)
        {
            List<Card> reverseList = new(cards);
            return reverseList.OrderBy(x => x.ListName).ThenByDescending(x => x.ReorderPosition).ToList();
        }

        /// <summary>
        /// Removes the "US" prefix from the names of the cards in the list.
        /// </summary>
        /// <param name="cards">The list of <see cref="Card"/> objects from which to remove the prefix.</param>
        public static void RemoveUsPrefix(List<Card> cards)
        {
            foreach (var card in cards)
            {
                if (!string.IsNullOrEmpty(card.Name))
                {
                    card.Name = Regex.Replace(card.Name, patternUS, "");
                }
            }
        }

        /// <summary>
        /// Adds a "US" prefix to the names of the cards in the list.
        /// </summary>
        /// <param name="jsonList">The list of <see cref="Card"/> objects to which to add the prefix.</param>
        public static void AddUsPrefix(List<Card> jsonList)
        {
            int cardIndexer = 1;
            foreach (var card in jsonList)
            {
                cardIndexer = card.ReorderPosition ?? cardIndexer;
                string formattedIndex = $"US-{cardIndexer:000}";
                card.Name = $"{formattedIndex} {card.Name}";
                cardIndexer++;
            }
        }

        /// <summary>
        /// Cleans a string by removing certain characters.
        /// </summary>
        /// <param name="input">The input string to clean.</param>
        /// <returns>The cleaned string.</returns>
        private static string ScourString(string input)
        {
            input = input.Replace("*", "");
            input = input.Replace("_", "");
            input = input.Replace("\"", "'");
            input = input.Replace("\n", "");
            return input;
        }

        /// <summary>
        /// Checks the validity of the input JSON file path.
        /// </summary>
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
        }

        /// <summary>
        /// Checks the existence of the output CSV file and displays an appropriate message.
        /// </summary>
        /// <returns><c>true</c> if the file exists; otherwise, <c>false</c>.</returns>
        private bool ConversionOutputCheck()
        {
            if (File.Exists(filePathOutputCSV))
            {
                MessageBox.Show("Conversion completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Conversion failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Event handler for the Enumerate checkbox change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Enumerate_CheckedChanged(object sender, EventArgs e)
        {
            mustEnumerate = enumerate.Checked;
        }

        /// <summary>
        /// Event handler for the generateMarkdown checkbox change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void generateMarkdown_CheckedChanged(object sender, EventArgs e)
        {
            mustGenerateMarkdown = generateMarkdown.Checked;
        }

        /// <summary>
        /// Event handler for the deEnumerate checkbox change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void deEnumerate_CheckedChanged(object sender, EventArgs e)
        {
            mustDeEnumerate = deEnumerate.Checked;
        }

        /// <summary>
        /// Event handler for the reverseOrder checkbox change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void reverseOrder_CheckedChanged(object sender, EventArgs e)
        {
            mustReverseOrder = reverseOrder.Checked;
        }

        /// <summary>
        /// Event handler for the closeOnSucces checkbox change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void closeOnSucces_CheckedChanged(object sender, EventArgs e)
        {
            mustCloseAfterConv = closeOnSucces.Checked;
        }

        /// <summary>
        /// Event handler for the generateLATEX checkbox change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void generateLATEX_CheckedChanged(object sender, EventArgs e)
        {
            mustGenerateLATEX = generateLATEX.Checked;
        }

        /// <summary>
        /// Event handler for the originalFormat checkbox change event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void originalFormat_CheckedChanged(object sender, EventArgs e)
        {
            mustKeepFormat = originalFormat.Checked;
        }
    }
}