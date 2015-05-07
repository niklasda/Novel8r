namespace Novel8r.WinForms.Views.CustomDialogs
{
    partial class ModifiedFilesDialog
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
            this.lvwFiles = new Infragistics.Win.UltraWinListView.UltraListView();
            this.lblSaveChanges = new Infragistics.Win.Misc.UltraLabel();
            this.btnCancel = new Infragistics.Win.Misc.UltraButton();
            this.btnNo = new Infragistics.Win.Misc.UltraButton();
            this.btnYes = new Infragistics.Win.Misc.UltraButton();
            ((System.ComponentModel.ISupportInitialize)(this.lvwFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwFiles
            // 
            this.lvwFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwFiles.ItemSettings.AllowEdit = Infragistics.Win.DefaultableBoolean.False;
            this.lvwFiles.ItemSettings.HideSelection = false;
            this.lvwFiles.ItemSettings.SelectionType = Infragistics.Win.UltraWinListView.SelectionType.None;
            this.lvwFiles.Location = new System.Drawing.Point(12, 41);
            this.lvwFiles.MainColumn.AutoSizeMode = ((Infragistics.Win.UltraWinListView.ColumnAutoSizeMode)((Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.Header | Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.AllItems)));
            this.lvwFiles.MainColumn.DataType = typeof(string);
            this.lvwFiles.MainColumn.Text = "Filename";
            this.lvwFiles.Name = "lvwFiles";
            this.lvwFiles.ShowGroups = false;
            this.lvwFiles.Size = new System.Drawing.Size(471, 216);
            this.lvwFiles.TabIndex = 3;
            this.lvwFiles.View = Infragistics.Win.UltraWinListView.UltraListViewStyle.Details;
            this.lvwFiles.ViewSettingsDetails.AutoFitColumns = Infragistics.Win.UltraWinListView.AutoFitColumns.ResizeAllColumns;
            this.lvwFiles.ViewSettingsDetails.ColumnAutoSizeMode = ((Infragistics.Win.UltraWinListView.ColumnAutoSizeMode)((Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.Header | Infragistics.Win.UltraWinListView.ColumnAutoSizeMode.AllItems)));
            this.lvwFiles.ViewSettingsDetails.ColumnHeaderStyle = Infragistics.Win.HeaderStyle.WindowsVista;
            this.lvwFiles.ViewSettingsDetails.FullRowSelect = true;
            // 
            // lblSaveChanges
            // 
            this.lblSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSaveChanges.Location = new System.Drawing.Point(12, 20);
            this.lblSaveChanges.Name = "lblSaveChanges";
            this.lblSaveChanges.Size = new System.Drawing.Size(471, 15);
            this.lblSaveChanges.TabIndex = 4;
            this.lblSaveChanges.Text = "Save changes to the following files?";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(408, 275);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            // 
            // btnNo
            // 
            this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Location = new System.Drawing.Point(327, 275);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "&No";
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnYes.Location = new System.Drawing.Point(246, 275);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "&Yes";
            // 
            // ModifiedFilesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 310);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblSaveChanges);
            this.Controls.Add(this.lvwFiles);
            this.Name = "ModifiedFilesDialog";
            this.Text = "Save Files";
            ((System.ComponentModel.ISupportInitialize)(this.lvwFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraLabel lblSaveChanges;
        private Infragistics.Win.Misc.UltraButton btnCancel;
        private Infragistics.Win.Misc.UltraButton btnNo;
        private Infragistics.Win.Misc.UltraButton btnYes;
        public Infragistics.Win.UltraWinListView.UltraListView lvwFiles;
    }
}