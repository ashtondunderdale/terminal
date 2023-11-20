namespace Terminal;

partial class Terminal
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        outputBox = new RichTextBox();
        commandBox = new RichTextBox();
        label1 = new Label();
        SuspendLayout();
        // 
        // outputBox
        // 
        outputBox.BackColor = Color.Black;
        outputBox.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
        outputBox.ForeColor = Color.LightGray;
        outputBox.Location = new Point(-1, 0);
        outputBox.Name = "outputBox";
        outputBox.Size = new Size(1372, 570);
        outputBox.TabIndex = 0;
        outputBox.Text = "";
        // 
        // commandBox
        // 
        commandBox.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
        commandBox.Location = new Point(899, 576);
        commandBox.Name = "commandBox";
        commandBox.Size = new Size(458, 30);
        commandBox.TabIndex = 1;
        commandBox.Text = "";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(721, 581);
        label1.Name = "label1";
        label1.Size = new Size(172, 20);
        label1.TabIndex = 2;
        label1.Text = "Enter Command Here ->";
        // 
        // Terminal
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1369, 618);
        Controls.Add(label1);
        Controls.Add(commandBox);
        Controls.Add(outputBox);
        Name = "Terminal";
        Text = "Sharp";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    public static RichTextBox outputBox;
    public static RichTextBox commandBox;
}
