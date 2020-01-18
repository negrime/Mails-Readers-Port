using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        private Controller _controller;
        const int clientsCount = 3;
        private ListBox[] listBoxMails;
        private ListBox[] listBoxReaders;
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            _controller =  new Controller(clientsCount, listBox1);
            _controller.Start(listBoxMails, listBoxReaders);
            buttonStart.Enabled = !buttonStart.Enabled;
            buttonStop.Enabled = !buttonStop.Enabled;
            buttonPause.Enabled = !buttonPause.Enabled;
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            _controller.Stop();
            ClearUi();
            buttonStart.Enabled = !buttonStart.Enabled;
            buttonStop.Enabled = !buttonStop.Enabled;
            Application.Restart();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Logger.GetLogger(listBoxLog);
            listBoxMails = CreateListBoxes(new Point(12, 62));
            listBoxReaders = CreateListBoxes(new Point(465, 62));
        }


        private void ClearUi()
        {
            for (int i = 0; i < clientsCount; i++)
            {
                listBoxMails[i].Items.Clear();
                listBoxReaders[i].Items.Clear();
            }
            listBoxLog.Items.Clear();
            listBox1.Items.Clear();
        }

        private ListBox[] CreateListBoxes(Point startPosition)
        {
            ListBox[] result = new ListBox[clientsCount];
            for (int i = 0; i < result.Length; ++i)
            {
                ListBox listBox = new ListBox();
                listBox.Location = new System.Drawing.Point(startPosition.X, startPosition.Y + i * 80 );
                listBox.Size = new System.Drawing.Size(154, 80);
                Controls.Add(listBox);
                result[i] = listBox;
            }

            return result;
        }

        private void ButtonPause_Click(object sender, EventArgs e)
        {
            _controller.Pause();
            buttonPause.Enabled = !buttonPause.Enabled;
            buttonResume.Enabled = !buttonResume.Enabled;
            buttonResume.Visible = true;
            buttonStart.Visible = false;
        }

        private void ButtonContinue_Click(object sender, EventArgs e)
        {
            _controller.Resume();
            buttonResume.Enabled = !buttonResume.Enabled;
            buttonPause.Enabled = !buttonPause.Enabled;
        }
    }
}
