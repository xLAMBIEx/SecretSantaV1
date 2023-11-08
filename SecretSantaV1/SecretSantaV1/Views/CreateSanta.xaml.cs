using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSantaV1.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecretSantaV1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateSanta : ContentPage
    {
        private List<Santa> currentSantas;

        public CreateSanta()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                currentSantas = HelperClass.DeserializeFromFile<List<Santa>>("SecretSantas");
            }
            catch (Exception ex)
            {

            }

            if (currentSantas == null)
            {
                currentSantas = new List<Santa>();
            }
        }

        private void btnCreateSanta_Clicked(object sender, EventArgs e)
        {                                 
            Santa newSanta = new Santa();

            newSanta.setName(txtName.Text.Trim());
            newSanta.setSurname(txtSurname.Text.Trim());
            newSanta.setEmail(txtEmail.Text.Trim());
            newSanta.setMobileNumber(txtMobile.Text.Trim());
            
            if(CheckSantaExists(newSanta.DisplayName, currentSantas)) 
            {
                return;
            }

            currentSantas.Add(newSanta);

            try
            {
                HelperClass.SerializeToFile("SecretSantas", currentSantas);
            }
            catch (Exception ex)
            {
                
            }

            CLearTextFields();
        }

        private bool CheckSantaExists(string santaFullName, List<Santa> existingSantas)
        {
            bool ret = false;

            for (int i = 0; i < existingSantas.Count; i++)
            {
                if(string.Equals(santaFullName, existingSantas[i].DisplayName))
                {
                    ret = true;
                }
            }

            return ret;
        }

        private void CLearTextFields()
        {
            txtName.Text = string.Empty;
            txtSurname.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void btnEditSanta_Clicked(object sender, EventArgs e)
        {
            string enteredSanta = txtName.Text + " " + txtSurname.Text;
            for(int i = 0; i < currentSantas.Count; i++)
            {
                if(enteredSanta == currentSantas[i].DisplayName)
                {
                    currentSantas[i].setEmail(txtEmail.Text);
                    currentSantas[i].setMobileNumber(txtMobile.Text);

                    try
                    {
                        HelperClass.SerializeToFile("SecretSantas", currentSantas);
                    }
                    catch (Exception ex)
                    {

                    }

                    CLearTextFields();
                }
            }
        }
    }
}