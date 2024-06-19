using SchoolAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibYazan
{
    internal class Book : ILendable
    {
        public string Title;
        public string Author;
        public DateTime ReleaseDate;

        private List<Genre> genres = new List<Genre>();
        public List<Genre> Genres
        {
            get { return genres; }
            set { genres = value; }
        }

        //public bool IsBorrowed;

        private string numberISBN;
        public string NumberISBN
        {
            get { return numberISBN; }
            private set 
            {
                if (value.Length < 13 || value.Length > 13 || !ControleNumberISBN(value))
                {
                    throw new FormatException($"The requested ISBN {value} number is invalid");
                }
                else
                {
                    numberISBN = value;
                }
            }
        }

        private bool isAvailable;
        public bool IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; }
        }

        private DateTime borrowingDate;
        public DateTime BorrowingDate
        {
            get { return borrowingDate; }
            set { borrowingDate = value; }
        }

        private int borrowDays;
        public int BorrowDays
        {
            get
            {
                if (this.genres.Contains(Genre.Schoolboek) && Genres.Count == 1)
                {
                    borrowDays = 20;
                    return borrowDays;
                }
                else
                {
                    borrowDays = 10;
                    return borrowDays;
                }
            }
            set { borrowDays = value; }
        }

        public Book (string title, string author, DateTime releaseDate, string numberISBN, List<Genre> genres, bool isAvailable)
        {
            this.Title = title;
            this.Author = author;
            this.ReleaseDate = releaseDate;
            this.NumberISBN = numberISBN;
            this.Genres = genres;
            this.IsAvailable = isAvailable;
        }

        public void Borrow()
        {
            Console.WriteLine();
            Console.WriteLine("Give the start Borrowing Date.  (2023-12-12)");
            BorrowingDate = Convert.ToDateTime(Console.ReadLine());
            DateTime startBorrowDate = BorrowingDate;
            this.isAvailable = false;
            DateTime endBorrowDate = startBorrowDate.AddDays(this.BorrowDays);
            Console.WriteLine();
            Console.WriteLine(
                $"The maximum borrowing days for this book is {this.BorrowDays}." +
                $"\r\nReturn the book to the library before {endBorrowDate}." +
                $"\r\nTo avoid any financial fines");
            Console.WriteLine();
            Console.ReadKey();
        }
        public void Return()
        {
            this.isAvailable = true;
            DateTime endBorrowDate = DateTime.Now;
            TimeSpan totalBorrowedDayDate = endBorrowDate - BorrowingDate;
            int totalBorrowedDay = totalBorrowedDayDate.Days;
            if (totalBorrowedDay > BorrowDays)
            {
                Console.WriteLine();
                Console.WriteLine($"You have exceeded the permitted borrowing period" +
                    $"\r\nYou will pay 0.40$ For every day of delay." +
                    $"\r\nYour total is : {totalBorrowedDay- BorrowDays} * 0.40$ = {Convert.ToDouble(totalBorrowedDay - BorrowDays)*0.40}$ ");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"You have returned your book in during the permitted borrowing perid" +
                    $"" +
                    $"\r\nGoodJob! (: ");
                Console.ReadKey();
            }
        }
        public static List<Book> ReadCsvFile()
        {
            List<Book> booksINCSVFile = new List<Book>();
            string[] lines = File.ReadAllLines
                (@"C:\Users\yazan\OneDrive\Desktop\SCHOOL\AP Hogeschool\academiejaar 2023-24\OO Programmeren\2.project\BibYazanDeel3_3\Books.txt");
            for (int i = 0; i < lines.Length; i++)
            {
                string data = lines[i];
                string[] gesplitst = data.Split(';');

                string titleBook = gesplitst[0];
                string authorBook = gesplitst[1];

                string ReleaseDate = gesplitst[2];
                string[] ReleaseDateGesplitst = ReleaseDate.Split('-');
                int year = Convert.ToInt32(ReleaseDateGesplitst[0]);
                int month = Convert.ToInt32(ReleaseDateGesplitst[1]);
                int day = Convert.ToInt32(ReleaseDateGesplitst[2]);
                DateTime releaseDateBook = new DateTime(year, month, day);

                string numberISBNBook = gesplitst[3];

                string genre = gesplitst[4];
                string[] genreDateGesplitst = genre.Split(",");
                List<Genre> genres = new List<Genre>();
                for (int index = 0; index < genreDateGesplitst.Length; index++)
                {
                    switch (genreDateGesplitst[index])
                    {
                        case "Fiction":
                            genres.Add(Genre.Fiction);
                            break;
                        case "NonFiction":
                            genres.Add(Genre.NonFiction);
                            break;
                        case "Mystery":
                            genres.Add(Genre.Mystery);
                            break;
                        case "ScienceFiction":
                            genres.Add(Genre.ScienceFiction);
                            break;
                        case "Fantasy":
                            genres.Add(Genre.Fantasy);
                            break;
                        case "Romance":
                            genres.Add(Genre.Romance);
                            break;
                        case "Thriller":
                            genres.Add(Genre.Thriller);
                            break;
                        case "Horror":
                            genres.Add(Genre.Horror);
                            break;
                        case "Adventure":
                            genres.Add(Genre.Adventure);
                            break;
                        case "Dystopian":
                            genres.Add(Genre.Dystopian);
                            break;
                        case "History":
                            genres.Add(Genre.History);
                            break;
                        case "Psychology":
                            genres.Add(Genre.Psychology);
                            break;
                        case "SelfHelp":
                            genres.Add(Genre.SelfHelp);
                            break;
                        case "Schoolboek":
                            genres.Add(Genre.Schoolboek);
                            break;
                        case "Other":
                            genres.Add(Genre.Other);
                            break;
                        default:
                            break;
                    }
                }
                bool isAvailable = Convert.ToBoolean(gesplitst[5]);
                Book newBook = new Book(titleBook, authorBook, releaseDateBook, numberISBNBook, genres, isAvailable);
                booksINCSVFile.Add(newBook);
            }
            string str = "Books loaded successfully from CSV.";
            ReturnStrInBox(str);
            return booksINCSVFile;
        }
        public bool ControleNumberISBN(string numberISBN)
        {
            int oneven = Convert.ToInt32(numberISBN.Substring(0, 1)) + Convert.ToInt32(numberISBN.Substring(2, 1)) +
                Convert.ToInt32(numberISBN.Substring(4, 1)) + Convert.ToInt32(numberISBN.Substring(6, 1)) +
                Convert.ToInt32(numberISBN.Substring(8, 1)) + Convert.ToInt32(numberISBN.Substring(10, 1));
            int even = Convert.ToInt32(numberISBN.Substring(1, 1)) + Convert.ToInt32(numberISBN.Substring(3, 1)) +
            Convert.ToInt32(numberISBN.Substring(5, 1)) + Convert.ToInt32(numberISBN.Substring(7, 1)) +
            Convert.ToInt32(numberISBN.Substring(9, 1)) + Convert.ToInt32(numberISBN.Substring(11, 1));
            int controle = (10 - (oneven + 3 * even) % 10) % 10;
            if (controle == Convert.ToInt32(numberISBN.Substring(12,1)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void ReturnStrInBox(string str)
        {
            string newStr = "| " + str + " |";
            string repeatChar = new String('*', newStr.Length);
            string repeatCharEmpty = new String(' ', newStr.Length - 2);
            Console.WriteLine("\n" + repeatChar);
            Console.WriteLine("|" + repeatCharEmpty + "|");
            Console.WriteLine(newStr);
            Console.WriteLine("|" + repeatCharEmpty + "|");
            Console.WriteLine(repeatChar + "\n");
        }
    }
}