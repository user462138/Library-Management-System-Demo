using SchoolAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BibYazan
{
    internal class Library
    {
        private List<Book> allBooks = new List<Book>();
        public List<Book> AllBooks
        {
            get { return allBooks; }
            set { allBooks = value; }
        }
        private Dictionary<DateTime,ReadingRoom> allReadingRoom = new Dictionary<DateTime, ReadingRoom>();
        public Dictionary<DateTime,ReadingRoom> AllReadingRoom
        {
            get { return allReadingRoom; }
        }
        public Library(List<Book> books)
        {
            foreach (var book in books)
            {
                AllBooks.Add(book);
            }
        }
        public Book SearchBookByTitle(string title)
        {
            foreach (Book book in AllBooks)
            {
                if (book.Title == title)
                {
                    return book;
                }
            }
            return null;
        }
        public Book SearchBookByAuthor(string author)
        {
            foreach (Book book in AllBooks)
            {
                if (book.Author == author)
                {
                    return book;
                }
            }
            return null;
        }
        public Book SearchBookByNumberISBN(string numberISBN)
        {
            foreach (Book book in AllBooks)
            {
                if (book.NumberISBN == numberISBN)
                {
                    return book;
                }
            }
            return null;
        }
        public void GetAllBorrowedBook()
        {
            Console.WriteLine("\n***************************Borrowed-Book-List***************************\n");
            foreach (var book in AllBooks)
            {
                if (book.IsAvailable == false)
                {
                    //Console.WriteLine($"Title =\t {book.Title}");
                    Console.WriteLine(string.Format("{0,-20}  {1,50}", "Title =", book.Title));
                }
            }
            Console.WriteLine("\n***************************Borrowed-Book-List***************************\n");
        }
        public void GetAllAvailableBook()
        {
            Console.WriteLine("\n**************************Available-Book-List***************************\n");
            foreach (var book in AllBooks)
            {
                if (book.IsAvailable == true)
                {
                    //Console.WriteLine($"Title =\t {book.Title}");
                    Console.WriteLine(string.Format("{0,-20}  {1,50}", "Title =", book.Title));
                }
            }
            Console.WriteLine("\n**************************Available-Book-List***************************\n");
        }
        public void GetAllBook()
        {
            Console.WriteLine("\n*******************************All-Books-List***************************\n");
            foreach (var book in AllBooks)
            {
                Console.WriteLine(string.Format("{0,-20}  {1,50}", "Title =", book.Title));
            }
            Console.WriteLine("\n*******************************All-Books-List***************************\n");
        }
        public void DeleteBook()
        {
            Console.WriteLine("Type book *Title* ");
            string bookTitle = Console.ReadLine();
            Book DisplayBook = SearchBookByTitle(bookTitle);
            if (DisplayBook != null)
            {
                AllBooks.Remove(SearchBookByTitle(bookTitle));
            }
            else
            {
                string str = $"Book title:{bookTitle} not found";
                ReturnStrInBox(str);
            }
        }
        public void ChangeBookInfo()
        {
            Console.WriteLine("Type BOOK ISBN Number");
            string numberISBN = Console.ReadLine();
            Book DisplayBook = SearchBookByNumberISBN(numberISBN);
            if (DisplayBook != null)
            {
                Console.WriteLine("what would you like to change (type a number)\n");
                Console.WriteLine("1. Book Title");
                Console.WriteLine("2. Book Author");


                int keuze = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n****************************");
                switch (keuze)
                {
                    case 1:
                        Console.Write($"Title = ");
                        DisplayBook.Title = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write($"Author = ");
                        DisplayBook.Title = Console.ReadLine();
                        break;
                    default:
                        break;
                }
                Console.WriteLine("****************************\n");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        public void ReturnStrInBox(string str)
        {
            string newStr = "| " + str + " |";
            string repeatChar = new String('*', newStr.Length);
            string repeatCharEmpty = new String(' ', newStr.Length-2);
            Console.WriteLine("\n" + repeatChar);
            Console.WriteLine("|" + repeatCharEmpty + "|");
            Console.WriteLine(newStr);
            Console.WriteLine("|" + repeatCharEmpty + "|");
            Console.WriteLine(repeatChar + "\n");
        }
        public void AddNewspaper()
        {
            Console.WriteLine("Wat is de naam van de krant?");
            string krantNaam = Console.ReadLine();
            Console.WriteLine("Wat is de datum van de krant?");
            DateTime krantDatum = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Wat is de Uitgeverij van de krant?");
            string krantUitgeverij = Console.ReadLine();
            NewsPaper newKrant = new NewsPaper(krantNaam, krantUitgeverij, krantDatum);
            this.AllReadingRoom.Add(krantDatum, newKrant);
        }
        public void AddMagazine()
        {
            Console.WriteLine("Wat is de naam van de maandblad?");
            string maandbladNaam = Console.ReadLine();
            Console.WriteLine("Wat is de maand van de maandblad?");
            byte maandbladmaand = Convert.ToByte(Console.ReadLine());
            Console.WriteLine("Wat is de jaar van de maandblad?");
            uint maandbladjaar = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine("Wat is de Uitgeverij van de krant?");
            string maandbladUitgeverij = Console.ReadLine();
            Magazine newMaandblad = new Magazine(maandbladNaam, maandbladUitgeverij, maandbladjaar, maandbladmaand);
            AllReadingRoom.Add(DateTime.Now, newMaandblad);
        }
        public void ShowAllMagazines()
        {
            Console.WriteLine("\nAlle maandbladen uit de leeszaal:\n");
            foreach (var item in AllReadingRoom)
            {
                if (item.Value.GetType().Name == "Magazine")
                {
                    Console.WriteLine($"- {item.Value.Title} van {item.Key.Month}/{item.Key.Year} van uitgeverrij {item.Value.Publisher}");
                }
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        public void ShowAllNewspapers()
        {
            Console.WriteLine("\n Alle kranten in de leeszaal: \n");
            foreach (var item in AllReadingRoom)
            {
                if (item.Value.GetType().Name == "NewsPaper")
                {
                    Console.WriteLine($"- {item.Value.Title} van {item.Key.ToString("dddd")} {item.Key.Day} {item.Key.ToString("MMMM")} {item.Key.Year} van uitgeverrij {item.Value.Publisher}");
                }
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        public void AcquisitionsReadingRoomToday()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"Aanwinsten in de leeszaal van {now.ToString("dddd")} {now.Day} {now.ToString("MMMM")} {now.Year}");
            foreach (var item in allReadingRoom)
            {
                Console.WriteLine($"{item.Value.Title} met id {item.Value.Identification}");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        public void AddBook()
        {
            try
            {
                Console.Write("Title = ");
                string title = Console.ReadLine();
                Console.Write("Author = ");
                string author = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    throw new ArgumentNullException($"Empty or invalid input. Please enter a valid value.");
                }
                Console.WriteLine("ReleaseDate : ");
                Console.Write("ReleaseDate (YEAR) = ");
                int year = Convert.ToInt32(Console.ReadLine());
                Console.Write("ReleaseDate (MONTH) = ");
                int month = Convert.ToInt32(Console.ReadLine());
                Console.Write("ReleaseDate (DAY) = ");
                int day = Convert.ToInt32(Console.ReadLine());
                DateTime releaseDateBook = new DateTime(year, month, day);
                Console.Write("ISBN-Numbers = ");
                string numberISBNBook = Console.ReadLine();
                Console.WriteLine("Genre : ");
                List<Genre> genres = new List<Genre>();
                int teller = 0;
                foreach (var name in Enum.GetNames<Genre>())
                {
                    Console.WriteLine($"{teller++}- {name}");
                }
                string genre = Console.ReadLine();
                switch (genre)
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
                Console.WriteLine("Would you like to add another Genre? (yes/no)");
                string anotherGenre = Console.ReadLine();
                while (anotherGenre.ToLower() == "yes")
                {
                    Console.WriteLine("choose a genre from the list above.");
                    genre = Console.ReadLine();
                    switch (genre)
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
                    Console.WriteLine("Would you like to add another Genre? (yes/no)");
                    anotherGenre = Console.ReadLine();
                }
                bool isAvailable = Convert.ToBoolean(true);

                Book newBook = new Book(title, author, releaseDateBook, numberISBNBook, genres, isAvailable);
                foreach (var book in allBooks)
                {
                    if (book.NumberISBN == newBook.NumberISBN)
                    {
                        throw new DuplicateDataException($"\nThis ISBN-number is belong to another book. please check it again.", book, newBook);
                    }
                }
                this.AllBooks.Add(newBook);
            }
            catch (ArgumentNullException ae)
            {
                ReturnStrInBox(ae.Message);
            }
            catch (FormatException fe)
            {
                ReturnStrInBox(fe.Message);
            }
            catch (DuplicateDataException dde)
            {
                Console.WriteLine(dde.Message);
                Console.WriteLine("below you will find all info about this ISBN-number");
                GetBookInfo(((Book)dde.Object1));
            }
        }
        public void GetBookInfoShort()
        {
            Console.WriteLine("Type BOOK Title");
            string BookTitle = Console.ReadLine();
            Book book = SearchBookByTitle(BookTitle);
            if (book != null)
            {
                Dictionary<string, string> bookInfo = new Dictionary<string, string>() { };
                bookInfo.Add("Title", book.Title);
                bookInfo.Add("Author", book.Author);
                bookInfo.Add("ReleaseDate", Convert.ToString(book.ReleaseDate.Year));
                bookInfo.Add("Book ISBN number", Convert.ToString(book.NumberISBN));

                if (book.IsAvailable)
                {
                    bookInfo.Add("Book Availability", "Available");
                }
                else
                {
                    bookInfo.Add("Book Availability", "Not Available");
                }
                Console.WriteLine("\n****************************************************");
                foreach (var item in bookInfo)
                {
                    Console.WriteLine(string.Format("{0,-25}  {1,25}", item.Key, item.Value));
                }
                Console.WriteLine("****************************************************\n");
                Console.ReadKey();
            }
            else
            {
                string str = $"Book title:{BookTitle} not found.";
                ReturnStrInBox(str);
                Console.ReadKey();
            }
        }
        public void GetBookInfo(Book bookInput)
        {
            foreach (Book book in AllBooks)
            {
                if (book == bookInput)
                {
                    Dictionary<string, string> bookInfo = new Dictionary<string, string>() { };
                    bookInfo.Add("Title", book.Title);
                    bookInfo.Add("Author", book.Author);
                    bookInfo.Add("ReleaseDate", Convert.ToString(book.ReleaseDate.Year));
                    bookInfo.Add("Book ISBN number", Convert.ToString(book.NumberISBN));

                    if (book.IsAvailable)
                    {
                        bookInfo.Add("Book Availability", "Available");
                    }
                    else
                    {
                        bookInfo.Add("Book Availability", "Not Available");
                    }
                    Console.WriteLine("\n****************************************************");
                    foreach (var item in bookInfo)
                    {
                        Console.WriteLine(string.Format("{0,-25}  {1,25}", item.Key, item.Value));
                    }
                    Console.WriteLine("****************************************************\n");
                    Console.ReadKey();
                }
                if (!(AllBooks.Contains(book)))
                {
                    string str = $"Not found, try again...";
                    ReturnStrInBox(str);
                    Console.ReadKey();
                }
            }
        }
        public void SearchBookBy()
        {
            Console.WriteLine("Search BOOK by: (type a number)");
            Console.WriteLine("1. ISBN-nummer");
            Console.WriteLine("2. Book Auther");

            int keuze = Convert.ToInt32(Console.ReadLine());
            switch (keuze)
            {
                case 1:
                    Console.WriteLine("Type BOOK ISBN-nummer");
                    string bookNumberISBN = Console.ReadLine();
                    Book DisplayBook = SearchBookByNumberISBN(bookNumberISBN);
                    if (DisplayBook != null)
                    {
                        GetBookInfo(DisplayBook);
                    }
                    else
                    {
                        string str1 = $"BOOK ISBN-Nummer not found.";
                        ReturnStrInBox(str1);
                        Console.ReadKey();
                    }
                    break;
                case 2:
                    Console.WriteLine("Type BOOK Auther");
                    string bookAuther = Console.ReadLine();
                    Book DisplayBook2 = SearchBookByAuthor(bookAuther);
                    if (DisplayBook2 != null)
                    {
                        GetBookInfo(SearchBookByAuthor(bookAuther));
                    }
                    else
                    {
                        string str2 = $"BOOK Auther not found.";
                        ReturnStrInBox(str2);
                        Console.ReadKey();
                    }
                    break;
                default:
                    string str = $"wrong input";
                    ReturnStrInBox(str);
                    Console.ReadKey();
                    break;
            }
        }
        public void ShowBooks()
        {
            Console.WriteLine("Show BOOK : (type a number)");
            Console.WriteLine("1. All Books in the Library");
            Console.WriteLine("2. All Borrowed Books in the Library");
            Console.WriteLine("3. All Available Books in the Library");

            int keuze = Convert.ToInt32(Console.ReadLine());
            switch (keuze)
            {
                case 1:
                    GetAllBook();
                    break;
                case 2:
                    GetAllBorrowedBook();
                    break;
                case 3:
                    GetAllAvailableBook();
                    break;
                default:
                    break;
            }
        }
        public void BorrowBook()
        {
            Console.WriteLine("Enter the *title* of the book to borrow: ");
            string bookTitleToBorrower = Console.ReadLine();
            /*BorrowedBook(SearchBookByTitle(bookTitleToBorrower));*/

            Book inputBook = SearchBookByTitle(bookTitleToBorrower);
            if (inputBook is not null)
            {
                if (inputBook.IsAvailable == false)
                {
                    string str = $"requested BOOK:{inputBook.Title} has already been borrowed";
                    ReturnStrInBox(str);
                }
                else 
                {
                    inputBook.Borrow();
                    string str = $"Book {inputBook.Title} has been successfully borrowed";
                    ReturnStrInBox(str);
                }
            }
            else
            {
                string str = $"Book {inputBook.Title} is not found in our Database";
                ReturnStrInBox(str);
            }
            Console.WriteLine();
            Console.ReadKey();
        }
        public void ReternBook()
        {
            Console.WriteLine("Enter the *title* of the book to return: ");
            string bookTitleToReturn = Console.ReadLine();

            Book inputBook = SearchBookByTitle(bookTitleToReturn);
            if (inputBook is not null)
            {
                if (inputBook.IsAvailable == true)
                {
                    string str = $"requested BOOK:{inputBook.Title} has already been returned";
                    ReturnStrInBox(str);
                }
                else
                {
                    inputBook.Return();
                    string str = $"Book {inputBook.Title} has been successfully returned";
                    ReturnStrInBox(str);
                }
            }
            else
            {
                string str = $"Book {inputBook.Title} is not found in our Database";
                ReturnStrInBox(str);
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}