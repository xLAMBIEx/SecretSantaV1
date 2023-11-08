using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretSantaV1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageFlyout : ContentPage
    {
        public ListView ListView;

        public MasterPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MasterPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MasterPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterPageFlyoutMenuItem> MenuItems { get; set; }
            
            public MasterPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MasterPageFlyoutMenuItem>(new[]
                {
                    new MasterPageFlyoutMenuItem { Id = 0, Title = "Manage Santas", SubMenuItems = new List<SubMenuItem>(new[]
                                                                                    { 
                                                                                        new SubMenuItem { Id = 0, Title = "Create Santa" },
                                                                                        new SubMenuItem { Id = 0, Title = "Edit Santa" },
                                                                                        new SubMenuItem { Id = 0, Title = "Remove Santa" },
                                                                                    }) 
                                                 },
                    new MasterPageFlyoutMenuItem { Id = 1, Title = "Manage Santa Groups", SubMenuItems = new List<SubMenuItem>(new[]
                                                                                    {
                                                                                        new SubMenuItem { Id = 0, Title = "Create Group" },
                                                                                        new SubMenuItem { Id = 0, Title = "Edit Group" },
                                                                                        new SubMenuItem { Id = 0, Title = "Remove Group" },
                                                                                    }) 
                                                 },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}