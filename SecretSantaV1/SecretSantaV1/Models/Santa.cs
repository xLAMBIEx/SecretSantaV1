using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SecretSantaV1.Models
{
    [Serializable]
    public class Santa
    {
        [DataMember]
        private string Name;
        [DataMember]
        private string Surname;
        [DataMember]
        private int Num;
        [DataMember]
        private int SantaOf;
        [DataMember]
        private string Email;
        [DataMember]
        private string MobileNumber;

        public Santa()
        {
            Name = "";
            Num = 0;
            SantaOf = 0;
            Email = "";
        }

        public Santa(string name, int num, string email)
        {
            Name = name;
            Num = num;
            Email = email;
            SantaOf = 0;
        }

        public void setSantaOf(int santaOf)
        {
            SantaOf = santaOf;
        }

        public int getSantaOf()
        {
            return SantaOf;
        }

        public string getName()
        {
            return Name;
        }

        public void setName(string name)
        {
            Name = name;
        }

        public string getSurname()
        {
            return Surname;
        }

        public void setSurname(string surname)
        {
            Surname = surname;
        }

        public int getNum()
        {
            return Num;
        }

        public void setNum(int num)
        {
            Num = num;
        }

        public string getEmail()
        {
            return Email;
        }

        public void setEmail(string email)
        {
            Email = email;
        }

        public void setMobileNumber(string number)
        {
            MobileNumber = number;
        }

        public string getMobileNumber()
        {
            return MobileNumber;
        }

        public string DisplayName
        {
            get { return Name + " " + Surname; }
        }
    }
}
