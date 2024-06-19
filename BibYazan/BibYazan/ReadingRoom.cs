using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibYazan
{
     public abstract class ReadingRoom
    {
		private string title;
		public string Title
		{
			get { return title; }
		}
		private string publisher;
		public string Publisher
		{
			get { return publisher; }
			set { publisher = value; }
		}
        abstract public string Identification { get; }
		abstract public string Categorie { get; }

		public ReadingRoom (string title, string publisher)
		{
			this.title = title;
            this.Publisher = publisher;
        }
    }
}
