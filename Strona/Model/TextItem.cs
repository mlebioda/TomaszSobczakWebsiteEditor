using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Strona.Model
{
    public enum TextAdjust
    {
        left,
        center,
        right
    }
    public class TextItem : Item
    {
        
        TextAdjust adjutst;

        public TextItem()
        {
            adjutst = TextAdjust.left;
            
        }
        
       

        public TextAdjust Adjust
        {
            get { return adjutst; }
            set
            {
                if (adjutst != value)
                {
                    adjutst = value;
                    RaisePropertyChanged();
                }
                
            }
        }

        [JsonIgnore]
        public int AdjustI
        {
            get { return (int)adjutst; }
            set
            {
                if((int)adjutst!=value)
                {
                    switch(value)
                    {
                        case 0:
                            adjutst = TextAdjust.left;
                            break;
                        case 1:
                            adjutst = TextAdjust.center;
                            break;
                        default:
                            adjutst = TextAdjust.right;
                            break;
                    }
                    RaisePropertyChanged();
                }
            }
        }


    }
}
