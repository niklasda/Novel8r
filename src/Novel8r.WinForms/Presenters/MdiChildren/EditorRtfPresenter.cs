using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using Novel8r.Logic.Interfaces;
using Novel8r.RtfEditor;
using Novel8r.WinForms.Views.MdiChildren;

namespace Novel8r.WinForms.Presenters.MdiChildren
{
    public class EditorRtfPresenter : IPresenter, IViewPresenter<EditorRtfForm>
    {
        private EditorRtfForm _view;

        public DialogResult ShowDialog()
        {
            return _view.ShowDialog();
        }

        public void Show()
        {
            _view.Show();
        }

        public EditorRtfForm View
        {
            get { return _view; }
            set { _view = value; }
        }

        public void Init()
        {
            _view.MdiParent = MainPresenter.Instance.View;

            _view.tbrNew.Click += tbrNew_Click;
            _view.tbrOpen.Click += tbrOpen_Click;
            _view.tbrSave.Click += tbrSave_Click;

            _view.tbrBold.Click += tbrBold_Click;
            _view.tbrItalic.Click += tbrItalic_Click;
            _view.tbrUnderline.Click += tbrUnderline_Click;

            _view.tbrLeft.Click += tbrLeft_Click;
            _view.tbrCenter.Click += tbrCenter_Click;
            _view.tbrRight.Click += tbrRight_Click;

            _view.tbrFont.Click += tbrFont_Click;
            _view.tbrColor.Click += tbrColor_Click;
            _view.tbrFind.Click += tbrFind_Click;

            _view.newToolStripMenuItem.Image = _view.tbrNew.Image;
            _view.openToolStripMenuItem.Image = _view.tbrOpen.Image;
            _view.saveToolStripMenuItem.Image = _view.tbrSave.Image;

            _view.boldToolStripMenuItem.Image = _view.tbrBold.Image;
            _view.italicToolStripMenuItem.Image = _view.tbrItalic.Image;
            _view.underlineToolStripMenuItem.Image = _view.tbrUnderline.Image;

            _view.leftToolStripMenuItem.Image = _view.tbrLeft.Image;
            _view.centerToolStripMenuItem.Image = _view.tbrCenter.Image;
            _view.rightToolStripMenuItem.Image = _view.tbrRight.Image;

            _view.findToolStripMenuItem.Image = _view.tbrFind.Image;
            _view.selectFontToolStripMenuItem.Image = _view.tbrFont.Image;
            _view.fontColorToolStripMenuItem.Image = _view.tbrColor.Image;

            _view.newToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            _view.openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            _view.saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            _view.saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
			
            _view.boldToolStripMenuItem.Click += BoldToolStripMenuItem_Click;
            _view.italicToolStripMenuItem.Click += ItalicToolStripMenuItem_Click;
            _view.underlineToolStripMenuItem.Click += UnderlineToolStripMenuItem_Click;
        	_view.normalToolStripMenuItem.Click += NormalToolStripMenuItem_Click;
        	_view.undoToolStripMenuItem.Click += mnuUndo_Click;
			_view.redoToolStripMenuItem.Click += mnuRedo_Click;


            _view.leftToolStripMenuItem.Click += LeftToolStripMenuItem_Click;
            _view.centerToolStripMenuItem.Click += CenterToolStripMenuItem_Click;
            _view.rightToolStripMenuItem.Click += RightToolStripMenuItem_Click;

            _view.findToolStripMenuItem.Click += FindToolStripMenuItem_Click;
            _view.replaceToolStripMenuItem.Click += FindAndReplaceToolStripMenuItem_Click;
            _view.selectFontToolStripMenuItem.Click += SelectFontToolStripMenuItem_Click;
            _view.fontColorToolStripMenuItem.Click += FontColorToolStripMenuItem_Click;

        	_view.printToolStripMenuItem.Click += PrintToolStripMenuItem_Click;
        	_view.pageColorToolStripMenuItem.Click += PageColorToolStripMenuItem_Click;
        	_view.previewToolStripMenuItem.Click += PreviewToolStripMenuItem_Click;
        	_view.pageSetupToolStripMenuItem.Click += mnuPageSetup_Click;

        	_view.selectAllToolStripMenuItem.Click += SelectAllToolStripMenuItem_Click;
        	_view.cutToolStripMenuItem.Click += CutToolStripMenuItem_Click;
        	_view.copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
        	_view.pasteToolStripMenuItem.Click += PasteToolStripMenuItem_Click;

        	_view.addBulletsToolStripMenuItem.Click += AddBulletsToolStripMenuItem_Click;
        	_view.removeBulletsToolStripMenuItem.Click += RemoveBulletsToolStripMenuItem_Click;


			_view.indentNoneToolStripMenuItem.Click += Indent0ToolStripMenuItem_Click;
			_view.indent5ptsToolStripMenuItem.Click += Indent5ToolStripMenuItem_Click;
			_view.indent10ptsToolStripMenuItem.Click += Indent10ToolStripMenuItem_Click;
			_view.indent15ptsToolStripMenuItem.Click += Indent15ToolStripMenuItem_Click;
			_view.indent20ptsToolStripMenuItem.Click += Indent20ToolStripMenuItem_Click;

        }


        private string currentFile;
        private int checkPrint;

        private OpenFileDialog dlgOpen = new OpenFileDialog();
        private SaveFileDialog dlgSave = new SaveFileDialog();
        private PrintDialog dlgPrint = new PrintDialog();
        private PrintPreviewDialog dlgPrintPreview = new PrintPreviewDialog();
        private PageSetupDialog dlgPageSetup = new PageSetupDialog();
        private FontDialog dlgFont = new FontDialog();
        private PrintDocument docPrint = new PrintDocument();
        private ColorDialog dlgColor = new ColorDialog();

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_view.rtbDoc.Modified)
                {
                    DialogResult answer = MessageBox.Show("Save current document before creating new document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.No)
                    {
                        currentFile = "";
                        _view.tbrFileName.Text = "Editor: New Document";
                        _view.rtbDoc.Modified = false;
                        _view.rtbDoc.Clear();
                        return;
                    }
                    else
                    {
                        SaveToolStripMenuItem_Click(this, EventArgs.Empty);
                        _view.rtbDoc.Modified = false;
                        _view.rtbDoc.Clear();
                        currentFile = "";
                        _view.tbrFileName.Text = "Editor: New Document";
                        return;
                    }
                }
                else
                {
                    currentFile = "";
                    _view.tbrFileName.Text = "Editor: New Document";
                    _view.rtbDoc.Modified = false;
                    _view.rtbDoc.Clear();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_view.rtbDoc.Modified)
                {
                    DialogResult answer = MessageBox.Show("Save current file before opening another document?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.No)
                    {
                        _view.rtbDoc.Modified = false;
                        OpenFile();
                    }
                    else
                    {
                        SaveToolStripMenuItem_Click(this, new EventArgs());
                        OpenFile();
                    }
                }
                else
                {
                    OpenFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void OpenFile(string fileName)
        {
            try
            {
                _view.rtbDoc.LoadFile(fileName, RichTextBoxStreamType.RichText);
            }
            catch (Exception)
            {

                _view.rtbDoc.LoadFile(fileName, RichTextBoxStreamType.PlainText);
            }
            currentFile = fileName;
            _view.rtbDoc.Modified = false;
            _view.tbrFileName.Text = "Editor: " + currentFile;

        }

        private void OpenFile()
        {
            try
            {
                dlgOpen.Title = "RTE - Open File";
                dlgOpen.DefaultExt = "rtf";
                dlgOpen.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                dlgOpen.FilterIndex = 1;
                dlgOpen.FileName = string.Empty;

                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {

                    if (dlgOpen.FileName == "")
                    {
                        return;
                    }

                    string strExt = Path.GetExtension(dlgOpen.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        _view.rtbDoc.LoadFile(dlgOpen.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        var txtReader = new StreamReader(dlgOpen.FileName);
                        _view.rtbDoc.Text = txtReader.ReadToEnd();
                        txtReader.Close();
                        txtReader = null;
                        _view.rtbDoc.SelectionStart = 0;
                        _view.rtbDoc.SelectionLength = 0;
                    }

                    currentFile = dlgOpen.FileName;
                    _view.rtbDoc.Modified = false;
                    _view.tbrFileName.Text = "Editor: " + currentFile;
                }
                //else
                //{
                //    MessageBox.Show("Open File request cancelled by user.", "Cancelled");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(currentFile))
                {
                    SaveAsToolStripMenuItem_Click(this, e);
                    return;
                }

                try
                {
                    string strExt = Path.GetExtension(currentFile);
                    strExt = strExt.ToUpper();
                    if (strExt == ".RTF")
                    {
                        _view.rtbDoc.SaveFile(currentFile);
                    }
                    else
                    {
                    	var txtWriter = new StreamWriter(currentFile);
                        txtWriter.Write(_view.rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        _view.rtbDoc.SelectionStart = 0;
                        _view.rtbDoc.SelectionLength = 0;
                    }

                    _view.tbrFileName.Text = "Editor: " + currentFile;
                    _view.rtbDoc.Modified = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "File Save Error");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dlgSave.Title = "RTE - Save File";
                dlgSave.DefaultExt = "rtf";
                dlgSave.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";
                dlgSave.FilterIndex = 1;

                if (dlgSave.ShowDialog() == DialogResult.OK)
                {

                    if (dlgSave.FileName == "")
                    {
                        return;
                    }

                    string strExt = Path.GetExtension(dlgSave.FileName);
                    strExt = strExt.ToUpper();

                    if (strExt == ".RTF")
                    {
                        _view.rtbDoc.SaveFile(dlgSave.FileName, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        var txtWriter = new StreamWriter(dlgSave.FileName);
                        txtWriter.Write(_view.rtbDoc.Text);
                        txtWriter.Close();
                        txtWriter = null;
                        _view.rtbDoc.SelectionStart = 0;
                        _view.rtbDoc.SelectionLength = 0;
                    }

                    currentFile = dlgSave.FileName;
                    _view.rtbDoc.Modified = false;
                    _view.tbrFileName.Text = "Editor: " + currentFile;
                    MessageBox.Show(currentFile + " saved.", "File Save");
                }
                //else
                //{
                //    MessageBox.Show("Save File request cancelled by user.", "Cancelled");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_view.rtbDoc.Modified)
                {
                    DialogResult answer;
                    answer = MessageBox.Show("Save this document before closing?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        _view.rtbDoc.Modified = false;
                     //   Application.Exit();
                    }
                }
                else
                {
                    _view.rtbDoc.Modified = false;
                 //  Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectAll();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to select all document content.", "RTE - Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.Copy();
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to copy document content.", "RTE - Copy", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.Cut();
            }
            catch
            {
                MessageBox.Show("Unable to cut document content.", "RTE - Cut", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.Paste();
            }
            catch
            {
                MessageBox.Show("Unable to copy clipboard content to document.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(_view.rtbDoc.SelectionFont == null))
                {
                    dlgFont.Font = _view.rtbDoc.SelectionFont;
                }
                else
                {
                    dlgFont.Font = null;
                }
                dlgFont.ShowApply = true;
                if (dlgFont.ShowDialog() == DialogResult.OK)
                {
                    _view.rtbDoc.SelectionFont = dlgFont.Font;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void FontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dlgColor.Color = _view.rtbDoc.ForeColor;
                if (dlgColor.ShowDialog() == DialogResult.OK)
                {
                    _view.rtbDoc.SelectionColor = dlgColor.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BoldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(_view.rtbDoc.SelectionFont == null))
                {
                    Font currentFont = _view.rtbDoc.SelectionFont;

                    FontStyle newFontStyle = _view.rtbDoc.SelectionFont.Style ^ FontStyle.Bold;

                    _view.rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ItalicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(_view.rtbDoc.SelectionFont == null))
                {
                    Font currentFont = _view.rtbDoc.SelectionFont;

                    FontStyle newFontStyle = _view.rtbDoc.SelectionFont.Style ^ FontStyle.Italic;

                    _view.rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void UnderlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(_view.rtbDoc.SelectionFont == null))
                {
                    Font currentFont = _view.rtbDoc.SelectionFont;

                    FontStyle newFontStyle = _view.rtbDoc.SelectionFont.Style ^ FontStyle.Underline;

                    _view.rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(_view.rtbDoc.SelectionFont == null))
                {
                    Font currentFont = _view.rtbDoc.SelectionFont;
                    const FontStyle newFontStyle = FontStyle.Regular;
                    _view.rtbDoc.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newFontStyle);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void PageColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dlgColor.Color = _view.rtbDoc.BackColor;
                if (dlgColor.ShowDialog() == DialogResult.OK)
                {
                    _view.rtbDoc.BackColor = dlgColor.Color;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void mnuUndo_Click(object sender, EventArgs e)
        {
            try
            {
                if (_view.rtbDoc.CanUndo)
                {
                    _view.rtbDoc.Undo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void mnuRedo_Click(object sender, EventArgs e)
        {
            try
            {
                if (_view.rtbDoc.CanRedo)
                {
                    _view.rtbDoc.Redo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void LeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CenterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void RightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void AddBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.BulletIndent = 10;
                _view.rtbDoc.SelectionBullet = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void RemoveBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionBullet = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Indent0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionIndent = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Indent5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionIndent = 5;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Indent10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionIndent = 10;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Indent15ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionIndent = 15;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Indent20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _view.rtbDoc.SelectionIndent = 20;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new frmFindReplace(_view.rtbDoc);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void FindAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = new frmFindReplace(_view.rtbDoc);
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void PreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dlgPrintPreview.Document = docPrint;
                dlgPrintPreview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dlgPrint.Document = docPrint;
                if (dlgPrint.ShowDialog() == DialogResult.OK)
                {
                    docPrint.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void mnuPageSetup_Click(object sender, EventArgs e)
        {
            try
            {
                dlgPageSetup.Document = docPrint;
                dlgPageSetup.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void InsertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgOpen.Title = "RTE - Insert Image File";
            dlgOpen.DefaultExt = "rtf";
            dlgOpen.Filter = "Bitmap Files|*.bmp|JPEG Files|*.jpg|GIF Files|*.gif";
            dlgOpen.FilterIndex = 1;
            dlgOpen.ShowDialog();

            if (dlgOpen.FileName == "")
            {
                return;
            }

            try
            {
                string strImagePath = dlgOpen.FileName;
                Image img = Image.FromFile(strImagePath);
                Clipboard.SetDataObject(img);
                DataFormats.Format df;
                df = DataFormats.GetFormat(DataFormats.Bitmap);
                if (_view.rtbDoc.CanPaste(df))
                {
                    _view.rtbDoc.Paste(df);
                }
            }
            catch
            {
                MessageBox.Show("Unable to insert image format selected.", "RTE - Paste", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rtbDoc_SelectionChanged(object sender, EventArgs e)
        {
            _view.tbrBold.Checked = _view.rtbDoc.SelectionFont.Bold;
            _view.tbrItalic.Checked = _view.rtbDoc.SelectionFont.Italic;
            _view.tbrUnderline.Checked = _view.rtbDoc.SelectionFont.Underline;
        }

        private void tbrSave_Click(object sender, EventArgs e)
        {
            SaveToolStripMenuItem_Click(this, e);
        }

        private void tbrOpen_Click(object sender, EventArgs e)
        {
            OpenToolStripMenuItem_Click(this, e);
        }

        private void tbrNew_Click(object sender, EventArgs e)
        {
            NewToolStripMenuItem_Click(this, e);
        }

        private void tbrBold_Click(object sender, EventArgs e)
        {
            BoldToolStripMenuItem_Click(this, e);
        }

        private void tbrItalic_Click(object sender, EventArgs e)
        {
            ItalicToolStripMenuItem_Click(this, e);
        }

        private void tbrUnderline_Click(object sender, EventArgs e)
        {
            UnderlineToolStripMenuItem_Click(this, e);
        }

        private void tbrFont_Click(object sender, EventArgs e)
        {
            SelectFontToolStripMenuItem_Click(this, e);
        }

        private void tbrLeft_Click(object sender, EventArgs e)
        {
            _view.rtbDoc.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void tbrCenter_Click(object sender, EventArgs e)
        {
            _view.rtbDoc.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void tbrRight_Click(object sender, EventArgs e)
        {
            _view.rtbDoc.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void tbrFind_Click(object sender, EventArgs e)
        {
            var f = new frmFindReplace(_view.rtbDoc);
            f.Show();
        }

        private void tbrColor_Click(object sender, EventArgs e)
        {
            FontColorToolStripMenuItem_Click(this, new EventArgs());
        }

        private void PrintDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            checkPrint = 0;
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            checkPrint = _view.rtbDoc.Print(checkPrint, _view.rtbDoc.TextLength, e);

            if (checkPrint < _view.rtbDoc.TextLength)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (_view.rtbDoc.Modified)
                {
                    DialogResult answer = MessageBox.Show("Save current document before exiting?", "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.No)
                    {
                        _view.rtbDoc.Modified = false;
                        _view.rtbDoc.Clear();
                        return;
                    }
                    else
                    {
                        SaveToolStripMenuItem_Click(this, new EventArgs());
                    }
                }
                else
                {
                    _view.rtbDoc.Clear();
                }
                currentFile = "";
                _view.tbrFileName.Text = "Editor: New Document";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public void OpenTemplate(string templateKey, params string[] arg)
        {

        }
    }
}
