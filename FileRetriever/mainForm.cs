using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Win32;
using System.IO;
namespace FileRetriever
{
    public partial class mainForm : Form 
    {   //agregar al registery para que windows lo corra en startUp
        RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        private NotifyIcon trayIcon;
        private int count = 0;
        public mainForm()
        {
            InitializeComponent();

            //icono notification
            trayIcon = new NotifyIcon();
            trayIcon.Visible = true;
            trayIcon.Icon = SystemIcons.Application; 
            trayIcon.Text = "FileRetriever";


            
            
        }
       

        private void mainForm_Load(object sender, EventArgs e)
        {
            //agregar app en el path de windows
            reg.SetValue("Test FileRetriever", Application.ExecutablePath.ToString());

            //Settings para las notificaciones esto se ejecuta al iniciar la app
            trayIcon.BalloonTipTitle = "FileRetriever";
            trayIcon.BalloonTipText = "The application has started successfully.";
            trayIcon.BalloonTipIcon = ToolTipIcon.Info; // Info, Warning, Error, None
            trayIcon.ShowBalloonTip(3000);

            //Style
            dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.AlternatingRowsDefaultCellStyle.BackColor = SystemColors.Control;

            cmbIntervals.SelectedIndex = 0;


        }


       

        private void btnAdd_Click(object sender, EventArgs e)
        {
             
            dgvItems.Rows.Add("Repositorio " + (dgvItems.Rows.Count + 1));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseFrom_Click(object sender, EventArgs e)
        {
            string path  = searchPath();
            txtRepositoryFrom.Text = path;
        }

        private void btnBrowseTo_Click(object sender, EventArgs e)
        {
            string path = searchPath();
            txtRepositoryTo.Text = path;
        }

        private string searchPath()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.ValidateNames = false;
                ofd.CheckFileExists = false;
                ofd.FileName = "Select this folder";
                ofd.Title = "Select Directory";

                if (ofd.ShowDialog(this) == DialogResult.OK)
                {

                    if (!string.IsNullOrEmpty(ofd.FileName))
                    {
                        var path = Path.GetDirectoryName(ofd.FileName);
                        return path;
                    }
                }

            }

            return null;
        }
    }
}
