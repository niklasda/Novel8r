using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinEditors;

namespace Novel8r.WinForms.Views
{
    partial class AboutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.lblProductName = new Infragistics.Win.Misc.UltraLabel();
            this.lblCopyright = new Infragistics.Win.Misc.UltraLabel();
            this.lblCompanyName = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            this.txtDescription = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.btnOk = new Infragistics.Win.Misc.UltraButton();
            this.pbDotway = new System.Windows.Forms.PictureBox();
            this.lblCompanyName2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDotway)).BeginInit();
            this.SuspendLayout();
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(12, 12);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(134, 109);
            this.logoPictureBox.TabIndex = 12;
            this.logoPictureBox.TabStop = false;
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Location = new System.Drawing.Point(181, 12);
            this.lblProductName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(76, 14);
            this.lblProductName.TabIndex = 19;
            this.lblProductName.Text = "Product Name";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(181, 29);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblCopyright.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(53, 14);
            this.lblCopyright.TabIndex = 21;
            this.lblCopyright.Text = "Copyright";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.BaseURL = "http://nidar.se";
            this.lblCompanyName.Location = new System.Drawing.Point(181, 46);
            this.lblCompanyName.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblCompanyName.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(68, 13);
            this.lblCompanyName.TabIndex = 22;
            this.lblCompanyName.TabStop = true;
            this.lblCompanyName.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.lblCompanyName.Value = "http://nida.se";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(181, 91);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Scrollbars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(291, 107);
            this.txtDescription.TabIndex = 23;
            this.txtDescription.TabStop = false;
            this.txtDescription.Text = "Description";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Location = new System.Drawing.Point(397, 219);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 24);
            this.btnOk.TabIndex = 24;
            this.btnOk.Text = "&OK";
            // 
            // pbDotway
            // 
            this.pbDotway.Image = ((System.Drawing.Image)(resources.GetObject("pbDotway.Image")));
            this.pbDotway.Location = new System.Drawing.Point(12, 140);
            this.pbDotway.Name = "pbDotway";
            this.pbDotway.Size = new System.Drawing.Size(134, 58);
            this.pbDotway.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbDotway.TabIndex = 25;
            this.pbDotway.TabStop = false;
            // 
            // lblCompanyName2
            // 
            this.lblCompanyName2.AutoSize = true;
            this.lblCompanyName2.BaseURL = "http://dotway.se";
            this.lblCompanyName2.Location = new System.Drawing.Point(181, 65);
            this.lblCompanyName2.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.lblCompanyName2.MaximumSize = new System.Drawing.Size(0, 17);
            this.lblCompanyName2.Name = "lblCompanyName2";
            this.lblCompanyName2.Size = new System.Drawing.Size(82, 13);
            this.lblCompanyName2.TabIndex = 26;
            this.lblCompanyName2.TabStop = true;
            this.lblCompanyName2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
            this.lblCompanyName2.Value = "http://dotway.se";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOk;
            this.ClientSize = new System.Drawing.Size(484, 255);
            this.Controls.Add(this.lblCompanyName2);
            this.Controls.Add(this.pbDotway);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.logoPictureBox);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbDotway)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPictureBox;
        private UltraButton btnOk;
        public UltraLabel lblProductName;
        public UltraLabel lblCopyright;
        public UltraFormattedLinkLabel lblCompanyName;
        public UltraTextEditor txtDescription;
        private System.Windows.Forms.PictureBox pbDotway;
        public UltraFormattedLinkLabel lblCompanyName2;
    }
}
