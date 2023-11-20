namespace Terminal
{
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
            SuspendLayout();
            // 
            // outputBox
            // 
            outputBox.BackColor = Color.Black;
            outputBox.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            outputBox.ForeColor = Color.White;
            outputBox.Location = new Point(434, 12);
            outputBox.Name = "outputBox";
            outputBox.Size = new Size(678, 304);
            outputBox.TabIndex = 0;
            outputBox.Text = "";
            // 
            // commandBox
            // 
            commandBox.Font = new Font("Consolas", 12F, FontStyle.Regular, GraphicsUnit.Point);
            commandBox.Location = new Point(434, 322);
            commandBox.Name = "commandBox";
            commandBox.Size = new Size(195, 30);
            commandBox.TabIndex = 1;
            commandBox.Text = "";
            // 
            // Terminal
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1124, 519);
            Controls.Add(commandBox);
            Controls.Add(outputBox);
            Name = "Terminal";
            Text = "Terminal";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox outputBox;
        private RichTextBox commandBox;
    }
}
