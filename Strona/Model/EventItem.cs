using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strona.Model
{
    public class EventItem : INotifyPropertyChanged
    {
        string m_date;
        string m_name;

        public EventItem(string date, string name)
        {
            m_date = date;
            m_name = name;
        }


        public string Date
        {
            get { return m_date; }
            set
            {
                if (m_date != value)
                {
                    m_date = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return m_name; }
            set
            {
                if (m_name != value)
                {
                    m_name = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
