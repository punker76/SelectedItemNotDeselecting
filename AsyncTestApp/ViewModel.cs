using AsyncTestApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Data;
using System.Windows.Threading;

namespace TestApp
{
    public class ViewModel : ModelBase
    {
        private List<Window> _data = new List<Window>()
            {
                new Window
                {
                    Id = 1,
                    WindowHandle = (IntPtr)1,
                    Description = "This one changes",
                    Dimensions = new Rect(10, 20, 30, 40)
                },
                new Window
                {
                    Id = 2,
                    WindowHandle = (IntPtr)2,
                    Description = "Random window 1",
                    Dimensions = new Rect(20, 30, 40, 50)
                },
                new Window
                {
                    Id = 3,
                    WindowHandle = (IntPtr)3,
                    Description = "Random window 2",
                    Dimensions = new Rect(30, 40, 50, 60)
                },
            };

        private ObservableCollection<Window> _items = new ObservableCollection<Window>();

        public ObservableCollection<Window> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        private ObservableCollection<Window> _selectedItems = new ObservableCollection<Window>();

        public ObservableCollection<Window> SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        public string ItemFilter
        {
            get => _windowFilter;
            set
            {
                SetProperty(ref _windowFilter, value);
                FilteredItems.Refresh();
            }
        }

        private string _windowFilter = string.Empty;

        ///<inheritdoc/>
        public ICollectionView FilteredItems { get; set; }

        public ViewModel()
        {
            FilteredItems = CollectionViewSource.GetDefaultView(Items);
            FilteredItems.Filter = w =>
            {
                if(string.IsNullOrEmpty(ItemFilter))
                {
                    return true;
                }

                var window = w as Window;
                return window?.Description.Contains(ItemFilter, StringComparison.OrdinalIgnoreCase) ?? false;
            };

            ManualRefreshCommand = new DelegateCommand(ManualRefresh);
            ChangeItemCommand = new DelegateCommand(ChangeItem);
        }


        public DelegateCommand ManualRefreshCommand { get; }

        private void ManualRefresh()
        {
            Items.UpdateCollection(_data);
        }

        public DelegateCommand ChangeItemCommand { get; }

        private void ChangeItem()
        {
            var rand = new Random();

            Items[0].Dimensions = new Rect(rand.Next(1, 100), rand.Next(1, 100), rand.Next(1, 100), rand.Next(1, 100));
        }
    }
}
