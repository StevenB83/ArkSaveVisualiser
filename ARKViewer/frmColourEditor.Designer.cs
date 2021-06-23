
namespace ARKViewer
{
    partial class frmColourEditor
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
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.lblClassName = new System.Windows.Forms.Label();
            this.btnCcancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.udId = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlColour = new System.Windows.Forms.Panel();
            this.udR = new System.Windows.Forms.NumericUpDown();
            this.udG = new System.Windows.Forms.NumericUpDown();
            this.udB = new System.Windows.Forms.NumericUpDown();
            this.grpEditor = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.udId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udB)).BeginInit();
            this.grpEditor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.BackColor = System.Drawing.SystemColors.Control;
            this.lblDisplayName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplayName.ForeColor = System.Drawing.Color.DimGray;
            this.lblDisplayName.Location = new System.Drawing.Point(24, 95);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(292, 23);
            this.lblDisplayName.TabIndex = 2;
            this.lblDisplayName.Text = "RGB";
            this.lblDisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblClassName
            // 
            this.lblClassName.BackColor = System.Drawing.SystemColors.Control;
            this.lblClassName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClassName.ForeColor = System.Drawing.Color.DimGray;
            this.lblClassName.Location = new System.Drawing.Point(23, 43);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(278, 23);
            this.lblClassName.TabIndex = 0;
            this.lblClassName.Text = "Id";
            this.lblClassName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCcancel
            // 
            this.btnCcancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCcancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCcancel.Location = new System.Drawing.Point(280, 255);
            this.btnCcancel.Name = "btnCcancel";
            this.btnCcancel.Size = new System.Drawing.Size(75, 23);
            this.btnCcancel.TabIndex = 2;
            this.btnCcancel.Text = "Close";
            this.btnCcancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(197, 255);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // udId
            // 
            this.udId.Location = new System.Drawing.Point(24, 70);
            this.udId.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.udId.Name = "udId";
            this.udId.Size = new System.Drawing.Size(290, 20);
            this.udId.TabIndex = 1;
            this.udId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udId.ValueChanged += new System.EventHandler(this.udId_ValueChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(24, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Colour";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlColour
            // 
            this.pnlColour.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlColour.Location = new System.Drawing.Point(24, 178);
            this.pnlColour.Name = "pnlColour";
            this.pnlColour.Size = new System.Drawing.Size(290, 25);
            this.pnlColour.TabIndex = 7;
            this.pnlColour.Click += new System.EventHandler(this.pnlColour_Click);
            // 
            // udR
            // 
            this.udR.Location = new System.Drawing.Point(27, 121);
            this.udR.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udR.Name = "udR";
            this.udR.Size = new System.Drawing.Size(92, 20);
            this.udR.TabIndex = 3;
            this.udR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udR.ValueChanged += new System.EventHandler(this.udR_ValueChanged);
            // 
            // udG
            // 
            this.udG.Location = new System.Drawing.Point(124, 121);
            this.udG.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udG.Name = "udG";
            this.udG.Size = new System.Drawing.Size(92, 20);
            this.udG.TabIndex = 4;
            this.udG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udG.ValueChanged += new System.EventHandler(this.udG_ValueChanged);
            // 
            // udB
            // 
            this.udB.Location = new System.Drawing.Point(222, 121);
            this.udB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.udB.Name = "udB";
            this.udB.Size = new System.Drawing.Size(92, 20);
            this.udB.TabIndex = 5;
            this.udB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.udB.ValueChanged += new System.EventHandler(this.udB_ValueChanged);
            // 
            // grpEditor
            // 
            this.grpEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEditor.Controls.Add(this.label2);
            this.grpEditor.Controls.Add(this.udB);
            this.grpEditor.Controls.Add(this.label22);
            this.grpEditor.Controls.Add(this.udG);
            this.grpEditor.Controls.Add(this.lblClassName);
            this.grpEditor.Controls.Add(this.udR);
            this.grpEditor.Controls.Add(this.lblDisplayName);
            this.grpEditor.Controls.Add(this.pnlColour);
            this.grpEditor.Controls.Add(this.udId);
            this.grpEditor.Controls.Add(this.label1);
            this.grpEditor.Location = new System.Drawing.Point(12, 12);
            this.grpEditor.Name = "grpEditor";
            this.grpEditor.Size = new System.Drawing.Size(341, 226);
            this.grpEditor.TabIndex = 0;
            this.grpEditor.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Colour Details";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.BackColor = System.Drawing.Color.Aqua;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(0, 6);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(343, 6);
            this.label22.TabIndex = 0;
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmColourEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCcancel;
            this.ClientSize = new System.Drawing.Size(368, 290);
            this.Controls.Add(this.grpEditor);
            this.Controls.Add(this.btnCcancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmColourEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colour Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmColourEditor_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.udId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udB)).EndInit();
            this.grpEditor.ResumeLayout(false);
            this.grpEditor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblDisplayName;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.Button btnCcancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown udId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlColour;
        private System.Windows.Forms.NumericUpDown udR;
        private System.Windows.Forms.NumericUpDown udG;
        private System.Windows.Forms.NumericUpDown udB;
        private System.Windows.Forms.GroupBox grpEditor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label22;
    }
}