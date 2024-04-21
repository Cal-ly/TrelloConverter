namespace TrelloConverter
{
    partial class Converter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chooseFileButton = new Button();
            openFileDialog1 = new OpenFileDialog();
            filePathJSON = new TextBox();
            convertButton = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // chooseFileButton
            // 
            chooseFileButton.Location = new Point(581, 119);
            chooseFileButton.Name = "chooseFileButton";
            chooseFileButton.Size = new Size(112, 34);
            chooseFileButton.TabIndex = 0;
            chooseFileButton.Text = "Choose File";
            chooseFileButton.UseVisualStyleBackColor = true;
            chooseFileButton.Click += ChooseFileButton_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // filePathJSON
            // 
            filePathJSON.BorderStyle = BorderStyle.FixedSingle;
            filePathJSON.Location = new Point(12, 82);
            filePathJSON.Multiline = true;
            filePathJSON.Name = "filePathJSON";
            filePathJSON.Size = new Size(799, 31);
            filePathJSON.TabIndex = 1;
            // 
            // convertButton
            // 
            convertButton.Location = new Point(699, 119);
            convertButton.Name = "convertButton";
            convertButton.Size = new Size(112, 34);
            convertButton.TabIndex = 2;
            convertButton.Text = "Convert";
            convertButton.UseVisualStyleBackColor = true;
            convertButton.Click += ConvertButton_Click;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(799, 64);
            textBox1.TabIndex = 4;
            textBox1.Text = "Trello Converter. This WinForm app has been developed for converting Trello exports in JSON format into a more malleable CSV and markdown files\r\n";
            // 
            // Converter
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 180);
            Controls.Add(textBox1);
            Controls.Add(convertButton);
            Controls.Add(filePathJSON);
            Controls.Add(chooseFileButton);
            Name = "Converter";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button chooseFileButton;
        private OpenFileDialog openFileDialog1;
        private TextBox filePathJSON;
        private Button convertButton;
        private TextBox textBox1;
    }
}