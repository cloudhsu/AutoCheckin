using System.Windows.Forms;

namespace AutoCheckIn
{
    public partial class newForm1 : Form
    {
        public newForm1()
        {
            InitializeComponent();
            UiDefaults.ApplyFixedFont(this);
            this.Icon = Properties.Resources.cloudbox;
            this.ShowIcon = true;
        }
    }
}
