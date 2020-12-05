using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HomeInspectorSchedule
{
    public static class Validate
    {
        public static bool Email_Validate(string email)
        {
            var emailLength = email.Length;
            var atIndex = email.IndexOf("@");
            var dotIndex = email.LastIndexOf(".");

            var domain = email.Substring(dotIndex + 1);
            var domainValidate = Domain_Check(domain);

            if (atIndex == -1 || dotIndex == -1 || dotIndex < atIndex || (dotIndex + 3) >= emailLength
                || atIndex < 3 || (atIndex + 5) >= emailLength || !((dotIndex - atIndex) > 3) || domainValidate == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Phone_Validate(string phone)
        {
            phone = phone.Trim();
            phone = phone.Replace("(", "");
            phone = phone.Replace(")", "");
            phone = phone.Replace("-", "");

            //bool result = phone.Any(x => char.IsLetter(x));
            bool result = phone.Any(x => !char.IsNumber(x));
            if ((phone.Length != 10 && phone.Length != 7) || result)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Domain_Check(string domain)
        {
            if (domain != "com" && domain != "edu" && domain != "gov" && domain != "net" && domain != "org")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string Phone_Syntax(string phone)
        {

            phone = phone.Trim();
            phone = phone.Replace("(", "");
            phone = phone.Replace(")", "");
            phone = phone.Replace("-", "");

            var length = phone.Length;

            if (length == 10)
            {
                phone = phone.Insert(6, "-");
                phone = phone.Insert(3, ")");
                phone = phone.Insert(0, "(");
            }
            else if (length == 7)
            {
                phone = phone.Insert(3, "-");
            }

            return phone;
        }

        public static bool Zip_Syntax(string zip)
        {
            var length = zip.Length;

            if(length == 5)
            {
                bool result = zip.Any(x => char.IsLetter(x));
                if (result)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public static string Capitalize_Name(string input)
        {           
            var lastTwoChar = input.Substring(input.Length - 2, 2);
            var checkSpace = lastTwoChar.Substring(0, 1);

            var allButLast = input.Substring(0, input.Length - 1);
            var justLast = input.Substring(input.Length - 1, 1);

            var newText = allButLast + justLast.ToUpper();

            if (checkSpace == " ")
            {
                return newText;
            }
            else
            {
                return input;
            }
            
        }
        public static bool BackSpace(string search, string lastSearch)
        {
            bool backspace = false;
            if (lastSearch != null && lastSearch != "")
            {
                int length = lastSearch.Length;
                string compare = lastSearch.Substring(0, length - 1);
                if (search == compare)
                {
                    backspace = true;
                }
            }
            return backspace;
        }

        static public string PhoneInput(Entry entry, string lastText)
        {
            const int charMax = 13;
            string text = entry.Text;

            if (text.Length > 8 && text.Length < charMax)
            {
                char check1 = text[0];
                if (check1 != '(')
                {
                    if(!text.Contains("("))
                        text = text.Insert(0, "(");
                }
                char check2 = text[4];
                if (check2 != ')')
                {
                    if(!text.Contains(")"))
                        text = text.Insert(4, ")");
                }
                char check3 = text[8];
                if (check3 != '-')
                {
                    if (!text.Contains("-"))
                        text = text.Insert(8, "-");
                }
            }

            else if (text.Length > charMax)
            {
                text = lastText;
            }

            entry.Text = text;
            lastText = text;
            return lastText;
        }
    }
}
