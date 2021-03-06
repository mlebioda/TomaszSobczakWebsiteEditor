using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Strona.ViewModel.Commands;
using Strona.Model;
using System.Windows.Threading;
using System.Windows;
using Strona.ViewModel.Commands.Helpers;
using System.IO;
using System.Collections.ObjectModel;

namespace Strona.ViewModel
{
    public class JsonVM : INotifyPropertyChanged
    {
        string path;
        NavItem<Image> obrazy;
        NavItem<Image> fotografia;
        NavItem<TextItem> teksty;
        NavItem<Image> artysta;
        ObservableCollection<EventItem> events;
        // List<string> tags;
        DispatcherTimer autosaveTimer;
        List<string> textTags;
        string selectedTextTag;
        int selectedContentAdjust;

        Image obrazySelectedItem;
        TextItem tekstySelectedItem;
        Image fotografiaSelectedItem;
        Image artystaSelectedItem;
        EventItem eventSelectedItem;

        int obrazySelectedIndex; 
        int tekstySelectedIndex; 
        int fotografiaSelectedIndex; 
        int artystaSelectedIndex; 
        int eventSelectedIndex;

        public OpenFolderCommand OpenFolderCommand { get; set; }
        public SaveFileCommand SaveFileCommand { get; set; }
        public OpenJsonCommand OpenJsonCommand { get; set; }
        public ReplaceAdjustCommand ReplaceAdjustCommand { get; set; }
        public UpdateJsonCommand UpdateJsonCommand { get; set; }
        public AddEventCommand AddEventCommand { get; set; }
        public RemoveEventCommand RemoveEventCommand { get; set; }
        public UnselectCommand UnselectCommand { get; set; }
        public JsonVM()
        {
            path = "";
            obrazy = new NavItem<Image>(ItemType.image);
            fotografia = new NavItem<Image>(ItemType.image);
            artysta = new NavItem<Image>(ItemType.image);
            teksty = new NavItem<TextItem>(ItemType.text);
            events = new ObservableCollection<EventItem>();

            OpenFolderCommand = new OpenFolderCommand(this);
            SaveFileCommand = new SaveFileCommand(this);
            OpenJsonCommand = new OpenJsonCommand(this);
            ReplaceAdjustCommand = new ReplaceAdjustCommand(this);
            UpdateJsonCommand = new UpdateJsonCommand(this);
            AddEventCommand = new AddEventCommand(this);
            RemoveEventCommand = new RemoveEventCommand(this);
            UnselectCommand = new UnselectCommand(this);
           // tags = new List<string>();
            textTags = new List<string>();
            selectedContentAdjust = (int)TextAdjust.left;
            selectedTextTag = "";

            obrazySelectedItem = new Image();
            tekstySelectedItem = new TextItem();
            fotografiaSelectedItem = new Image();
            artystaSelectedItem = new Image();
            eventSelectedItem = new EventItem();

            obrazySelectedIndex = 0;
            tekstySelectedIndex = 0;
            fotografiaSelectedIndex = 0;
            artystaSelectedIndex = 0;
            eventSelectedIndex = 0;
            autosaveTimer = new DispatcherTimer(
                TimeSpan.FromSeconds(900), 
                DispatcherPriority.Background, new EventHandler(DoAutoSave), 
                Application.Current.Dispatcher);


        }
        void DoAutoSave(object sender, EventArgs e)
        {
            if(isValid(false))
            {

                    JsonItem jsonItem = new JsonItem(Obrazy, Fotografia, Teksty, Artysta, Events);
                    CommandsHelpers.SaveJson(jsonItem);
                
            }
        }

        /// <summary>
        /// Check if naVitems have nodes
        /// </summary>
        /// <param name="and"> if true operator is && else ||</param>
        /// <returns></returns>
        public bool isValid(bool and)
        {
            if (and)
            {
                if (fotografia.isValid() && obrazy.isValid() && teksty.isValid())
                {
                    return true;
                }
            }
            else
            {
                if (fotografia.isValid() || obrazy.isValid() || teksty.isValid())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Path to selected directory/File
        /// </summary>
        public string Path
        {
            get { return path; }
            
            set
            {
                if( !value.Equals("") && !value.Equals(path) )
                {
                    path = value;


                    RaisePropertyChanged();
                }
            }
        }

        //Tags used in Teksty (unique)
        public List<string> TextTags
        {
            get { return textTags; }
            set
            {
                if(textTags!=value)
                {
                    textTags = value;
                    RaisePropertyChanged();
                }
            }
        }


        public string SelectedTextTag
        {
            get { return selectedTextTag; }
            set
            {
                if (selectedTextTag != value)
                {
                    selectedTextTag = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int SelectedContentAdjust
        {
            get { return selectedContentAdjust; }
            set
            {
                if (selectedContentAdjust != value)
                {
                    selectedContentAdjust = value;
                    RaisePropertyChanged();
                }
            }
        }

        public NavItem<Image> Obrazy
        {
            get { return obrazy; }
            set
            {
                if( obrazy != value )
                {
                    obrazy = value;
                    ObrazyItems = obrazy.Items;
                    RaisePropertyChanged();
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
                    FotografiaItems = fotografia.Items;
                    RaisePropertyChanged();
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
                    ArtystaItems = artysta.Items;
                    RaisePropertyChanged();
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
                    this.TekstyItems = teksty.Items;
                    RaisePropertyChanged();
                }
            }
        }

        public ObservableCollection<EventItem> Events
        {
            get { return events;}
            set
            {
                events = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TextItem> TekstyItems
        {
            get { return teksty.Items; }
            set
            {
             //   if (teksty.Images != value)
              //  {
                    teksty.Items = value;
                    RaisePropertyChanged();
               // }
            }
        }
        public ObservableCollection<Image> ObrazyItems
        {
            get { return obrazy.Items; }
            set
            {
               // if (obrazy.Images != value)
               // {
                    obrazy.Items = value;
                    RaisePropertyChanged();
              //  }
            }
        }
        public ObservableCollection<Image> FotografiaItems
        {
            get { return fotografia.Items; }
            set
            {
           //     if (fotografia.Images != value)
            //    {
                    fotografia.Items = value;
                    RaisePropertyChanged();
             //   }
            }
        }
        public ObservableCollection<Image> ArtystaItems
        {
            get { return artysta.Items; }
            set
            {

                artysta.Items = value;
                    
                for(int i = 0;  i<artysta.Items.Count; i++)
                {
                    artysta.Items[i].Caption = "";
                    artysta.Items[i].Alt = "";
                    artysta.Items[i].Size = "";
                    artysta.Items[i].Material = "";
                }
                    
                RaisePropertyChanged();
            }
        }

        public TextItem TekstySelectedItem
        {
            get { return tekstySelectedItem; }
            set
            {
                if (tekstySelectedItem != value)
                {
                    tekstySelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        } 

        public EventItem EventSelectedItem
        {
            get { return eventSelectedItem; }
            set
            {
                if (eventSelectedItem != value)
                {
                    eventSelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        } 

        public List<string> AdjustList
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("Do lewej strony");
                list.Add("Do środka");
                list.Add("Do prawej strony");
                return list;
                
            }
            set { }
        }

        public List<string> FontSizeList
        {
            get
            {
                List<string> list = new List<string>();
                list.Add("Średni");
                list.Add("Duży");
                list.Add("Największy");
                return list;

            }
            set { }
        }

        public Image ObrazySelectedItem
        {
            get { return obrazySelectedItem; }
            set
            {
                if (obrazySelectedItem != value)
                {
                    obrazySelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        } 
        public Image FotografiaSelectedItem
        {
            get { return fotografiaSelectedItem; }
            set
            {
                if (fotografiaSelectedItem != value)
                {
                    fotografiaSelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public Image ArtystaSelectedItem
        {
            get { return artystaSelectedItem; }
            set
            {
                if (artystaSelectedItem != value)
                {
                    artystaSelectedItem = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int ObrazySelectedIndex
        {
            get { return obrazySelectedIndex; }
            set
            {
                if (obrazySelectedIndex != value)
                {
                    obrazySelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int TekstySelectedIndex
        {
            get { return tekstySelectedIndex; }
            set
            {
                if (tekstySelectedIndex != value)
                {
                    tekstySelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }
        public int FotografiaSelectedIndex
        {
            get { return fotografiaSelectedIndex; }
            set
            {
                if (fotografiaSelectedIndex != value)
                {
                    fotografiaSelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }
        
        public int ArtystaSelectedIndex
        {
            get { return artystaSelectedIndex; }
            set
            {
                if (artystaSelectedIndex != value)
                {
                    artystaSelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }

        public int EventSelectedIndex
        {
            get { return eventSelectedIndex; }
            set
            {
                if (eventSelectedIndex != value)
                {
                    eventSelectedIndex = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
