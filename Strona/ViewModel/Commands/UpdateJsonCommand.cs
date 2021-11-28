using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Strona.Model;
using Strona.ViewModel.Commands.Helpers;
using System.IO;

namespace Strona.ViewModel.Commands
{
    public class UpdateJsonCommand : ICommand
    {
        public JsonVM ViewModel { get; set; }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (Helpers.CommandsHelpers.AskForOverwriteFile(ViewModel) == false)
                return;

            //this.ViewModel.Path = CommandsHelpers.OpenFolder();
            string dirPath = CommandsHelpers.OpenFolder();
            
            if (!Directory.Exists(dirPath))
            {
                System.Windows.MessageBox.Show("Wybrany folder nie istnieje");
                return;
            }

            string jsonPath = CommandsHelpers.OpenFile();
            if (!File.Exists(jsonPath))
            {
                System.Windows.MessageBox.Show("Wybrany folder nie istnieje");
                return;
            }

            NavItem<Image> dirFotografia = new NavItem<Image>(ItemType.image);
            NavItem<Image> dirObrazy = new NavItem<Image>(ItemType.image);
            NavItem<TextItem> dirTeksty = new NavItem<TextItem>(ItemType.text);

            NavItem<Image> jsonFotografia = new NavItem<Image>(ItemType.image);
            NavItem<Image> jsonObrazy = new NavItem<Image>(ItemType.image);
            NavItem<TextItem> jsonTeksty = new NavItem<TextItem>(ItemType.text);

            ///1. Sprawdzenie czy w pliku json są ścieżki do plików, których nie ma w folderze
            


            CommandsHelpers.GetItemsFromDirectory(dirPath, ref dirObrazy, ref dirFotografia, ref dirTeksty);
            CommandsHelpers.GetItemsFromFile(jsonPath, ref jsonObrazy, ref jsonFotografia, ref jsonTeksty);



            CommandsHelpers.UpdateItemsFromDirectory(
                ref dirFotografia,
                ref dirObrazy,
                ref dirTeksty,
                jsonFotografia,
                jsonObrazy,
                jsonTeksty
                );


            this.ViewModel.Teksty = dirTeksty;
            this.ViewModel.Fotografia = dirFotografia;
            this.ViewModel.Obrazy = dirObrazy;

            this.ViewModel.TextTags = getTextTags(dirTeksty);
            if (this.ViewModel.TextTags.Count < 0)
                this.ViewModel.SelectedTextTag = this.ViewModel.TextTags[0];
        }

        List<string> getTextTags(NavItem<TextItem> teksty)
        {
            if (teksty.Items.Count == 0)
                return new List<string>();

            List<string> tags = new List<string>();

            foreach(TextItem item in teksty.Items)
            {
                if (item.Tag != "")
                    tags.Add(item.Tag);
            }

            return tags;
        }

        public UpdateJsonCommand( JsonVM viewModel )
        {
            this.ViewModel = viewModel;
        }
    }
}
