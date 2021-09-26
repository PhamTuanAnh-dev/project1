using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Process[] proc;

        void getAllProcess()
        {
            proc = Process.GetProcesses();
            listBox.Items.Clear();
            foreach (var process in proc)
            {
                listBox.Items.Add(process.ProcessName);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            getAllProcess();
        }

        private void btn_EndTask_Click(object sender, EventArgs e)
        {
            try
            {
                EndProcessTree(listBox.SelectedItem.ToString() + ".exe");
                getAllProcess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void runNewTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Run_New_Task form = new Run_New_Task())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    getAllProcess();
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getAllProcess();
        }

        private static void EndProcessTree(string imageName)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "taskkill",
                Arguments = $"/im {imageName} /f /t",
                CreateNoWindow = true,
                UseShellExecute = false
            }).WaitForExit();
        }
    }
}
