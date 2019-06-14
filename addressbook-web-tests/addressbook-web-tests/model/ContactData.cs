using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")]
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allMails;
        private string allInfo;

        public ContactData(string firstname)
        {
            Firstname = firstname;
        }
        public ContactData()
        {
        }
        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname
            && Lastname == other.Lastname;
        }
        public override int GetHashCode()
        {
            return Firstname.GetHashCode() ^ Lastname.GetHashCode();
        }
        public override string ToString()
        {
            return "firstname =" + Firstname + " lastname =" + Lastname;
        }
        public int CompareTo(ContactData other)
        {
            var str1 = string.Concat(other.Firstname, other.Lastname);
            var str = string.Concat(Firstname, Lastname);
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return str.CompareTo(str1);
        }
        [Column(Name = "firstname")]
        public string Firstname { get; set; }
        [Column(Name = "lastname")]
        public string Lastname { get; set; }
        [Column(Name = "middlename")]
        public string Middlename { get; set; }
        [Column(Name = "nickname")]
        public string Nickname { get; set; }
        [Column(Name = "title")]
        public string Title { get; set; }
        [Column(Name = "company")]
        public string Company { get; set; }
        [Column(Name = "address")]
        public string Address { get; set; }
        [Column(Name = "home")]
        public string HomePhone { get; set; }
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }
        [Column(Name = "work")]
        public string WorkPhone { get; set; }
        [Column(Name = "fax")]
        public string Fax { get; set; }
        [Column(Name = "email")]
        public string Email { get; set; }
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        [Column(Name = "email3")]
        public string Email3 { get; set; }
        [Column(Name = "homepage")]
        public string Homepage { get; set; }
        [Column(Name = "ayear")]
        public string Ayear { get; set; }
        [Column(Name = "byear")]
        public string Byear { get; set; }
        [Column(Name = "address2")]
        public string Address2 { get; set; }
        [Column(Name = "phone2")]
        public string Phone2 { get; set; }
        [Column(Name = "notes")]
        public string Notes { get; set; }
        [Column(Name = "bday")]
        public string Bday { get; set; }
        [Column(Name = "aday")]
        public string Aday { get; set; }
        [Column(Name = "amonth")]
        public string Amonth { get; set; }
        [Column(Name = "bmonth")]
        public string Bmonth { get; set; }
        [Column(Name = "id"), PrimaryKey] //Identity?
        public string Id { get; set; }
        [Column(Name = "deprecated")]
        public string Deprecated { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[ -()]", "") + "\r\n";
        }

        public string AllMails
        {
            get
            {
                if (allMails != null)
                {
                    return allMails;
                }
                else
                {
                    return CorrectBreakMail(Email) + CorrectBreakMail(Email2) + CorrectBreakMail(Email3).Trim();
                }
            }
            set
            {
                allMails = value;
            }
        }
        // Корректный перенос строки с емейлом
        private string CorrectBreakMail(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                return str + "\r\n";
            }
        }

        public string AllInfoFromDetails
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                else
                {
                    string resultAllText = "";

                    string FIO = "";

                    if (!string.IsNullOrEmpty(Firstname))
                    {
                        FIO += Firstname;
                    }
                    if (!string.IsNullOrEmpty(Lastname))
                    {
                        FIO += Lastname;
                    }
                    if (!string.IsNullOrEmpty(FIO))
                    {
                        resultAllText += FIO + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(Address))
                    {
                        resultAllText += Address + "\r\n";
                    }
                    resultAllText += "\r\n";
                    if (!string.IsNullOrEmpty(HomePhone))
                    {
                        resultAllText += "H: " + HomePhone + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(MobilePhone))
                    {
                        resultAllText += "M: " + MobilePhone + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(WorkPhone))
                    {
                        resultAllText += "W: " + WorkPhone + "\r\n";
                    }
                    resultAllText += "\r\n";
                    if (!string.IsNullOrEmpty(Email))
                    {
                        resultAllText += Email + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(Email2))
                    {
                        resultAllText += Email2 + "\r\n";
                    }
                    if (!string.IsNullOrEmpty(Email3))
                    {
                        resultAllText += Email3 + "\r\n";
                    }
                    resultAllText += "\r\n";

                    return resultAllText.Trim('\r', '\n');
                }
            }
            set
            {
                allInfo = value;
            }
        }
        public static List<ContactData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") select c).ToList();
            }
        }
    }
}