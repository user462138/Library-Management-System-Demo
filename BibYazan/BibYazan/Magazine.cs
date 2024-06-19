using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibYazan
{
    public class Magazine : ReadingRoom
    {
		private byte month;

		public byte Month
		{
			get { return month; }
            private set
            {
				if (value >12)
				{
                    Console.WriteLine("De maand is maximaal 12");
                }
                else
                {
                    month = value;
                }
            }
		}

		private uint year;

		public uint Year
		{
			get { return year; }
			set 
			{
                if (value > 2500)
                {
                    Console.WriteLine("Het jaartal is maximaal 2500");
                }
                else
                {
                    year = value;
                }
            }
		}
        public override string Identification
        {
            get
            {
                DateTime dateMagazine = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), 01);
                return strToId(Title, dateMagazine);
            }
        }
        public override string Categorie
        {
            get { return "Maandblad"; }
        }
        public Magazine(string title, string publisher, uint year, byte month) : base(title, publisher)
        {
            Year = year;
            Month = month;
        }

        /* ******************************************** */
        /* converting string to ID for "Identification" */
        /* ******************************************** */

        public string strToId(string str, DateTime date)
        {
            string id = "";
            string[] data = str.Split();
            if (data.Length > 1)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    id += data[i].Substring(0, 1).ToUpper();
                }
            }
            else
            {
                id += str.ToUpper();
            }
            if (date == NewsPaper.Date)
            {
                id += date.ToString("dd");
            }
            id += date.ToString("MM");
            id += date.Year;
            return id;
        }
    }
}