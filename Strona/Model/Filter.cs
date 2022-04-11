using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Strona.Model
{
    public class Filter
    {
        string m_caption;
        string m_tag;
        Dropdown m_dropdown;

        public Filter()
        {
            m_caption = "";
            m_tag = "";
        }

        public string Caption
        {
            get { return m_caption; }
            set
            {
                if (m_caption != value)
                {
                    string preparedCaption = value;
                    if(preparedCaption.Contains("_"))
                    {
                        preparedCaption = preparedCaption.Replace('_', ' ');
                    }
                    m_caption = preparedCaption;
                    RaisePropertyChanged();
                }
            }
        }
        public string Tag
        {
            get { return m_tag; }
            set
            {
                if (m_tag != value)
                {
                    m_tag = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Dropdown Dropdown
        {
            get { return m_dropdown; }
            set
            {
                if (m_dropdown != value)
                {
                    m_dropdown = value;
                    RaisePropertyChanged();
                }
            }
        }

        public bool IsDropdown()
        {
            if (m_dropdown == null)
                return false;
            else
                return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
