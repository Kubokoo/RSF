﻿namespace RSF
{
    partial class ResultsWindow
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SizeLeftLabel = new System.Windows.Forms.Label();
            this.textBoxSizeLeft = new System.Windows.Forms.TextBox();
            this.DeleteLeft = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.TextBoxLeft = new System.Windows.Forms.TextBox();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.SizeRightLabel = new System.Windows.Forms.Label();
            this.textBoxSizeRight = new System.Windows.Forms.TextBox();
            this.DeleteRight = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.TextBoxRight = new System.Windows.Forms.TextBox();
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SizeLeftLabel);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxSizeLeft);
            this.splitContainer1.Panel1.Controls.Add(this.DeleteLeft);
            this.splitContainer1.Panel1.Controls.Add(this.buttonPrevious);
            this.splitContainer1.Panel1.Controls.Add(this.TextBoxLeft);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBoxLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.SizeRightLabel);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxSizeRight);
            this.splitContainer1.Panel2.Controls.Add(this.DeleteRight);
            this.splitContainer1.Panel2.Controls.Add(this.buttonNext);
            this.splitContainer1.Panel2.Controls.Add(this.TextBoxRight);
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxRight);
            this.splitContainer1.Size = new System.Drawing.Size(600, 378);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 0;
            // 
            // SizeLeftLabel
            // 
            this.SizeLeftLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SizeLeftLabel.AutoSize = true;
            this.SizeLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.SizeLeftLabel.Location = new System.Drawing.Point(3, 238);
            this.SizeLeftLabel.Name = "SizeLeftLabel";
            this.SizeLeftLabel.Size = new System.Drawing.Size(31, 13);
            this.SizeLeftLabel.TabIndex = 8;
            this.SizeLeftLabel.Text = "Size";
            // 
            // textBoxSizeLeft
            // 
            this.textBoxSizeLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSizeLeft.Location = new System.Drawing.Point(40, 237);
            this.textBoxSizeLeft.Name = "textBoxSizeLeft";
            this.textBoxSizeLeft.ReadOnly = true;
            this.textBoxSizeLeft.Size = new System.Drawing.Size(257, 20);
            this.textBoxSizeLeft.TabIndex = 7;
            // 
            // DeleteLeft
            // 
            this.DeleteLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DeleteLeft.Location = new System.Drawing.Point(96, 343);
            this.DeleteLeft.Name = "DeleteLeft";
            this.DeleteLeft.Size = new System.Drawing.Size(75, 23);
            this.DeleteLeft.TabIndex = 6;
            this.DeleteLeft.Text = "Delete";
            this.DeleteLeft.UseVisualStyleBackColor = true;
            this.DeleteLeft.Click += new System.EventHandler(this.DeleteLeft_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPrevious.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonPrevious.Location = new System.Drawing.Point(3, 343);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(87, 23);
            this.buttonPrevious.TabIndex = 5;
            this.buttonPrevious.Text = "<< Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // TextBoxLeft
            // 
            this.TextBoxLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxLeft.Location = new System.Drawing.Point(3, 211);
            this.TextBoxLeft.Name = "TextBoxLeft";
            this.TextBoxLeft.ReadOnly = true;
            this.TextBoxLeft.Size = new System.Drawing.Size(294, 20);
            this.TextBoxLeft.TabIndex = 1;
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxLeft.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(294, 202);
            this.pictureBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLeft.TabIndex = 0;
            this.pictureBoxLeft.TabStop = false;
            // 
            // SizeRightLabel
            // 
            this.SizeRightLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SizeRightLabel.AutoSize = true;
            this.SizeRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.SizeRightLabel.Location = new System.Drawing.Point(4, 237);
            this.SizeRightLabel.Name = "SizeRightLabel";
            this.SizeRightLabel.Size = new System.Drawing.Size(47, 13);
            this.SizeRightLabel.TabIndex = 6;
            this.SizeRightLabel.Text = "Bigger:";
            // 
            // textBoxSizeRight
            // 
            this.textBoxSizeRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSizeRight.Location = new System.Drawing.Point(57, 238);
            this.textBoxSizeRight.Name = "textBoxSizeRight";
            this.textBoxSizeRight.ReadOnly = true;
            this.textBoxSizeRight.Size = new System.Drawing.Size(238, 20);
            this.textBoxSizeRight.TabIndex = 5;
            // 
            // DeleteRight
            // 
            this.DeleteRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DeleteRight.Location = new System.Drawing.Point(135, 343);
            this.DeleteRight.Name = "DeleteRight";
            this.DeleteRight.Size = new System.Drawing.Size(75, 23);
            this.DeleteRight.TabIndex = 4;
            this.DeleteRight.Text = "Delete";
            this.DeleteRight.UseVisualStyleBackColor = true;
            this.DeleteRight.Click += new System.EventHandler(this.DeleteRight_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.buttonNext.Location = new System.Drawing.Point(216, 343);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(87, 23);
            this.buttonNext.TabIndex = 3;
            this.buttonNext.Text = "Next >>";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // TextBoxRight
            // 
            this.TextBoxRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxRight.Location = new System.Drawing.Point(4, 211);
            this.TextBoxRight.Name = "TextBoxRight";
            this.TextBoxRight.ReadOnly = true;
            this.TextBoxRight.Size = new System.Drawing.Size(299, 20);
            this.TextBoxRight.TabIndex = 2;
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxRight.Location = new System.Drawing.Point(1, 3);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(302, 202);
            this.pictureBoxRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRight.TabIndex = 1;
            this.pictureBoxRight.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // ResultsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 378);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(360, 260);
            this.Name = "ResultsWindow";
            this.Text = "Search Results";
            this.ResizeEnd += new System.EventHandler(this.imageLoad);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxLeft;
        private System.Windows.Forms.PictureBox pictureBoxRight;
        private System.Windows.Forms.TextBox TextBoxLeft;
        private System.Windows.Forms.TextBox TextBoxRight;
        private System.Windows.Forms.Button DeleteLeft;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button DeleteRight;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label SizeLeftLabel;
        private System.Windows.Forms.TextBox textBoxSizeLeft;
        private System.Windows.Forms.Label SizeRightLabel;
        private System.Windows.Forms.TextBox textBoxSizeRight;
    }
}