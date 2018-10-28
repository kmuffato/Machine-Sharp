﻿namespace MachineSharpGUI
{
    partial class MachineSharp
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
            this.RunButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StopButton = new System.Windows.Forms.Button();
            this.AddNeuronsButton = new System.Windows.Forms.Button();
            this.AddLayersButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.RemoveNeuronsButton = new System.Windows.Forms.Button();
            this.RemoveLayersButton = new System.Windows.Forms.Button();
            this.NetTypeLabel = new System.Windows.Forms.Label();
            this.ChangeNetButton = new System.Windows.Forms.Button();
            this.SetInputsButton = new System.Windows.Forms.Button();
            this.SetInputDataSourceButton = new System.Windows.Forms.Button();
            this.NetConfigDetailsBox = new System.Windows.Forms.TextBox();
            this.TrainButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Location = new System.Drawing.Point(626, 559);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(60, 69);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(724, 311);
            this.panel1.TabIndex = 1;
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(709, 546);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            // 
            // AddNeuronsButton
            // 
            this.AddNeuronsButton.Location = new System.Drawing.Point(60, 422);
            this.AddNeuronsButton.Name = "AddNeuronsButton";
            this.AddNeuronsButton.Size = new System.Drawing.Size(117, 23);
            this.AddNeuronsButton.TabIndex = 3;
            this.AddNeuronsButton.Text = "Add Neurons";
            this.AddNeuronsButton.UseVisualStyleBackColor = true;
            // 
            // AddLayersButton
            // 
            this.AddLayersButton.Location = new System.Drawing.Point(195, 422);
            this.AddLayersButton.Name = "AddLayersButton";
            this.AddLayersButton.Size = new System.Drawing.Size(124, 23);
            this.AddLayersButton.TabIndex = 5;
            this.AddLayersButton.Text = "Add Layers";
            this.AddLayersButton.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Location = new System.Drawing.Point(626, 445);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(156, 67);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "Net Stats:";
            // 
            // RemoveNeuronsButton
            // 
            this.RemoveNeuronsButton.Location = new System.Drawing.Point(60, 462);
            this.RemoveNeuronsButton.Name = "RemoveNeuronsButton";
            this.RemoveNeuronsButton.Size = new System.Drawing.Size(117, 23);
            this.RemoveNeuronsButton.TabIndex = 0;
            this.RemoveNeuronsButton.Text = "Remove Neurons";
            this.RemoveNeuronsButton.UseVisualStyleBackColor = true;
            // 
            // RemoveLayersButton
            // 
            this.RemoveLayersButton.Location = new System.Drawing.Point(195, 462);
            this.RemoveLayersButton.Name = "RemoveLayersButton";
            this.RemoveLayersButton.Size = new System.Drawing.Size(124, 23);
            this.RemoveLayersButton.TabIndex = 7;
            this.RemoveLayersButton.Text = "Remove layers";
            this.RemoveLayersButton.UseVisualStyleBackColor = true;
            // 
            // NetTypeLabel
            // 
            this.NetTypeLabel.AutoSize = true;
            this.NetTypeLabel.Location = new System.Drawing.Point(202, 32);
            this.NetTypeLabel.Name = "NetTypeLabel";
            this.NetTypeLabel.Size = new System.Drawing.Size(50, 13);
            this.NetTypeLabel.TabIndex = 8;
            this.NetTypeLabel.Text = "Net type:";
            // 
            // ChangeNetButton
            // 
            this.ChangeNetButton.Location = new System.Drawing.Point(60, 27);
            this.ChangeNetButton.Name = "ChangeNetButton";
            this.ChangeNetButton.Size = new System.Drawing.Size(116, 23);
            this.ChangeNetButton.TabIndex = 9;
            this.ChangeNetButton.Text = "Change Net Type";
            this.ChangeNetButton.UseVisualStyleBackColor = true;
            // 
            // SetInputsButton
            // 
            this.SetInputsButton.Location = new System.Drawing.Point(60, 386);
            this.SetInputsButton.Name = "SetInputsButton";
            this.SetInputsButton.Size = new System.Drawing.Size(117, 23);
            this.SetInputsButton.TabIndex = 10;
            this.SetInputsButton.Text = "Set Inputs Data Type";
            this.SetInputsButton.UseVisualStyleBackColor = true;
            this.SetInputsButton.Click += new System.EventHandler(this.SetInputsButton_Click);
            // 
            // SetInputDataSourceButton
            // 
            this.SetInputDataSourceButton.Location = new System.Drawing.Point(195, 386);
            this.SetInputDataSourceButton.Name = "SetInputDataSourceButton";
            this.SetInputDataSourceButton.Size = new System.Drawing.Size(124, 23);
            this.SetInputDataSourceButton.TabIndex = 11;
            this.SetInputDataSourceButton.Text = "Set Input Data Source";
            this.SetInputDataSourceButton.UseVisualStyleBackColor = true;
            // 
            // NetConfigDetailsBox
            // 
            this.NetConfigDetailsBox.BackColor = System.Drawing.SystemColors.Info;
            this.NetConfigDetailsBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NetConfigDetailsBox.Location = new System.Drawing.Point(372, 401);
            this.NetConfigDetailsBox.Multiline = true;
            this.NetConfigDetailsBox.Name = "NetConfigDetailsBox";
            this.NetConfigDetailsBox.ReadOnly = true;
            this.NetConfigDetailsBox.Size = new System.Drawing.Size(199, 152);
            this.NetConfigDetailsBox.TabIndex = 12;
            this.NetConfigDetailsBox.Text = "Net Config:";
            // 
            // TrainButton
            // 
            this.TrainButton.Location = new System.Drawing.Point(626, 530);
            this.TrainButton.Name = "TrainButton";
            this.TrainButton.Size = new System.Drawing.Size(75, 23);
            this.TrainButton.TabIndex = 13;
            this.TrainButton.Text = "Train";
            this.TrainButton.UseVisualStyleBackColor = true;
            // 
            // MachineSharp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 601);
            this.Controls.Add(this.TrainButton);
            this.Controls.Add(this.NetConfigDetailsBox);
            this.Controls.Add(this.SetInputDataSourceButton);
            this.Controls.Add(this.SetInputsButton);
            this.Controls.Add(this.ChangeNetButton);
            this.Controls.Add(this.NetTypeLabel);
            this.Controls.Add(this.RemoveLayersButton);
            this.Controls.Add(this.RemoveNeuronsButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.AddLayersButton);
            this.Controls.Add(this.AddNeuronsButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RunButton);
            this.Name = "MachineSharp";
            this.Text = "MachineSharp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button AddNeuronsButton;
        private System.Windows.Forms.Button AddLayersButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button RemoveNeuronsButton;
        private System.Windows.Forms.Button RemoveLayersButton;
        private System.Windows.Forms.Label NetTypeLabel;
        private System.Windows.Forms.Button ChangeNetButton;
        private System.Windows.Forms.Button SetInputsButton;
        private System.Windows.Forms.Button SetInputDataSourceButton;
        private System.Windows.Forms.TextBox NetConfigDetailsBox;
        private System.Windows.Forms.Button TrainButton;
    }
}

