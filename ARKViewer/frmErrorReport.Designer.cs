namespace ARKViewer
{
    partial class frmErrorReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmErrorReport));
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.picPlayerGender = new System.Windows.Forms.PictureBox();
            this.pnlPlayerInventory = new System.Windows.Forms.Panel();
            this.rtbError = new System.Windows.Forms.RichTextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPlayerGender)).BeginInit();
            this.pnlPlayerInventory.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPlayerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.DimGray;
            this.lblPlayerName.Location = new System.Drawing.Point(113, 7);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(508, 25);
            this.lblPlayerName.TabIndex = 12;
            this.lblPlayerName.Text = "Busted!";
            // 
            // picPlayerGender
            // 
            this.picPlayerGender.Image = ((System.Drawing.Image)(resources.GetObject("picPlayerGender.Image")));
            this.picPlayerGender.Location = new System.Drawing.Point(9, 7);
            this.picPlayerGender.Name = "picPlayerGender";
            this.picPlayerGender.Size = new System.Drawing.Size(98, 87);
            this.picPlayerGender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPlayerGender.TabIndex = 14;
            this.picPlayerGender.TabStop = false;
            // 
            // pnlPlayerInventory
            // 
            this.pnlPlayerInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlayerInventory.BackColor = System.Drawing.Color.PowderBlue;
            this.pnlPlayerInventory.Controls.Add(this.rtbError);
            this.pnlPlayerInventory.Location = new System.Drawing.Point(9, 103);
            this.pnlPlayerInventory.Name = "pnlPlayerInventory";
            this.pnlPlayerInventory.Size = new System.Drawing.Size(635, 271);
            this.pnlPlayerInventory.TabIndex = 15;
            // 
            // rtbError
            // 
            this.rtbError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbError.Location = new System.Drawing.Point(9, 9);
            this.rtbError.Name = "rtbError";
            this.rtbError.ReadOnly = true;
            this.rtbError.Size = new System.Drawing.Size(615, 248);
            this.rtbError.TabIndex = 0;
            this.rtbError.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnClose.Location = new System.Drawing.Point(569, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(115, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(506, 56);
            this.label1.TabIndex = 17;
            this.label1.Text = "It looks like we missed something important and it caused a crash.  \r\n\r\nPlease re" +
    "port the below information back to @MirageUK on the forum.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmErrorReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(656, 421);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlPlayerInventory);
            this.Controls.Add(this.lblPlayerName);
            this.Controls.Add(this.picPlayerGender);
            this.Name = "frmErrorReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error Report";
            ((System.ComponentModel.ISupportInitialize)(this.picPlayerGender)).EndInit();
            this.pnlPlayerInventory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.PictureBox picPlayerGender;
        private System.Windows.Forms.Panel pnlPlayerInventory;
        private System.Windows.Forms.RichTextBox rtbError;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}