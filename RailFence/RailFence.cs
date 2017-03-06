using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RailFence
{
    public partial class RailFence : Form
    {
        public bool wasAppended { get; set; } //global holder of information that was appended
        public RailFence()
        {
            InitializeComponent();
            wasAppended = false; //no append by default
        }

        public static string GetCipher(string original, bool wasAppended)
        {            
            if (original.Length == 0) //protection
                return null;

            if (original.Length % 2 != 0) //checking append
            {
                wasAppended = true;
                original += "Q"; //hardcored append letter
            }

            string firstGroup = ""; //for splitted parts
            string secondGroup = "";

            for (int i = 0; i < original.Length; i++) //go throught string
            {
                if (i % 2 == 0) //splitting to two groups
                {
                    firstGroup += original[i];
                }
                else
                {
                    secondGroup += original[i];
                }
            }

            return(String.Format("{0}{1}", firstGroup, secondGroup)); //joining strings
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = GetCipher(textBox1.Text, wasAppended); 
            textBox1.Text = "";
        }

        public static string GetDeCipher(string cipher, bool wasAppended)
        {
            if (cipher.Length == 0) //protection
                return null;

            int splitPoint = (int)(cipher.Length / 2); //to found place where split original string

            string firstGroup = ""; //for stroring splitted strings
            string secondGroup = "";

            string decipher = "";

            for (int i = 0; i < splitPoint; i++) //splitting first part from original string
            {
                firstGroup += cipher[i];
            }

            for (int i = splitPoint; i < cipher.Length; i++) //get second half of string
            {
                secondGroup += cipher[i];
            }

            int firstcounter = 0; //to separate counting in substrings
            int secondcounter = 0;

            for (int i = 0; i < cipher.Length; i++)
            {
                if (i == (cipher.Length - 1) && wasAppended == true)
                {
                    break; //ballast append is lost
                }

                if (i % 2 == 0) //from which string take letter
                {
                    decipher += firstGroup[firstcounter++]; //taking letter
                }
                else
                {
                    decipher += secondGroup[secondcounter++];
                }                
            }

            return decipher;
        }
        private void button2_Click(object sender, EventArgs e)
        {         
            textBox1.Text = GetDeCipher(textBox2.Text, wasAppended);
            textBox2.Text = "";
        }
    }
}
;