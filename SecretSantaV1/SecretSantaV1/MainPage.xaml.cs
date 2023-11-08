using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using SecretSantaV1.Views;
using SecretSantaV1.Models;

namespace SecretSantaV1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadSantaGroups();
        }

        public bool bNewRandom = false;
        public bool bCouple = false;
        public bool bSelf = true;

        public List<Santa> santas = new List<Santa>();
        public bool errorFlag = false;

        private List<SantaGroup> santaGroups = new List<SantaGroup>();

        private void LoadSantaGroups()
        {           
            try
            {
                santaGroups = HelperClass.DeserializeFromFile<List<SantaGroup>>("SecretSantaGroups");

                pckSantaGroups.ItemsSource = santaGroups;
                pckSantaGroups.ItemDisplayBinding = new Binding("GroupName");
            }
            catch (Exception ex)
            {

            }
        }

        private void Test()
        {
            string[] strings = new string[5];

            for(int i = 0; i < 5; i++)
            {
                strings[i] = Console.ReadLine();
            }

            Array.Sort(strings);

            for(int i = 0; i < 5; i++)
            {
                Console.WriteLine(strings);
            }
        }

        public async void btnSubmit_Clicked(object sender, EventArgs args)
        {
            if (edtSantas.Text == "No Santas added yet...")
            {
                await DisplayAlert("Info", "All fields are required.", "OK");
                return;
            }

            ResetAllocations();

            Random rand = new Random();
            int iNewRand = 10;
            int santaNum = 0;

            for (int i = 0; i < santas.Count; ++i)
            {
                while (bNewRandom == false || bCouple == true || bSelf == true)
                {
                    iNewRand = GenerateRandom(rand);
                    CheckNumber(santas, iNewRand);
                    //CheckCouple(i + 1, iNewRand);
                    CheckSelf(i + 1, iNewRand);

                    if (i == santas.Count - 1 && bSelf)
                    {
                        i = 0;
                        ResetAllocations();
                    }
                }

                santaNum = iNewRand;
                santas[i].setSantaOf(santaNum);
                bNewRandom = false;
                bCouple = false;
                bSelf = true;
            }

            try
            {
                for (int i = 0; i < santas.Count; ++i)
                {
                    SendEmail(santas[i], santas);
                }
            }
            catch (Exception ex)
            {
                errorFlag = true;
            }

            if (!errorFlag)
            {
                await DisplayAlert("Success!", "All Secret Santas have successfully been assigned.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Could not assign all Secret Santas.", "OK");
            }
        }

        private void ResetAllocations()
        {
            for (int i = 0; i < santas.Count; i++)
            {
                santas[i].setSantaOf(-1);
            }
        }

        private int GenerateRandom(Random aRand)
        {
            int num = 0;
            num = aRand.Next(1, santas.Count + 1);
            return num;
        }

        private void CheckNumber(List<Santa> aSantas, int num)
        {
            for (int i = 0; i < santas.Count; ++i)
            {
                if (num == aSantas[i].getSantaOf())
                {
                    bNewRandom = false;
                    break;
                }
                else
                {
                    bNewRandom = true;
                }
            }
        }

        private void CheckSelf(int aSantaNum, int num)
        {
            if (aSantaNum == num)
            {
                bSelf = true;
            }
            else
            {
                bSelf = false;
            }
        }

        private void CheckCouple(int aSantaNum, int num)
        {
            if (aSantaNum == 2 && num == 3)
            {
                bCouple = true;
            }
            else if (aSantaNum == 3 && num == 2)
            {
                bCouple = true;
            }
            else if (aSantaNum == 7 && num == 8)
            {
                bCouple = true;
            }
            else if (aSantaNum == 8 && num == 7)
            {
                bCouple = true;
            }
            else
            {
                bCouple = false;
            }
        }

        private void SendEmail(Santa aSanta, List<Santa> aSantas)
        {
            string santaOf = "";
            string sTo = aSanta.getEmail();
            string sSubject = "Secret Santa";
            string sBody = string.Empty;

            for (int i = 0; i < santas.Count; ++i)
            {
                if (aSanta.getSantaOf() == aSantas[i].getNum())
                {
                    santaOf = aSantas[i].getName() + " " + aSantas[i].getSurname();
                }
            }

            if(radDefaultMesg.IsChecked)
            {
                sBody = "Hi " + aSanta.getName() + ",\n\nYou are the Secret Santa of "
                + santaOf + " this year!\n\nThe price limit for the gift is R" + GetPriceLimit()
                + ", no need to go overboard." + "\n\nBest wishes,\nDigital Santa";
            }
            else
            {
                sBody = string.Format(txtMessage.Text, aSanta.getName(), aSanta.getSurname(), santaOf, GetPriceLimit());
            }

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("xsantaxxxclausx@gmail.com", "pdawaftxcxbvvnfn"),
                    EnableSsl = true,
                };

                smtpClient.Send("xsantaxxxclausx@gmail.com", sTo, sSubject, sBody);
            }
            catch (Exception ex)
            {
                errorFlag = true;
            }
        }

        private decimal GetPriceLimit()
        {
            string originalText = txtMaxGiftPrice.Text.Trim();
            decimal convertedText = 0;

            originalText = originalText.Replace("R", string.Empty);
            originalText = originalText.Replace(" ", string.Empty);

            try
            {
                convertedText = Convert.ToDecimal(originalText);
            }
            catch(Exception ex) 
            {
                
            }  

            return convertedText;
        }

        private void btnManageSantas_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateSanta());
        }

        private void btnManageSantaGroups_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CreateGroup());
            ClearFormData();
            LoadSantaGroups();
        }

        private void pckSantaGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                edtSantas.Text = string.Empty;

                if (pckSantaGroups.SelectedIndex >= 0)
                {
                    string dispString = string.Empty;

                    santas.AddRange(santaGroups[pckSantaGroups.SelectedIndex].Santas);

                    for (int i = 0; i < santas.Count; i++)
                    {
                        santas[i].setNum(i + 1);
                    }

                    for (int i = 0; i < santas.Count; i++)
                    {
                        dispString += santas[i].getNum() + ". " + santas[i].getName() + " " + santas[i].getSurname() + " : " + santas[i].getEmail() + "\n";
                    }

                    edtSantas.Text = dispString;
                }
            }
            catch (Exception ex)
            {

            }            
        }

        private void ClearFormData()
        {
            pckSantaGroups.SelectedIndex = -1;
            edtSantas.Text = string.Empty;
            txtMaxGiftPrice.Text = string.Empty;            
        }

        private void radDefaultMesg_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(radDefaultMesg.IsChecked)
            {
                txtMessage.IsEnabled = false;
                txtMessage.IsVisible = false;
            }
            else
            {
                txtMessage.IsEnabled = true;
                txtMessage.IsVisible = true;
            }
        }
    }
}
