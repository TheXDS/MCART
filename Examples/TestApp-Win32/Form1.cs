using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCART.PluginSupport;
using MCART.Forms;
using MCART.Security.Password;


namespace TestApp_Win32
{
    public partial class Form1 : Form
    {
        List<IPlugin> pl;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Form1"/>.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            pl = Plugin.LoadEverything<IPlugin>();
            foreach (IPlugin j in pl)
            {
                if (j.HasInteractions)
                    mnuPlugins.DropDownItems.Add(j.UIMenu);
            }
        }

        private void MnuExit_Click(object sender, EventArgs e) => Close();

        private void MnuScreenshot_Click(object sender, EventArgs e)
        {
            // TODO: realizar captura de pantalla.
        }

        private void MnuTestPw_Click(object sender, EventArgs e)
        {
            // TODO: PasswordDialog
            //PwDialogResult r = (new PasswordDialog);
        }

        private void MnuGenPin_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Generators.Pin());
        }
        private void MnuGenPwPw_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Generators.Safe());
        }
        private void MnuGenSecurePw_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Generators.VeryComplex());
        }
    }
}
