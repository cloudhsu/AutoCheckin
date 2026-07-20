using System.Drawing;
using System.Windows.Forms;

namespace AutoCheckIn
{
    internal static class UiDefaults
    {
        public static void ApplyFixedFont(Form form)
        {
            form.AutoScaleMode = AutoScaleMode.None;
            ApplyFont(form.Controls);
        }

        private static void ApplyFont(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                var isEmphasized = control.Font.Bold;
                control.Font = new Font(
                    "Segoe UI",
                    11F,
                    isEmphasized ? FontStyle.Bold : FontStyle.Regular,
                    GraphicsUnit.Point);
                if (control.HasChildren)
                {
                    ApplyFont(control.Controls);
                }
            }
        }
    }
}
