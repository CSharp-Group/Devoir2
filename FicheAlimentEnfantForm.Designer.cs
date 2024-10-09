namespace FicheAliments
{
    partial class FicheAlimentEnfantForm
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
            this.nomLabel = new System.Windows.Forms.Label();
            this.prenomLabel = new System.Windows.Forms.Label();
            this.telephoneLabel = new System.Windows.Forms.Label();
            this.infoLabel = new System.Windows.Forms.Label();
            this.nomTextBox = new System.Windows.Forms.TextBox();
            this.prenomTextBox = new System.Windows.Forms.TextBox();
            this.infoRichTextBox = new System.Windows.Forms.RichTextBox();
            this.telephoneMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // nomLabel
            // 
            this.nomLabel.AutoSize = true;
            this.nomLabel.Location = new System.Drawing.Point(13, 13);
            this.nomLabel.Name = "nomLabel";
            this.nomLabel.Size = new System.Drawing.Size(50, 20);
            this.nomLabel.TabIndex = 0;
            this.nomLabel.Text = "Nom: ";
            // 
            // prenomLabel
            // 
            this.prenomLabel.AutoSize = true;
            this.prenomLabel.Location = new System.Drawing.Point(13, 56);
            this.prenomLabel.Name = "prenomLabel";
            this.prenomLabel.Size = new System.Drawing.Size(68, 20);
            this.prenomLabel.TabIndex = 1;
            this.prenomLabel.Text = "Prénom:";
            // 
            // telephoneLabel
            // 
            this.telephoneLabel.AutoSize = true;
            this.telephoneLabel.Location = new System.Drawing.Point(13, 99);
            this.telephoneLabel.Name = "telephoneLabel";
            this.telephoneLabel.Size = new System.Drawing.Size(88, 20);
            this.telephoneLabel.TabIndex = 2;
            this.telephoneLabel.Text = "Téléphone:";
            // 
            // infoLabel
            // 
            this.infoLabel.AutoSize = true;
            this.infoLabel.Location = new System.Drawing.Point(13, 142);
            this.infoLabel.Name = "infoLabel";
            this.infoLabel.Size = new System.Drawing.Size(41, 20);
            this.infoLabel.TabIndex = 3;
            this.infoLabel.Text = "Info:";
            // 
            // nomTextBox
            // 
            this.nomTextBox.Location = new System.Drawing.Point(107, 13);
            this.nomTextBox.Name = "nomTextBox";
            this.nomTextBox.Size = new System.Drawing.Size(417, 26);
            this.nomTextBox.TabIndex = 4;
            this.nomTextBox.TextChanged += new System.EventHandler(this.clientTextBox_TextChanged);
            // 
            // prenomTextBox
            // 
            this.prenomTextBox.Location = new System.Drawing.Point(107, 56);
            this.prenomTextBox.Name = "prenomTextBox";
            this.prenomTextBox.Size = new System.Drawing.Size(417, 26);
            this.prenomTextBox.TabIndex = 5;
            this.prenomTextBox.TextChanged += new System.EventHandler(this.clientTextBox_TextChanged);
            // 
            // infoRichTextBox
            // 
            this.infoRichTextBox.Location = new System.Drawing.Point(107, 142);
            this.infoRichTextBox.Name = "infoRichTextBox";
            this.infoRichTextBox.Size = new System.Drawing.Size(417, 234);
            this.infoRichTextBox.TabIndex = 7;
            this.infoRichTextBox.Text = "";
            this.infoRichTextBox.TextChanged += new System.EventHandler(this.clientTextBox_TextChanged);
            // 
            // telephoneMaskedTextBox
            // 
            this.telephoneMaskedTextBox.Location = new System.Drawing.Point(108, 99);
            this.telephoneMaskedTextBox.Mask = "(999) 000-0000";
            this.telephoneMaskedTextBox.Name = "telephoneMaskedTextBox";
            this.telephoneMaskedTextBox.Size = new System.Drawing.Size(165, 26);
            this.telephoneMaskedTextBox.TabIndex = 8;
            this.telephoneMaskedTextBox.TextChanged += new System.EventHandler(this.clientTextBox_TextChanged);
            // 
            // FicheAlimentEnfantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 407);
            this.Controls.Add(this.telephoneMaskedTextBox);
            this.Controls.Add(this.infoRichTextBox);
            this.Controls.Add(this.prenomTextBox);
            this.Controls.Add(this.nomTextBox);
            this.Controls.Add(this.infoLabel);
            this.Controls.Add(this.telephoneLabel);
            this.Controls.Add(this.prenomLabel);
            this.Controls.Add(this.nomLabel);
            this.Name = "FicheAlimentEnfantForm";
            this.Text = "Aliment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FicheAlimentEnfantForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nomLabel;
        private System.Windows.Forms.Label prenomLabel;
        private System.Windows.Forms.Label telephoneLabel;
        private System.Windows.Forms.Label infoLabel;
        private System.Windows.Forms.TextBox nomTextBox;
        private System.Windows.Forms.TextBox prenomTextBox;
        private System.Windows.Forms.RichTextBox infoRichTextBox;
        private System.Windows.Forms.MaskedTextBox telephoneMaskedTextBox;
    }
}