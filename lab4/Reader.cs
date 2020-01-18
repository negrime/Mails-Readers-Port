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
    class Reader : IThread
    {
        private int _readCount;
        private Thread _thread;
        private Port _port;
        private ListBox _listBox;
        private RecrateDelegate _method;
        public Reader(int readCount, int number, ListBox listBox,RecrateDelegate method, Port port)
        {
            _listBox = listBox;
            _readCount = readCount;
            _port = port;
            _method = method;
            _thread = new Thread(Read);
            _thread.Name = "Reader " + number;
            _thread.Start();
        }

        private void Read()
        {
            for (int i = _readCount; i > 0; i--)
            {
                Thread.Sleep(new Random().Next(2000, 4000));
                try
                {
                    int value = _port.Read(Thread.CurrentThread.Name);
                    _listBox.Invoke(new Action(() => _listBox.Items.Add(_thread.Name + " added: " + value)));
                }
                catch (InvalidOperationException ex)
                {
                    _listBox.Invoke(new Action(() => _listBox.Items.Add(ex.Message)));
                }

            }

            _listBox.Invoke(new Action(() => _listBox.Items.Clear()));
            _listBox.Invoke(new Action(() => _listBox.Items.Add(_thread.Name + " fineshed!")));
            _listBox.Invoke(new Action(() => _listBox.Items.Add("Total reead: " + _readCount)));
            _listBox.Invoke(new Action(() => _listBox.ForeColor = Color.Red));
            Thread.Sleep(new Random().Next(3000, 5000));
            _listBox.Invoke(new Action(() => _listBox.ForeColor = Color.Black));
            _method(this, _listBox);
            _thread.Abort();
        }

        public void Stop()
        {
            if (_thread.ThreadState != ThreadState.Suspended && _thread.ThreadState != ThreadState.AbortRequested && _thread.ThreadState != ThreadState.Stopped)
                _thread.Abort();
        }
  

        public void Pause()
        {
            if (_thread.ThreadState != ThreadState.Suspended && _thread.ThreadState != ThreadState.Stopped)
                _thread.Suspend();
        }
        public void Resume()
        {
            _thread.Resume();
        }

   
    }
}
