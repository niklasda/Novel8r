using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Novel8r.WinForms.Views.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
    public class ToolStripCheckedBox : MyToolStripControlHost
    {
        public ToolStripCheckedBox()
            : base(new CheckBox())
        {
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckBox CheckBoxControl
        {
            get { return Control as CheckBox; }
        }

        public bool Checked
        {
            get { return CheckBoxControl.Checked; }
            set { CheckBoxControl.Checked = value; }
        }
    }

    // Necessary in order to not crash design-time
    public class MyToolStripControlHost : ToolStripControlHost
    {
        public MyToolStripControlHost()
            : base(new Control())
        {
        }

        public MyToolStripControlHost(Control c)
            : base(c)
        {
        }
    }
}