using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace VendingMachineConsoleApp
{
    class Program
    {
        //Global Variables:
        //Each array has all phrases used in the application in their specific language
        static string[] english = { "Hi again!", "Please to insert your coins type: ENTER 0.15 (if you insert 15cts).If you need help type --help" };
        static string[] deutsch = { "Hallo nochmal!", "Bitte geben Sie Ihren Münztyp ein: Geben Sie 0,15 ein(wenn Sie 15 ct einwerfen).Wenn Sie Hilfe benötigen, geben Sie ein --help" };
        static string[] french = { "Hi again!", "Please to insert your coins type: ENTER 0.15 (if you insert 15cts).If you need help type --help" };
        //Total number of sentences
        static int totalSentences = english.Length;
        //Total number of slots
        static int slots = 3;
        //Price of selected product
        static double productPrice = 0.00;

        //method that generates help commands
        static void ShowHelpCommands()
        {
            String line;
            try
            {
                //CHANGE PATH TO A LOCAL PATH!
                string filePath = Path.GetFullPath(@"C:\Users\endi\Desktop\HelpCommands.txt");
                //Pass the file path to the StreamReader constructor
                StreamReader sr = new StreamReader(filePath);
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until the end of file
                while (line != null)
                {
                    //write the line to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();

                //ConsoleKeyInfo keyinfo;
                //keyinfo = Console.ReadKey();
                //Console.WriteLine(keyinfo.Key + " was pressed");
                string helpCommandByUser = Console.ReadLine();
                TypeHelpCommands(helpCommandByUser);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("We can not help you for the moment. Machine is broken :(");
                Console.WriteLine("Try again later.");
            }

        }

        //method that reads user command after help commands are shown
        static void TypeHelpCommands(string helpCommandByUser)
        {
            bool repeatHelp = true;
            while (repeatHelp)
            {
                switch (helpCommandByUser.ToLower())
                {
                    case "--currency":
                    case "currency":
                        Console.WriteLine("We are only using Euro. We will update our currencies in the future");
                        repeatHelp = false;
                        break;

                    case "--help":
                    case "help":
                        ShowHelpCommands();
                        repeatHelp = false;
                        break;

                    case "--slots":
                    case "slots":
                        DisplaySlotsAmount();
                        repeatHelp = false;
                        break;

                    case "--show":
                    case "shpw":
                        ReadProductFile("DisplayProducts");
                        repeatHelp = false;
                        break;

                }
            }


        }

        //method to fill the selectedLanguage array with the language chosen from command line
        static void FillLanguageArray(string language, List<string> selectedLanguage)
        {
            if (language == "e")
            {
                // Loop with foreach and write colors with string interpolation.
                for (int index = 0; index < english.Length; index++)
                {
                    selectedLanguage.Add(english[index]);
                }
            }
            if (language == "d")
            {
                // Loop with foreach and write colors with string interpolation.
                for (int index = 0; index < deutsch.Length; index++)
                {
                    selectedLanguage.Add(deutsch[index]);
                }
            }
            if (language == "f")
            {
                // Loop with foreach and write colors with string interpolation.
                for (int index = 0; index < french.Length; index++)
                {
                    selectedLanguage.Add(french[index]);
                }
            }
        }

        //method to read product file
        static void ReadProductFile(string fromFunction, int productId = 0)
        {
            String line, product, stock, price, displayStock;
            int productStock, index = 0;
            try
            {
                //CHANGE PATH TO A LOCAL PATH!
                string filePath = Path.GetFullPath(@"C:\Users\endi\Desktop\EnglishProducts.txt");
                //Pass the file path to the StreamReader constructor
                StreamReader sr = new StreamReader(filePath);
                //Read the first line of text: 
                line = sr.ReadLine();
                //Continue to read until the end of file
                while (line != null)
                {
                    index++;
                    //product name
                    product = line.Substring(line.IndexOf(":") + 1, line.IndexOf("-") - 2);
                    //product price
                    price = line.Substring(line.LastIndexOf("-") + 1, 4);
                    //product stock
                    stock = line.Substring(line.IndexOf("-") + 1, 2);
                    try
                    {
                        productStock = Convert.ToInt32(stock);
                    }
                    catch (FormatException fe1)
                    {
                        try
                        {
                            stock = stock.Remove(stock.Length - 1);
                            productStock = Convert.ToInt32(stock);
                        }

                        catch (FormatException fe2)
                        {
                            Console.WriteLine("Unknown problem while readinf product stockss. We are sorry :(");
                            continue;
                        }
                        Console.WriteLine("There are" + stock + "  " + product + " left");
                    }
                    //if there are no left products write sold out
                    if (productStock == 0)
                    {
                        displayStock = "SOLD OUT";
                    }
                    else
                    {
                        if (productStock == 1)
                        {
                            displayStock = productStock + " Item Left";
                        }
                        //if there are more than one products write item in plural
                        else
                        {
                            displayStock = productStock + " Items Left";
                        }
                    }

                    if (fromFunction == "DisplayProducts") { DisplayProducts(index, product, price, displayStock); }
                    if (fromFunction == "GetProductDetails"){ 
                        if(index == productId)
                        {
                            GetProductPrice(price);
                        }
                    }

                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("We can not show you the stocks for the moment. Machine is broken :(");
                Console.WriteLine("Try again later.");
            }
        }

        //method to display current product stocks
        static void DisplayProducts(int id, string product, string price, string displayStock)
        {
            Console.WriteLine("number " + id + " ." + product + " " + price + "Euro" + " - " + displayStock);
        }

        //method to display slots
        static void DisplaySlotsAmount()
        {
            if (slots == 1)
            {
                Console.WriteLine("The machine can take" + slots + " other slot");
            }
            else
            {
                if (slots == 0)
                {
                    Console.WriteLine("The machine can not take more slots");
                }
                else
                {
                    Console.WriteLine("The machine can take" + slots + " other slots");
                }
            }

        }

        //method to get the price of the product user chooses from command line
        static void GetProductPrice(string price = "")
        {
            productPrice = Convert.ToDouble(price);
        }



        static void Main(string[] args)
        {
            //The selectedLanguage array will be filled with the informations in the language that user has chosen
            List<string> selectedLanguage = new List<string>();
            bool entry = true; bool enterCoins = true;

            while (entry)
            {
                Console.WriteLine("Hello World! Hallo Welt! Bonjour le monde!");
                Console.WriteLine("Please type your preferred language to continue (type EN for English) ");
                Console.WriteLine("Bitte geben Sie Ihre bevorzugte Sprache ein, um fortzufahren(Typ DE für Deutsch) ");
                Console.WriteLine("Veuillez saisir votre langue préférée pour continuer(tapez FR pour le français) ");

                //firstCommunucation will read the first command that user inputs
                string firstCommunication = Console.ReadLine();

                switch (firstCommunication.ToLower())
                {
                    case "en":
                        entry = false;
                        FillLanguageArray("e", selectedLanguage);
                        break;

                    case "de":
                        entry = false;
                        FillLanguageArray("d", selectedLanguage);
                        break;

                    case "fr":
                        entry = false;
                        FillLanguageArray("f", selectedLanguage);
                        break;

                    case "--help":
                    case "help":
                        ShowHelpCommands();
                        entry = false;
                        break;

                    default:
                        Console.WriteLine("Unknown command. Please read again the information below!");
                        Console.WriteLine("...");
                        Thread.Sleep(1000);
                        continue;
                }

                while (enterCoins)
                {
                    Console.WriteLine(selectedLanguage[0] + selectedLanguage[1]);
                    //starting the machine to work with coins
                    string coins = Console.ReadLine();

                    if (coins.Length > 0)
                    {
                        if (coins.ToLower() == "--help" || coins.ToLower() == "help")
                        {
                            ShowHelpCommands();
                        }

                        if(coins.Length > 9)
                        { 
                            if (coins.ToLower().Substring(0, 5) == "enter" && coins.ToLower().Substring(7,1) == ".")
                            {
                                double amountEntered, pennys;
                                bool selectNumber = true;
                                try
                                {
                                    amountEntered = Convert.ToDouble(coins.Substring(6, 4));
                                    pennys = (int)(((decimal)amountEntered % 1) * 100);
                                    if (pennys % 5 == 0)
                                    {
                                        BuyingProcess process = new BuyingProcess(amountEntered);
                                        process.ShowTotalAmountEntered();
                                        while (selectNumber)
                                        {
                                            Console.WriteLine("To show all product details press --show");
                                            Thread.Sleep(500);
                                            Console.WriteLine("If you already know your product number, press SELECT Number");
                                            string userCommand = Console.ReadLine();
                                            switch (userCommand.ToLower())
                                            {
                                                case "--show":
                                                case "show":
                                                    ReadProductFile("DisplayProducts");
                                                    break;

                                                case "1":
                                                    selectNumber = false; 
                                                    ReadProductFile("GetProductDetails", 1);
                                                    process.CurrentProductPrice = productPrice;
                                                    if (process.HasEnoughMoney())
                                                    {
                                                        process.AmountDeduction();
                                                    }
                                                    else
                                                    {
                                                        selectNumber = true;
                                                        Console.WriteLine("You don't have enough money for this product.");
                                                        Thread.Sleep(500);
                                                        Console.WriteLine("Please choose another product.");
                                                    }
                                                    break;

                                                case "2":
                                                    selectNumber = false;
                                                    ReadProductFile("GetProductDetails", 2);
                                                    process.CurrentProductPrice = productPrice;
                                                    break;

                                                case "3":
                                                    selectNumber = false;
                                                    ReadProductFile("GetProductDetails", 3);
                                                    process.CurrentProductPrice = productPrice;
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid command. Try again!");
                                                    break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("The machine accepts only valid coins 5cts to 2Euro");
                                        Thread.Sleep(500);
                                        Console.WriteLine("Try again to insert coins...");
                                        Thread.Sleep(500);
                                    }
                                        
                                }
                                catch (FormatException fe)
                                {
                                    Console.WriteLine("Wrong Syntax. Please try again to enter coins!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Wrong Syntax. Please try again to enter coins!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong Syntax. Please try again to enter coins!");
                        }
                    }

                }


            }






        }
    }
}
