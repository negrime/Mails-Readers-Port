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
    class Port
    {
        private Queue<int> _elements;
        private Semaphore _mails;
        private Semaphore _readers;
        private Mutex _mutex;
        private ListBox _listBox;
        private Logger _logger;




        public Port(int mailsMaxCount, int readersMaxCount, ListBox listBox)
        {
            _listBox = listBox;
            _elements = new Queue<int>();
            _mails = new Semaphore(mailsMaxCount, mailsMaxCount);
            _readers = new Semaphore(1, readersMaxCount);
            _mutex = new Mutex();
            _logger = Logger.GetLogger();
        }

        public void Write(int value, string threadName)
        {
            _mutex.WaitOne();
            _mails.WaitOne();

            _elements.Enqueue(value);
            try
            {
                _listBox.Invoke(new Action(() => _listBox.Items.Add(value)));
                _listBox.Invoke(new Action(() => _listBox.SelectedIndex = 0));
            }
            catch (Exception)
            {

            }
            _mails.Release();
            _mutex.ReleaseMutex();
            _logger.Log(threadName + " added value: " + value);
            
        }

        public int Read(string threadName)
        {
            _mutex.WaitOne();
            _readers.WaitOne();
            if (_elements.Count < 1)
            {
                _readers.Release();
                _mutex.ReleaseMutex();
                throw new InvalidOperationException("Port is empty!");
            }

            int result = _elements.Dequeue();
            try
            {
                _listBox.Invoke(new Action(() => _listBox.Items.RemoveAt(0)));
            }
            catch (Exception)
            {

            }
            _readers.Release();
            _mutex.ReleaseMutex();
            _logger.Log(threadName + " removed value: " + result);

            return result;
        }

     

    }
}
