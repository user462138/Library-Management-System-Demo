using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibYazan
{
    public class NewsPaper : ReadingRoom
    {
		private static DateTime date;

		public static DateTime Date
		{
			get { return date; }
			set { date = value; }
		}
		public override string Identification
        {
			get 
			{
				return strToId(Title,Date);
			}
		}
        public override string Categorie 
        {
            get { return "Krant"; }
        }
        public NewsPaper(string title, string publisher, DateTime date) : base(title, publisher)
		{
			Date = date;
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
