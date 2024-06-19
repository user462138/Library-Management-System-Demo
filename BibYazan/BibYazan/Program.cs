using System;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BibYazan
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Book> books = Book.ReadCsvFile();
                Library bibyazan = new Library(books);
                while (true)
                {
                    Console.WriteLine("Wat wil je doen?");
                    Console.WriteLine("1. Add a book");
                    Console.WriteLine("2. Change Book Info");
                    Console.WriteLine("3. Display book info");
                    Console.WriteLine("4. Search book");
                    Console.WriteLine("5. Delete a book");
                    Console.WriteLine("6. Show books");
                    Console.WriteLine("7. Borrowed book");
                    Console.WriteLine("8. Return book");
                    Console.WriteLine("9. Add a newspaper");
                    Console.WriteLine("10. Add a monthly magazine");
                    Console.WriteLine("11. Show all newspapers");
                    Console.WriteLine("12. Show all magazine");
                    Console.WriteLine("13. Show all acquisitions from the reading room");
                    Console.WriteLine("0. TEST");
                    try
                    {
                        int keuze = Convert.ToInt32(Console.ReadLine());
                        switch (keuze)
                        {
                            case 1:
                                bibyazan.AddBook();
                                break;
                            case 2:
                                bibyazan.ChangeBookInfo();
                                break;
                            case 3:
                                bibyazan.GetBookInfoShort();
                                break;
                            case 4:
                                bibyazan.SearchBookBy();
                                break;
                            case 5:
                                bibyazan.DeleteBook();
                                break;
                            case 6:
                                bibyazan.ShowBooks();
                                break;
                            case 7:
                                bibyazan.BorrowBook();
                                break;
                            case 8:
                                bibyazan.ReternBook();
                                break;
                            case 9:
                                bibyazan.AddNewspaper();
                                break;
                            case 10:
                                bibyazan.AddMagazine();
                                break;
                            case 11:
                                bibyazan.ShowAllNewspapers();
                                break;
                            case 12:
                                bibyazan.ShowAllMagazines();
                                break;
                            case 13:
                                bibyazan.AcquisitionsReadingRoomToday();
                                break;
                            case 0:
                                TEST();
                                break;
                            default:
                                Book.ReturnStrInBox("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    catch (FormatException fx)
                    {
                        Book.ReturnStrInBox(fx.Message);
                    }
                    catch (OverflowException oe)
                    {
                        Book.ReturnStrInBox(oe.Message);
                    }
                }
            }
            catch (DirectoryNotFoundException dnfe)
            {
                Console.WriteLine(dnfe.Message);
            }
        }
        public static void TEST()
        {
            foreach (var item in Enum.GetNames<Genre>())
            {
                Console.WriteLine(item);
            }
        }
    }
}