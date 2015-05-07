
namespace Novel8r.WinForms.Views.MdiChildren
{
    partial class SearchChildForm
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
            this.btnSearch = new Infragistics.Win.Misc.UltraButton();
            this.cboSearchAreaType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.txtSearchArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.grpCriteria = new Infragistics.Win.Misc.UltraGroupBox();
            this.chkIncludeSystemObjects = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.chkCaseSensitive = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.chkExactMatch = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.grpWhat = new Infragistics.Win.Misc.UltraGroupBox();
            this.lblWhat = new Infragistics.Win.Misc.UltraLabel();
            this.grdResults = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchAreaType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCriteria)).BeginInit();
            this.grpCriteria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpWhat)).BeginInit();
            this.grpWhat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(418, 17);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            // 
            // cboSearchAreaType
            // 
            this.cboSearchAreaType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.cboSearchAreaType.LimitToList = true;
            this.cboSearchAreaType.Location = new System.Drawing.Point(6, 19);
            this.cboSearchAreaType.Name = "cboSearchAreaType";
            this.cboSearchAreaType.Size = new System.Drawing.Size(121, 21);
            this.cboSearchAreaType.TabIndex = 9;
            // 
            // txtSearchArea
            // 
            this.txtSearchArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchArea.Location = new System.Drawing.Point(133, 19);
            this.txtSearchArea.Name = "txtSearchArea";
            this.txtSearchArea.Size = new System.Drawing.Size(279, 21);
            this.txtSearchArea.TabIndex = 5;
            // 
            // grpCriteria
            // 
            this.grpCriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCriteria.Controls.Add(this.chkIncludeSystemObjects);
            this.grpCriteria.Controls.Add(this.chkCaseSensitive);
            this.grpCriteria.Controls.Add(this.chkExactMatch);
            this.grpCriteria.Controls.Add(this.btnSearch);
            this.grpCriteria.Controls.Add(this.cboSearchAreaType);
            this.grpCriteria.Controls.Add(this.txtSearchArea);
            this.grpCriteria.Location = new System.Drawing.Point(12, 58);
            this.grpCriteria.Name = "grpCriteria";
            this.grpCriteria.Size = new System.Drawing.Size(499, 75);
            this.grpCriteria.TabIndex = 10;
            this.grpCriteria.Text = "Search Criteria";
            // 
            // chkIncludeSystemObjects
            // 
            this.chkIncludeSystemObjects.Checked = true;
            this.chkIncludeSystemObjects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSystemObjects.Location = new System.Drawing.Point(328, 46);
            this.chkIncludeSystemObjects.Name = "chkIncludeSystemObjects";
            this.chkIncludeSystemObjects.Size = new System.Drawing.Size(150, 17);
            this.chkIncludeSystemObjects.TabIndex = 12;
            this.chkIncludeSystemObjects.Text = "Include Resources";
            // 
            // chkCaseSensitive
            // 
            this.chkCaseSensitive.Location = new System.Drawing.Point(225, 46);
            this.chkCaseSensitive.Name = "chkCaseSensitive";
            this.chkCaseSensitive.Size = new System.Drawing.Size(105, 17);
            this.chkCaseSensitive.TabIndex = 11;
            this.chkCaseSensitive.Text = "Case Sensitive";
            // 
            // chkExactMatch
            // 
            this.chkExactMatch.Location = new System.Drawing.Point(133, 46);
            this.chkExactMatch.Name = "chkExactMatch";
            this.chkExactMatch.Size = new System.Drawing.Size(86, 17);
            this.chkExactMatch.TabIndex = 10;
            this.chkExactMatch.Text = "Exact Match";
            // 
            // grpWhat
            // 
            this.grpWhat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpWhat.Controls.Add(this.lblWhat);
            this.grpWhat.Location = new System.Drawing.Point(12, 4);
            this.grpWhat.Name = "grpWhat";
            this.grpWhat.Size = new System.Drawing.Size(499, 48);
            this.grpWhat.TabIndex = 11;
            this.grpWhat.Text = "You are searching for";
            // 
            // lblWhat
            // 
            this.lblWhat.AutoSize = true;
            this.lblWhat.Location = new System.Drawing.Point(7, 20);
            this.lblWhat.Name = "lblWhat";
            this.lblWhat.Size = new System.Drawing.Size(180, 14);
            this.lblWhat.TabIndex = 0;
            this.lblWhat.Text = "Column names in Server.Database";
            // 
            // grdResults
            // 
            this.grdResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdResults.DisplayLayout.MaxColScrollRegions = 1;
            this.grdResults.DisplayLayout.MaxRowScrollRegions = 1;
            this.grdResults.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grdResults.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdResults.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdResults.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdResults.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grdResults.Location = new System.Drawing.Point(13, 140);
            this.grdResults.Name = "grdResults";
            this.grdResults.Size = new System.Drawing.Size(498, 180);
            this.grdResults.TabIndex = 12;
            this.grdResults.UseOsThemes = Infragistics.Win.DefaultableBoolean.True;
            // 
            // SearchChildForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 332);
            this.Controls.Add(this.grdResults);
            this.Controls.Add(this.grpWhat);
            this.Controls.Add(this.grpCriteria);
            this.Name = "SearchChildForm";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.cboSearchAreaType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSearchArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpCriteria)).EndInit();
            this.grpCriteria.ResumeLayout(false);
            this.grpCriteria.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpWhat)).EndInit();
            this.grpWhat.ResumeLayout(false);
            this.grpWhat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Infragistics.Win.Misc.UltraButton btnSearch;
        public Infragistics.Win.UltraWinEditors.UltraComboEditor cboSearchAreaType;
        public Infragistics.Win.UltraWinEditors.UltraTextEditor txtSearchArea;
        public Infragistics.Win.UltraWinEditors.UltraCheckEditor chkCaseSensitive;
        public Infragistics.Win.UltraWinEditors.UltraCheckEditor chkExactMatch;
        public Infragistics.Win.Misc.UltraLabel lblWhat;
        public Infragistics.Win.UltraWinEditors.UltraCheckEditor chkIncludeSystemObjects;
        public Infragistics.Win.UltraWinGrid.UltraGrid grdResults;
        public Infragistics.Win.Misc.UltraGroupBox grpCriteria;
        public Infragistics.Win.Misc.UltraGroupBox grpWhat;

    }
}