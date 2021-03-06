﻿namespace Sandbox
{
    partial class frmInterface
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
            this.btnGenerateMap = new System.Windows.Forms.Button();
            this.txtBaseURL = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetCode = new System.Windows.Forms.TextBox();
            this.lblQuery = new System.Windows.Forms.Label();
            this.txtQuery = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.cmbName = new System.Windows.Forms.ComboBox();
            this.btnCopyURL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGenerateMap
            // 
            this.btnGenerateMap.Location = new System.Drawing.Point(204, 116);
            this.btnGenerateMap.Name = "btnGenerateMap";
            this.btnGenerateMap.Size = new System.Drawing.Size(122, 23);
            this.btnGenerateMap.TabIndex = 4;
            this.btnGenerateMap.Text = "Generate PageMap";
            this.btnGenerateMap.UseVisualStyleBackColor = true;
            this.btnGenerateMap.Click += new System.EventHandler(this.btnGenerateMap_Click);
            // 
            // txtBaseURL
            // 
            this.txtBaseURL.Location = new System.Drawing.Point(104, 38);
            this.txtBaseURL.Name = "txtBaseURL";
            this.txtBaseURL.Size = new System.Drawing.Size(280, 20);
            this.txtBaseURL.TabIndex = 1;
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(12, 41);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(59, 13);
            this.lblURL.TabIndex = 2;
            this.lblURL.Text = "Base URL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Target:";
            // 
            // txtTargetCode
            // 
            this.txtTargetCode.Location = new System.Drawing.Point(104, 90);
            this.txtTargetCode.Name = "txtTargetCode";
            this.txtTargetCode.Size = new System.Drawing.Size(280, 20);
            this.txtTargetCode.TabIndex = 3;
            // 
            // lblQuery
            // 
            this.lblQuery.AutoSize = true;
            this.lblQuery.Location = new System.Drawing.Point(12, 67);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(38, 13);
            this.lblQuery.TabIndex = 6;
            this.lblQuery.Text = "Query:";
            // 
            // txtQuery
            // 
            this.txtQuery.Location = new System.Drawing.Point(104, 64);
            this.txtQuery.Name = "txtQuery";
            this.txtQuery.Size = new System.Drawing.Size(280, 20);
            this.txtQuery.TabIndex = 2;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "Name:";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(332, 116);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(52, 23);
            this.btnTest.TabIndex = 9;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // cmbName
            // 
            this.cmbName.FormattingEnabled = true;
            this.cmbName.Location = new System.Drawing.Point(104, 11);
            this.cmbName.Name = "cmbName";
            this.cmbName.Size = new System.Drawing.Size(280, 21);
            this.cmbName.TabIndex = 10;
            this.cmbName.TextChanged += new System.EventHandler(this.cmbName_TextChanged);
            // 
            // btnCopyURL
            // 
            this.btnCopyURL.Location = new System.Drawing.Point(104, 116);
            this.btnCopyURL.Name = "btnCopyURL";
            this.btnCopyURL.Size = new System.Drawing.Size(94, 23);
            this.btnCopyURL.TabIndex = 11;
            this.btnCopyURL.Text = "Copy Full URL";
            this.btnCopyURL.UseVisualStyleBackColor = true;
            this.btnCopyURL.Click += new System.EventHandler(this.btnCopyURL_Click);
            // 
            // frmInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 150);
            this.Controls.Add(this.btnCopyURL);
            this.Controls.Add(this.cmbName);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblQuery);
            this.Controls.Add(this.txtQuery);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTargetCode);
            this.Controls.Add(this.lblURL);
            this.Controls.Add(this.txtBaseURL);
            this.Controls.Add(this.btnGenerateMap);
            this.Name = "frmInterface";
            this.Text = "Interface";
            this.Load += new System.EventHandler(this.frmInterface_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGenerateMap;
        private System.Windows.Forms.TextBox txtBaseURL;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTargetCode;
        private System.Windows.Forms.Label lblQuery;
        private System.Windows.Forms.TextBox txtQuery;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ComboBox cmbName;
        private System.Windows.Forms.Button btnCopyURL;
    }
}

