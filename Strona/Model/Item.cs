using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Strona.Model
{
    public enum ItemType
    {
        image, text
    }
    public class Item : INotifyPropertyChanged
    {
        string caption;
        string src;
        string tag;

        public Item()
        {
            caption = "";
            src = "";
            tag = "";

        }

        public string Caption
        {
            get { return caption; }
            set
            {
                if (caption != value)
                {
                    string preparedCaption = value;
                    if(preparedCaption.Contains("_"))
                    {
                        preparedCaption = preparedCaption.Replace('_', ' ');
                    }
                    caption = preparedCaption;
                    RaisePropertyChanged();
                }
            }
        }
        public string Tag
        {
            get { return tag; }
            set
            {
                if (tag != value)
                {
                    tag = value;
                    RaisePropertyChanged();
                }

            }
        }


        public string Src
        {
            get { return src; }
            set
            {
                if (src != value)
                {
                    src = value;
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
