using System;
using System.Windows.Forms;
using ExtendedRichTextBox;


namespace Novel8r.RtfEditor
{
    public partial class frmFindReplace : Form
    {
        // member variable pointing to main form
        RichTextBoxPrintCtrl _rtbDoc;
        

        // default constructor
        public frmFindReplace()
        {
            InitializeComponent();
        }


        // overloaded constructor accepteing main form as
        // an argument
        public frmFindReplace(RichTextBoxPrintCtrl f)
        {
            InitializeComponent();
            _rtbDoc = f;
        }



        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                int StartPosition;
                StringComparison searchType;

                if (chkMatchCase.Checked)
                {
                    searchType = StringComparison.Ordinal;
                }
                else
                {
                    searchType = StringComparison.OrdinalIgnoreCase;
                }

                StartPosition = _rtbDoc.Text.IndexOf(txtSearchTerm.Text, searchType);

                if (StartPosition == 0)
                {
                    MessageBox.Show("String: " + txtSearchTerm.Text + " not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                _rtbDoc.Select(StartPosition, txtSearchTerm.Text.Length);
                _rtbDoc.ScrollToCaret();
                //mMain.Focus();
                btnFindNext.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }



        private void btnFindNext_Click(object sender, EventArgs e)
        {
            try
            {
                int StartPosition = _rtbDoc.SelectionStart + 2;

                StringComparison searchType;

                if (chkMatchCase.Checked)
                {
                    searchType = StringComparison.Ordinal;
                }
                else
                {
                    searchType = StringComparison.OrdinalIgnoreCase;
                }

                StartPosition = _rtbDoc.Text.IndexOf(txtSearchTerm.Text, StartPosition, searchType);

                if (StartPosition == 0 || StartPosition < 0)
                {
                    MessageBox.Show("String: " + txtSearchTerm.Text + " not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                _rtbDoc.Select(StartPosition, txtSearchTerm.Text.Length);
                _rtbDoc.ScrollToCaret();
                //mMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }




        private void btnReplace_Click(object sender, EventArgs e)
        {
            try
            {
                if (_rtbDoc.SelectedText.Length != 0)
                {
                    _rtbDoc.SelectedText = txtReplacementText.Text;
                }

                int StartPosition;
                StringComparison searchType;

                if (chkMatchCase.Checked)
                {
                    searchType = StringComparison.Ordinal;
                }
                else
                {
                    searchType = StringComparison.OrdinalIgnoreCase;
                }

                StartPosition = _rtbDoc.Text.IndexOf(txtSearchTerm.Text, searchType);

                if (StartPosition == 0 || StartPosition < 0)
                {
                    MessageBox.Show("String: " + txtSearchTerm.Text + " not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }

                _rtbDoc.Select(StartPosition, txtSearchTerm.Text.Length);
                _rtbDoc.ScrollToCaret();
                //mMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }



        private void btnReplaceAll_Click(object sender, EventArgs e)
        {

            try
            {
                _rtbDoc.Rtf = _rtbDoc.Rtf.Replace(txtSearchTerm.Text.Trim(), txtReplacementText.Text.Trim());


                int StartPosition;
                StringComparison searchType;

                if (chkMatchCase.Checked)
                {
                    searchType = StringComparison.Ordinal;
                }
                else
                {
                    searchType = StringComparison.OrdinalIgnoreCase;
                }

                StartPosition = _rtbDoc.Text.IndexOf(txtReplacementText.Text, searchType);

                _rtbDoc.Select(StartPosition, txtReplacementText.Text.Length);
                _rtbDoc.ScrollToCaret();
                //   mMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void txtSearchTerm_TextChanged(object sender, EventArgs e)
        {
            btnFindNext.Enabled = false;
        }

        
    }
}