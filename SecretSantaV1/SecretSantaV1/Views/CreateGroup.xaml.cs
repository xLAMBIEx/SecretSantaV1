using SecretSantaV1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Internals.GIFBitmap;

namespace SecretSantaV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateGroup : ContentPage
	{
        private List<Santa> allSantas = new List<Santa>();
		private List<Santa> groupSantas = new List<Santa>();
		private Santa selectedSanta = new Santa();

        public CreateGroup ()
		{
			InitializeComponent ();
			LoadData();
		}

		public void LoadData()
		{
            try
            {
                allSantas = HelperClass.DeserializeFromFile<List<Santa>>("SecretSantas");

				pckSantas.ItemsSource = allSantas; //TRY THIS IF BELOW DOES NOT WORK
				//pckSantas.SetBinding(Picker.ItemsSourceProperty, "allSantas");
				//pckSantas.SetBinding(Picker.SelectedItemProperty, "selectedSanta");
				pckSantas.ItemDisplayBinding = new Binding("DisplayName");
            }
            catch(Exception ex)
            {

            }
        }

		public void ClearData()
		{
			allSantas.Clear();
			groupSantas.Clear();
			selectedSanta = null;
		}

		private void btnAddSanta_Clicked(object sender, EventArgs e)
		{
            for (int i = 0; i < groupSantas.Count; i++)
            {
                if (string.Equals(selectedSanta.DisplayName, groupSantas[i].DisplayName))
                {
					return;
                }
            }

            selectedSanta.setNum(groupSantas.Count + 1);
			groupSantas.Add(selectedSanta);
		}

		private void btnSaveGroup_Clicked(object sender, EventArgs e)
		{
			SantaGroup santaGroup = new SantaGroup();
			List<SantaGroup> santaGroups = new List<SantaGroup>(); 

			santaGroup.GroupName = txtGroupName.Text.Trim();
			santaGroup.Description = txtDescription.Text.Trim();
			santaGroup.Santas = groupSantas;

			try
			{
				santaGroups = HelperClass.DeserializeFromFile<List<SantaGroup>>("SecretSantaGroups");

				if(santaGroups == null)
				{
					santaGroups = new List<SantaGroup>();
				}

				santaGroups.Add(santaGroup);

                HelperClass.SerializeToFile("SecretSantaGroups", santaGroups);
            }
			catch (Exception ex)
            {
				
			}
		}

		private bool CheckSantaAlreadyInGroup(SantaGroup santaGroup, string santaFullName)
		{
			bool ret = false;

			for(int i = 0; i< santaGroup.Santas.Count; i++)
			{
				if(string.Equals(santaFullName, santaGroup.Santas[i].DisplayName))
				{
					ret = true;
				}
			}

			return ret;
		}

        private void pckSantas_SelectedIndexChanged(object sender, EventArgs e)
        {
			if(pckSantas.SelectedIndex != -1)
			{
				selectedSanta = allSantas[pckSantas.SelectedIndex];
			}
        }
    }
}