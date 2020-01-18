using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    class Mail : IThread
    {
        private Thread _thread;
        private Port _port;
        private ListBox _listBox;
        public Mail(int number, ListBox listBox ,Port port)
        {
            _listBox = listBox;
            _port = port;
            _thread = new Thread(Write);
            _thread.Name = "Mail " + number;
            _thread.Start();
        }

        private void Write()
        {
            while (true)
            {
                int value = DateTime.Now.Millisecond;
                _port.Write(value, Thread.CurrentThread.Name);
                try
                {
                    _listBox.Invoke(new Action(() => _listBox.Items.Add(_thread.Name + " added value: "+ value)));
                    _listBox.Invoke(new Action(() => _listBox.SelectedIndex = _listBox.Items.Count - 1));
                }
                catch (Exception)
                {

                }
                Thread.Sleep(new Random().Next(1000, 6000));
            }
        }

        public void Stop()
        {
            if (_thread.ThreadState !=ThreadState.Suspended && _thread.ThreadState != ThreadState.AbortRequested && _thread.ThreadState != ThreadState.Stopped && _thread.ThreadState != ThreadState.WaitSleepJoin)
                _thread.Abort();
        }

        public void Pause()
        {
            if (_thread.ThreadState != ThreadState.Suspended && _thread.ThreadState != ThreadState.Stopped)
                _thread.Suspend();
        }
        

        public void Resume()
        {
            if (_thread.ThreadState != ThreadState.Stopped)
                _thread.Resume();
        }
    }
}
