using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Names
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var phoneBook = InitPhoneBook();

            Console.WriteLine("Отсортировать записи? (y/n): ");
            var sortInput = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (sortInput == 'n' || sortInput == 'N')
            {
                return;
            }

            if (sortInput == 'y' || sortInput == 'Y')
            {
                phoneBook = phoneBook.OrderBy(c => c.Name).ThenBy(c => c.LastName).ToList();
            }

            const int pageSize = 2;
            int pageNumber = 1;

            while (true)
            {
                Console.WriteLine($"\nВведите номер страницы (1-{Math.Ceiling((double)phoneBook.Count / pageSize)}): ");
                var pageInput = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (char.IsDigit(pageInput))
                {
                    pageNumber = int.Parse(pageInput.ToString());

                    if (pageNumber < 1 || pageNumber > (phoneBook.Count + pageSize - 1) / pageSize)
                    {
                        Console.WriteLine("Неправильный номер страницы.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод, пожалуйста, введите число.");
                    continue;
                }

                DisplayPage(phoneBook, pageNumber, pageSize);
            }
        }

        static void DisplayPage(List<Contact> phoneBook, int pageNumber, int pageSize)
        {
            int skipAmount = (pageNumber - 1) * pageSize;

            var page = phoneBook.Skip(skipAmount).Take(pageSize).ToList();

            Console.WriteLine($"Телефонная книга - Страница {pageNumber}");
            
            foreach (var contact in page)
            {
                Console.WriteLine($"Имя: {contact.Name}, Фамилия: {contact.LastName}, Телефон: {contact.PhoneNumber}, Email: {contact.Email}");
            }
        }

        static List<Contact> InitPhoneBook()
        {
            var phoneBook = new List<Contact>();

            phoneBook.Add(new Contact("Игорь", "Николаев", 79990000001, "igor@example.com"));
            phoneBook.Add(new Contact("Сергей", "Довлатов", 79990000010, "serge@example.com"));
            phoneBook.Add(new Contact("Анатолий", "Карпов", 79990000011, "anatoly@example.com"));
            phoneBook.Add(new Contact("Валерий", "Леонтьев", 79990000012, "valera@example.com"));
            phoneBook.Add(new Contact("Сергей", "Брин", 799900000013, "serg@example.com"));
            phoneBook.Add(new Contact("Иннокентий", "Смоктуновский", 799900000015, "innokentii@example.com"));

            return phoneBook;
        }
    }

    public class Contact
    {
        public Contact(string name, string lastName, long phoneNumber, string email)
        {
            Name = name;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string Name { get; }
        public string LastName { get; }
        public long PhoneNumber { get; }
        public string Email { get; }
    }
}
