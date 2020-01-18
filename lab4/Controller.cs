using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public delegate void RecrateDelegate(Object sender, ListBox listBox);
    class Controller : IThread
    {
        private int _clientsCount;
        private LinkedList<Mail> _mails;
        private LinkedList<Reader> _readers;
        private Port _port;
        private RecrateDelegate _method;
        private int readeresCount;
        public Controller(int clientsCount = 3 ,ListBox portListBox = null)
        {
            _mails = new LinkedList<Mail>();
            _readers = new LinkedList<Reader>();
            _clientsCount = clientsCount;
            _port = new Port(1, 5, portListBox);
            _method = Recreate;
        }
        

        public void Start(ListBox[] listBoxesMails, ListBox[] listBoxesReaders)
        {
            for (int i = 0; i < _clientsCount; i++)
            {
                _mails.AddLast(new Mail(i,listBoxesMails[i], _port));
                _readers.AddLast(new Reader(new Random().Next(3, 7), i,listBoxesReaders[i], _method, _port));
                readeresCount++;
            }
        }

        private void Recreate(Object sender, ListBox listBox)
        {
            listBox.Invoke(new Action(() => listBox.Items.Clear()));
            _readers.Remove((Reader)sender);
            readeresCount++;
            _readers.AddLast(new Reader(new Random().Next(2, 6), readeresCount, listBox, _method, _port));
        }
        public void Stop()
        {
            foreach (var item in _mails)
            {
                item.Stop();
            }

            foreach (var item in _readers)
            {
                item.Stop();
            }
        }

        public void Pause()
        {
            foreach (var item in _mails)
            {
                item.Pause();
            }

            foreach (var item in _readers)
            {
                item.Pause();
            }
        }

        public void Resume()
        {
            foreach (var item in _mails)
            {
                item.Resume();
            }

            foreach (var item in _readers)
            {
                item.Resume();
            }
        }
    }
}
