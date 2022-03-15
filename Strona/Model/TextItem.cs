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

    public enum FontSize
    {
        large,
        larger,
        xlLarge
    }

    public class TextItem : Item
    {
        
        TextAdjust adjutst;
        String subCaption1;
        String subCaption2;
        FontSize captionFontSize;
        FontSize subCaption1FontSize;
        FontSize subCaption2FontSize;


        public TextItem()
        {
            adjutst = TextAdjust.left;
            subCaption1 = "";
            subCaption2 = "";
            captionFontSize = FontSize.xlLarge;
            subCaption1FontSize = FontSize.xlLarge;
            subCaption2FontSize = FontSize.xlLarge;
        }

        public FontSize CaptionFontSize
        {
            get { return captionFontSize; }
            set
            {
                if (captionFontSize != value)
                {
                    captionFontSize = value;
                    RaisePropertyChanged();
                }

            }
        }

        public FontSize SubCaption1FontSize
        {
            get { return subCaption1FontSize; }
            set
            {
                if (subCaption1FontSize != value)
                {
                    subCaption1FontSize = value;
                    RaisePropertyChanged();
                }

            }
        }


        public FontSize SubCaption2FontSize
        {
            get { return subCaption2FontSize; }
            set
            {
                if (subCaption2FontSize != value)
                {
                    subCaption2FontSize = value;
                    RaisePropertyChanged();
                }

            }
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

        public string SubCaption1
        {
            get { return subCaption1; }
            set
            {
                if (subCaption1 != value)
                {
                    subCaption1 = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string SubCaption2
        {
            get { return subCaption2; }
            set
            {
                if (subCaption2 != value)
                {
                    subCaption2 = value;
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

        [JsonIgnore]
        public int CaptionFontSizeI
        {
            get { return (int)captionFontSize; }
            set
            {
                if ((int)captionFontSize != value)
                {
                    switch (value)
                    {
                        case 0:
                            captionFontSize = FontSize.large;
                            break;
                        case 1:
                            captionFontSize = FontSize.larger;
                            break;
                        default:
                            captionFontSize = FontSize.xlLarge;
                            break;
                    }
                    RaisePropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public int SubCaption1FontSizeI
        {
            get { return (int)subCaption1FontSize; }
            set
            {
                if ((int)subCaption1FontSize != value)
                {
                    switch (value)
                    {
                        case 0:
                            subCaption1FontSize = FontSize.large;
                            break;
                        case 1:
                            subCaption1FontSize = FontSize.larger;
                            break;
                        default:
                            subCaption1FontSize = FontSize.xlLarge;
                            break;
                    }
                    RaisePropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public int SubCaption2FontSizeI
        {
            get { return (int)subCaption2FontSize; }
            set
            {
                if ((int)subCaption2FontSize != value)
                {
                    switch (value)
                    {
                        case 0:
                            subCaption2FontSize = FontSize.large;
                            break;
                        case 1:
                            subCaption2FontSize = FontSize.larger;
                            break;
                        default:
                            subCaption2FontSize = FontSize.xlLarge;
                            break;
                    }
                    RaisePropertyChanged();
                }
            }
        }

    }
}
