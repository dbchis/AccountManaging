using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace AccountManaging
{
    public class Account
    {
        private int accountID;
        private int balance;
        private string firstName;
        private string lastName;
        public Account()
        {

        }
        public Account(int acc,  string first, string last, int bal)
        {
            accountID = acc;
            balance = bal;
            firstName = first;
            lastName = last;
        }

        public int AccountID { get => accountID; set => accountID = value; }
        public int Balance { get => balance; set => balance = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }

        public override string ToString()
        {
            return $"ID: {AccountID} |Name: {FirstName} {LastName}" +
                $" |Balance: {Balance}";
        }
        public void FillInfo()
        {
            Console.WriteLine("ID: ");
            AccountID = int.Parse(Console.ReadLine());
            Console.WriteLine("FirstName: ");
            FirstName = Console.ReadLine();
            Console.WriteLine("LastName: ");
            LastName = Console.ReadLine();
            Console.WriteLine("Balance: ");
            Balance = int.Parse(Console.ReadLine());

        }
        public void Query()
        {
            Console.WriteLine(ToString());
        }
    }
    public class AccountList
    {
        ArrayList Accounts = new ArrayList();
        public void NewAccount()
        {
            Account acc = new Account();
            acc.FillInfo();
            Accounts.Add(acc);
        }
        public void Report()
        {
            foreach(Account acc in Accounts)
            {
                acc.Query();
            }
        }
        public void SaveFile()
        {
            Console.Write("Input filename to save: ");
            string filename = Console.ReadLine();
            try
            {
                FileStream output = new FileStream(filename,
                    FileMode.CreateNew, FileAccess.Write);
                StreamWriter writer = new StreamWriter(output);

                foreach(Account acc in Accounts)
                {
                    writer.WriteLine($"{acc.AccountID}|{acc.FirstName}|" +
                        $"{acc.LastName}|{acc.Balance}");
                }
                writer.Close();
                output.Close();

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);

            }
        }
        public void LoadFile()
        {
            Console.Write("Input file name to load: ");
            string filename = Console.ReadLine();
            Accounts.Clear();
            try
            {
                FileStream input = new FileStream(filename,
                FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(input);
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    string[] list = str.Split('|');
                    Account acc = new Account(
                        int.Parse(list[0]), list[1], list[2], int.Parse(list[3]));
                    Accounts.Add(acc);

                }
                input.Close();
                reader.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);

            }
        }
        public void main()
        {
            int a = 0;
            do
            {
                Console.Write("\n1.Add");
                Console.Write("|2.Save");
                Console.Write("|3.Load");
                Console.Write("|4.Report");
                Console.Write("|0.Exit\n");

                Console.Write("Enter: ");
                // Read the input from the user
                string input = Console.ReadLine();

                // Attempt to parse the input to an integer
                if (int.TryParse(input, out int number))
                {
                    a = number;
                    switch (number)
                    {
                        case 1:
                            NewAccount();
                            Console.WriteLine("Add Account Success");
                            break;
                        case 2:
                            SaveFile();
                            Console.WriteLine("SaveFile Success");
                            break;
                        case 3:
                            LoadFile();
                            Console.WriteLine("LoadFile Success");
                            break;
                        case 4:
                            Console.WriteLine("-------List--------");
                            Report();
                            break;
                        case 0:
                            break;


                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            } while (a > 0);



        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            AccountList accList = new AccountList();
            accList.main();
        }
    }
}
