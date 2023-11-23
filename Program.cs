// Money Tracking Application
//The application can track expense and income entered by the user
//The user is capable of adding, updating and removing the tasks in the list
//The user can also quit and save the current task list to file

using MoneyTracking;
using System.Diagnostics;
using System.Reflection;

const string filepath = "MoneyTracking.txt"; //Will directly go to the debug folder by default as path not specified

ShowMenuItems();


//Function to show the main menu on Console
void ShowMenuItems()
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Welcome to the Track Money.");
    Console.WriteLine("1-Show Transactions");
    Console.WriteLine("2-Add new Transaction (Expense/Income)");
    Console.WriteLine("3-Edit a Transaction");
    Console.WriteLine("4-Delete a Transaction ");
    Console.WriteLine("5-Quit");

    Console.Write("Pick an option : ");
    Console.ResetColor();

    string userInput = Console.ReadLine();
    switch (userInput)
    {
        case "1":
            ShowTransaction();//List all the transactions in the file
            break;
        case "2":
            AddTransaction();//Add Transaction in the file
            break;
        case "3":
            EditTransaction();//Edit Transaction in the file
            break;
        case "4":
            DeleteTransaction();//Delete Transaction in the file
            break;
        case "5":
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Thank you for using this application!");//Quit the application
            Console.ResetColor();            
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid option. Please try again.");//Invalid input from the user
            Console.ResetColor();
            ShowMenuItems();
            break;
    }
}

void EditTransaction()
{
    //Take input from user for the record to delete from file
    int Flag = 0;   //Flag to record in case we have any null values entered
    bool EditFlag = false;  //Flag to record, we found a matching record for edit

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Please enter the information of record you wanna edit.");
    //Title of the record
    Console.Write("Enter the Old Title :");
    Console.ResetColor();
    string OldTitle = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the New Title :");
    Console.ResetColor();
    string NewTitle = Console.ReadLine();

    //Type of the record
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the Old Type :");
    Console.ResetColor();
    string OldType = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the New Type :");
    Console.ResetColor();
    string NewType = Console.ReadLine();

    //Month of the record
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the Old Month :");
    Console.ResetColor();
    string OldMonth = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the New Month :");
    Console.ResetColor();
    string NewMonth = Console.ReadLine();

    //Amount of the record
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the Old Amount :");
    Console.ResetColor();
    string OldAmt = Console.ReadLine();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the New Amount :");
    Console.ResetColor();
    string NewAmt = Console.ReadLine();

    if (string.IsNullOrEmpty(OldTitle) || string.IsNullOrEmpty(OldType) || string.IsNullOrEmpty(OldMonth) || string.IsNullOrEmpty(OldAmt) 
            || string.IsNullOrEmpty(NewTitle) || string.IsNullOrEmpty(NewType) || string.IsNullOrEmpty(NewMonth) || string.IsNullOrEmpty(NewAmt))
    {
        Flag = 1;
    }

    try
    {
        if (Flag == 0)
        {
            List<Transaction> AllEntries = new List<Transaction>();
            List<String> lines = File.ReadAllLines(filepath).ToList();
            if (lines.Count > 0)
            {
                foreach (var line in lines)
                {
                    string[] entries = line.Split(',');
                    Transaction newtxn = new Transaction();
                    newtxn.Title = entries[0];
                    newtxn.Type = entries[1];
                    newtxn.Month = entries[2];
                    newtxn.Amount = float.Parse(entries[3]);

                    if (entries[0].ToLower().Trim() != OldTitle.ToLower().Trim())
                    {
                        AllEntries.Add(newtxn);
                    }
                    else if (entries[1].ToLower().Trim() != OldType.ToLower().Trim())
                    {
                        AllEntries.Add(newtxn);
                    }
                    else if (entries[2].ToLower().Trim() != OldMonth.ToLower().Trim())
                    {
                        AllEntries.Add(newtxn);
                    }
                    else if(entries[3].ToLower().Trim() != OldAmt.ToLower().Trim())
                    {
                        AllEntries.Add(newtxn);
                    }

                    if(entries[0].ToLower().Trim() == OldTitle.ToLower().Trim() && entries[1].ToLower().Trim() == OldType.ToLower().Trim()
                        && entries[2].ToLower().Trim() == OldMonth.ToLower().Trim() && entries[3].ToLower().Trim() == OldAmt.ToLower().Trim())
                    {
                        bool chkAmount = float.TryParse(NewAmt, out float amt);

                        if (chkAmount)
                        {
                            AllEntries.Add(new Transaction { Title = NewTitle, Type = NewType, Month = NewMonth, Amount = amt });
                        }
                        EditFlag = true;
                    }
                }
                if (EditFlag == false)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("No Matching Records found in file.");
                    Console.ResetColor();
                }
                else
                {
                    List<string> Input = new List<string>();
                    foreach (var entry in AllEntries)
                    {
                        Input.Add($"{entry.Title},{entry.Type},{entry.Month},{entry.Amount}");
                    }
                    File.WriteAllLines(filepath, Input);
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Records edited in file.");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no records in the file.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There is an invalid entry. Please try again.");
            Console.ResetColor();
        }
        ShowMenuItems();
    }
    catch (FileNotFoundException Ex)
    {
        Console.WriteLine("The file doesnot exist on the specified location. " + DateTime.Now + " " + Ex.Message);
    }
    catch (Exception Ex)
    {
        Console.WriteLine("Error : " + DateTime.Now + Ex.Message);
    }
}


//Function to delete a specific row from the file 
void DeleteTransaction()
{
    //Take input from user for the record to delete from file
    int Flag = 0;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Please enter the information of record you wanna delete.");
    //Title of the record
    Console.Write("Enter the Title :");
    Console.ResetColor();
    string InputTitle = Console.ReadLine();

    //Type of the record
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the Type :");
    Console.ResetColor();
    string InputType = Console.ReadLine();

    //Month of the record
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("Enter the Month :");
    Console.ResetColor();
    string InputMonth = Console.ReadLine();
    
    if (string.IsNullOrEmpty(InputTitle) || string.IsNullOrEmpty(InputType) || string.IsNullOrEmpty(InputMonth))
    {
        Flag = 1;
    }
       
        try
        {
        if(Flag == 0)   
        {
            List<Transaction> AllEntries = new List<Transaction>();
            List<String> lines = File.ReadAllLines(filepath).ToList();
            if (lines.Count > 0)
            {
                foreach (var line in lines)
                {
                    string[] entries = line.Split(',');
                    Transaction newtxn = new Transaction();
                    newtxn.Title = entries[0];
                    newtxn.Type = entries[1];
                    newtxn.Month = entries[2];
                    newtxn.Amount = float.Parse(entries[3]);

                    if (entries[0].ToLower().Trim() != InputTitle.ToLower().Trim())
                    {
                        AllEntries.Add(newtxn);
                    }
                    else if (entries[1].ToLower().Trim() != InputType.ToLower().Trim())
                    {
                        AllEntries.Add(newtxn);
                    }
                    else if(entries[2].ToLower().Trim() != InputMonth.ToLower().Trim())
                    {
                        AllEntries.Add(newtxn);
                    }
                    
                }
                List<string> Input = new List<string>();
                foreach (var entry in AllEntries)
                {
                    Input.Add($"{entry.Title},{entry.Type},{entry.Month},{entry.Amount}");
                }
                File.WriteAllLines(filepath, Input);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Records deleted from file.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There are no records in the file.");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There is an invalid entry. Please try again.");
            Console.ResetColor();
        }
        ShowMenuItems();
    }
    catch (FileNotFoundException Ex)
    {
        Console.WriteLine("The file doesnot exist on the specified location. " + DateTime.Now + " " + Ex.Message);
    }
    catch (Exception Ex)
    {
        Console.WriteLine("Error : " + DateTime.Now + Ex.Message);
    }
}

//Function to add new entry to the Money Tracking file
void AddTransaction()
{
    try
    {

        while (true)
        {
            int flag = 0; // Flag to record any invalid entries
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("To enter a new Income/Expense entry - follow the steps | To Quit Enter : 'Q'");
            Console.ResetColor();

            //Entering Title of Transaction
            Console.Write("ENTER THE TITLE OF TXN : ");
            string title = Console.ReadLine();
            if (string.IsNullOrEmpty(title))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry.");
                Console.ResetColor();
                flag = 1;
            }
            else if (title.ToLower().Trim() == "q")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Exiting the Add Application.");
                Console.ResetColor();
                break;
            }

            //Entering Type of Transaction
            Console.Write("ENTER THE TYPE OF TXN (INCOME/EXPENSE) : ");
            string type = Console.ReadLine();
            if (string.IsNullOrEmpty(type))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry.");
                Console.ResetColor();
                flag = 1;
            }
            else if (type.ToLower().Trim() == "q")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Exiting the Add Application.");
                Console.ResetColor();
                break;
            }
            else if (!(type.ToLower().Trim() == "income" || type.ToLower().Trim() == "expense"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry.");
                Console.ResetColor();
                flag = 1;
            }

            //Entering Month of Transaction
            Console.Write("ENTER THE MONTH OF TXN : ");
            string month = Console.ReadLine();
            if (string.IsNullOrEmpty(month))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry.");
                Console.ResetColor();
                flag = 1;
            }
            else if (month.ToLower().Trim() == "q")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Exiting the Add Application.");
                Console.ResetColor();
                break;
            }

            //Entering Month of Transaction
            Console.Write("ENTER THE AMOUNT OF TXN : ");
            string amount = Console.ReadLine();

            bool chkAmount = float.TryParse(amount, out float amt); //Check whether the amount is coming in right format or not

            if (string.IsNullOrEmpty(amount))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This is an invalid entry.");
                Console.ResetColor();
                flag = 1;
            }
            else if (amount.ToLower().Trim() == "q")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Exiting the Add Application.");
                Console.ResetColor();
                break;
            }
            else
            {
                if (!chkAmount)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This is an invalid entry.");
                    Console.ResetColor();
                    flag = 1;
                }
            }
            if (flag == 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There is an invalid entry. Please try again.");
                Console.ResetColor();
            }
            else
            {
                List<Transaction> items = new List<Transaction>();
                items.Add(new Transaction { Title = title, Type = type, Month = month, Amount = amt });
                List<string> Input = new List<string>();
                foreach (var entry in items)
                {
                    Input.Add($"{entry.Title},{entry.Type},{entry.Month},{entry.Amount}");
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Writing to the text file");
                File.AppendAllLines(filepath, Input); //Appends the records at the end of file, if file doesnt exist then create it  
                                                      //File.WriteAllLines(filepath, Input);
                Console.WriteLine("Entry written to the text file");
                Console.ResetColor();
            }
        }

        ShowMenuItems();
    }
    catch (FileNotFoundException Ex)
    {
        Console.WriteLine("The file doesnot exist on the specified location. " + DateTime.Now + " " + Ex.Message);
    }
    catch (Exception Ex)
    {
        Console.WriteLine("Error : " + DateTime.Now + Ex.Message);
    }
}

//Function to show all the transactions in File on Console
void ShowTransaction()
{
    try
    {

        //Take input from user what he want to see Income/Expense/All
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Please choose the type- Income/Expense/All :");
        Console.ResetColor();
        string InputType = Console.ReadLine();

        //Check to find we get the right input from the user or not
        if (!(InputType.ToUpper().Trim() == "INCOME" || InputType.ToUpper().Trim() == "EXPENSE" || InputType.ToUpper().Trim() == "ALL") || string.IsNullOrEmpty(InputType))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This is an invalid entry. There are no records to display.");
            Console.ResetColor();
        }

        List<Transaction> AllEntries = new List<Transaction>();
        List<String> lines = File.ReadAllLines(filepath).ToList();
        if (lines.Count > 0)
        {
            foreach (var line in lines)
            {
                string[] entries = line.Split(',');
                Transaction newtxn = new Transaction();
                newtxn.Title = entries[0];
                newtxn.Type = entries[1];
                newtxn.Month = entries[2];
                newtxn.Amount = float.Parse(entries[3]);

                AllEntries.Add(newtxn);
            }
            List<Transaction> sortedEntries = AllEntries.OrderBy(Transaction => Transaction.Month).ThenBy(Transaction => Transaction.Title).ToList();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Sorted Money Tracking List by Month and Title :");
            Console.ResetColor();
            Console.WriteLine("TITLE".PadRight(15) + "TYPE".PadRight(15) + "MONTH".PadRight(15) + "AMOUNT");
            foreach (var entry in sortedEntries)
            {
                if (entry.Type.ToUpper().Trim() == InputType.ToUpper().Trim())
                {
                    Console.WriteLine($"{entry.Title.ToUpper().PadRight(15)}{entry.Type.ToUpper().PadRight(15)}{entry.Month.ToUpper().PadRight(15)}{entry.Amount} ");
                }
                else if (InputType.ToUpper().Trim() == "ALL")
                {
                    Console.WriteLine($"{entry.Title.ToUpper().PadRight(15)}{entry.Type.ToUpper().PadRight(15)}{entry.Month.ToUpper().PadRight(15)}{entry.Amount} ");
                }
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no records in the file.");
            Console.ResetColor();
        }
        ShowMenuItems();
    }
    catch (FileNotFoundException Ex)
    {
        Console.WriteLine("The file doesnot exist on the specified location. " + DateTime.Now + " " + Ex.Message);
    }
    catch (Exception Ex)
    {
        Console.WriteLine("Error : " + DateTime.Now + Ex.Message);
    }
}