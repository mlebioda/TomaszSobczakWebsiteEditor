using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strona.Model
{
    public class JsonItem
    {
        NavItem<Image> obrazy;
        NavItem<Image> fotografia;
        NavItem<TextItem> teksty;
        NavItem<Image> artysta;
        ObservableCollection<EventItem> events;

        public JsonItem(
            NavItem<Image> o,
            NavItem<Image> f,
            NavItem<TextItem> t,
            NavItem<Image> a,
            ObservableCollection<EventItem> e
            )
        {
            obrazy = o;
            fotografia = f;
            teksty = t;
            artysta = a;
            events = e;
        }

        public NavItem<Image> Obrazy
        {
            get { return obrazy; }
            set
            {
                if (obrazy != value)
                {
                    obrazy = value;
                }
            }
        }
        
        public NavItem<Image> Artysta
        {
            get { return artysta; }
            set
            {
                if (artysta != value)
                {
                    artysta = value;
                }
            }
        }

        public NavItem<Image> Fotografia
        {
            get { return fotografia; }
            set
            {
                if (fotografia != value)
                {
                    fotografia = value;
                }
            }
        }

        public NavItem<TextItem> Teksty
        {
            get { return teksty; }
            set
            {
                if (teksty != value)
                {
                    teksty = value;
                }
            }
        }
        
        public ObservableCollection<EventItem> Events
        {
            get { return events; }
            set
            {
                if (events != value)
                {
                    events = value;
                }
            }
        }
    }
}
