﻿namespace ArkanoidGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            GameIterationTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // GameIterationTimer
            // 
            GameIterationTimer.Interval = 10;
            GameIterationTimer.Tick += GameIterationTimer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Name = "Form1";
            Text = "Form1";
            Load += FrmArkanoidMain_Load;
            Paint += FrmArkanoidMain_Paint;
            KeyDown += FrmArkanoidMain_KeyDown;
            MouseClick += FrmArkanoidMain_MouseClick;
            MouseMove += FrmArkanoidMain_MouseMove;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer GameIterationTimer;
    }
}