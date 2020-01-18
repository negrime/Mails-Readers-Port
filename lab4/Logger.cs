using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    class Logger
    {
        private static Logger _instance;
        private ListBox _listBox;

        public static Logger GetLogger(ListBox listBox = null)
        {
            if (_instance == null)
                _instance = new Logger(listBox);
            return _instance;
        }
        
        protected Logger(ListBox listBox)
        {
            _listBox = listBox;
        }

        public void Log(string message)
        {
            try
            {
                _listBox.Invoke(new Action(() => _listBox.Items.Add(message)));
                _listBox.Invoke(new Action(() => _listBox.SelectedIndex = _listBox.Items.Count - 1));
            }
            catch (Exception)
            {

            }
        }
    }
}
