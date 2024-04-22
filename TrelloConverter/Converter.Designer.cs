namespace TrelloConverter;

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Converter));
        chooseFileButton = new Button();
        openFileDialog1 = new OpenFileDialog();
        filePathJSON = new TextBox();
        convertButton = new Button();
        textBox1 = new TextBox();
        enumerate = new CheckBox();
        generateMarkdown = new CheckBox();
        deEnumerate = new CheckBox();
        reverseOrder = new CheckBox();
        closeOnSucces = new CheckBox();
        generateLATEX = new CheckBox();
        originalFormat = new CheckBox();
        SuspendLayout();
        // 
        // chooseFileButton
        // 
        chooseFileButton.Location = new Point(612, 201);
        chooseFileButton.Name = "chooseFileButton";
        chooseFileButton.Size = new Size(147, 34);
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
        filePathJSON.Location = new Point(12, 135);
        filePathJSON.Multiline = true;
        filePathJSON.Name = "filePathJSON";
        filePathJSON.Size = new Size(747, 32);
        filePathJSON.TabIndex = 1;
        // 
        // convertButton
        // 
        convertButton.Location = new Point(612, 241);
        convertButton.Name = "convertButton";
        convertButton.Size = new Size(147, 34);
        convertButton.TabIndex = 2;
        convertButton.Text = "Convert";
        convertButton.UseVisualStyleBackColor = true;
        convertButton.Click += ConvertButton_Click;
        // 
        // textBox1
        // 
        textBox1.BackColor = SystemColors.ControlDark;
        textBox1.BorderStyle = BorderStyle.None;
        textBox1.Location = new Point(12, 12);
        textBox1.Multiline = true;
        textBox1.Name = "textBox1";
        textBox1.ReadOnly = true;
        textBox1.Size = new Size(747, 106);
        textBox1.TabIndex = 4;
        textBox1.Text = resources.GetString("textBox1.Text");
        // 
        // enumerate
        // 
        enumerate.AutoSize = true;
        enumerate.Checked = true;
        enumerate.CheckState = CheckState.Checked;
        enumerate.Location = new Point(325, 205);
        enumerate.Name = "enumerate";
        enumerate.Size = new Size(128, 29);
        enumerate.TabIndex = 5;
        enumerate.Text = "Enumerate";
        enumerate.UseVisualStyleBackColor = true;
        enumerate.CheckedChanged += Enumerate_CheckedChanged;
        // 
        // generateMarkdown
        // 
        generateMarkdown.AutoSize = true;
        generateMarkdown.Checked = true;
        generateMarkdown.CheckState = CheckState.Checked;
        generateMarkdown.Location = new Point(12, 245);
        generateMarkdown.Name = "generateMarkdown";
        generateMarkdown.Size = new Size(151, 29);
        generateMarkdown.TabIndex = 6;
        generateMarkdown.Text = "Generate .md";
        generateMarkdown.UseVisualStyleBackColor = true;
        generateMarkdown.CheckedChanged += generateMarkdown_CheckedChanged;
        // 
        // deEnumerate
        // 
        deEnumerate.AutoSize = true;
        deEnumerate.Location = new Point(167, 205);
        deEnumerate.Name = "deEnumerate";
        deEnumerate.Size = new Size(152, 29);
        deEnumerate.TabIndex = 7;
        deEnumerate.Text = "UnEnumerate";
        deEnumerate.UseVisualStyleBackColor = true;
        deEnumerate.CheckedChanged += deEnumerate_CheckedChanged;
        // 
        // reverseOrder
        // 
        reverseOrder.AutoSize = true;
        reverseOrder.Checked = true;
        reverseOrder.CheckState = CheckState.Checked;
        reverseOrder.Location = new Point(459, 205);
        reverseOrder.Name = "reverseOrder";
        reverseOrder.Size = new Size(147, 29);
        reverseOrder.TabIndex = 8;
        reverseOrder.Text = "Import ready";
        reverseOrder.UseVisualStyleBackColor = true;
        reverseOrder.CheckedChanged += reverseOrder_CheckedChanged;
        // 
        // closeOnSucces
        // 
        closeOnSucces.AutoSize = true;
        closeOnSucces.Checked = true;
        closeOnSucces.CheckState = CheckState.Checked;
        closeOnSucces.Location = new Point(325, 245);
        closeOnSucces.Name = "closeOnSucces";
        closeOnSucces.Size = new Size(168, 29);
        closeOnSucces.TabIndex = 9;
        closeOnSucces.Text = "Close on succes";
        closeOnSucces.UseVisualStyleBackColor = true;
        closeOnSucces.CheckedChanged += closeOnSucces_CheckedChanged;
        // 
        // generateLATEX
        // 
        generateLATEX.AutoSize = true;
        generateLATEX.Checked = true;
        generateLATEX.CheckState = CheckState.Checked;
        generateLATEX.Location = new Point(169, 245);
        generateLATEX.Name = "generateLATEX";
        generateLATEX.Size = new Size(150, 29);
        generateLATEX.TabIndex = 10;
        generateLATEX.Text = "Generate .tex";
        generateLATEX.UseVisualStyleBackColor = true;
        generateLATEX.CheckedChanged += generateLATEX_CheckedChanged;
        // 
        // originalFormat
        // 
        originalFormat.AutoSize = true;
        originalFormat.Location = new Point(12, 205);
        originalFormat.Name = "originalFormat";
        originalFormat.Size = new Size(144, 29);
        originalFormat.TabIndex = 11;
        originalFormat.Text = "Keep Format";
        originalFormat.UseVisualStyleBackColor = true;
        originalFormat.CheckedChanged += originalFormat_CheckedChanged;
        // 
        // Converter
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ControlDark;
        ClientSize = new Size(771, 287);
        Controls.Add(originalFormat);
        Controls.Add(generateLATEX);
        Controls.Add(closeOnSucces);
        Controls.Add(reverseOrder);
        Controls.Add(deEnumerate);
        Controls.Add(generateMarkdown);
        Controls.Add(enumerate);
        Controls.Add(textBox1);
        Controls.Add(convertButton);
        Controls.Add(filePathJSON);
        Controls.Add(chooseFileButton);
        Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        Name = "Converter";
        Text = "Trello Converter";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button chooseFileButton;
    private OpenFileDialog openFileDialog1;
    private TextBox filePathJSON;
    private Button convertButton;
    private TextBox textBox1;
    private CheckBox enumerate;
    private CheckBox generateMarkdown;
    private CheckBox deEnumerate;
    private CheckBox reverseOrder;
    private CheckBox closeOnSucces;
    private CheckBox generateLATEX;
    private CheckBox originalFormat;
}