using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;


//add while test
//if product stock 0

namespace VendingMachineConsoleApp
{
    class Program
    {
        //Global Variables:
        //Each array has all phrases used in the application in their specific language : 28
        static string[] english = { "Please to insert your coins type: ENTER 0.15 (if you insert 15cts or 1.00 for 1Euro).If you need help type --help", "Amount entered: ", "To show all product details press --show", "If you already know your product number, press SELECT Number", "If you want to enter more coins, press COINS and you'll get the instructions", "You don't have enough money for this product.", "Please choose another product.", "Wrong Syntax. Please try again to enter coins!", "The machine accepts only valid coins 5cts to 2Euro", "Try again to insert coins...", "We can not help you for the moment. Machine is broken :(", "Try again later.", "We are only using Euro. We will update our currencies in the future", "Unknown problem while reading product stocks. We are sorry :(", "There are ", " left", "number ", "The machine can take ", " other slot", "The machine can not take more slots", "  slots", "Amount left: ", "Product number ", " was dispensed", "Thank you for using our machine!", "Please take your change: ", "The minimum price of the products is 50 cents if you do not want to continue, type RETURN COINS or else thing to continue", "Take your coins and Thank you! :)" };
        static string[] deutsch = { "Bitte geben Sie Ihren Münztyp ein: ENTER 0.15 (wenn Sie 15 ct einwerfen oder 1.00 für 1Euro).Wenn Sie Hilfe benötigen, geben Sie ein --help", "Eingegebener Betrag: ", "Um alle Produktdetails anzuzeigen, drücken Sie --show", "Wenn Sie Ihre Produktnummer bereits kennen, drücken Sie SELECT Number", "Wenn Sie mehr Münzen eingeben möchten, drücken Sie COINS und Sie erhalten die Anweisungen.", "Sie haben nicht genug Geld für dieses Produkt.", "Bitte wählen Sie ein anderes Produkt.", "Falsche Syntax. Bitte versuchen Sie erneut, Münzen einzugeben!", "Der Automat akzeptiert nur gültige Münzen von 5 ct bis 2Euro", "Versuchen Sie erneut, Münzen einzuwerfen...", "Wir können Ihnen im Moment nicht helfen. Maschine ist kaputt :(", "Versuchen Sie es später noch einmal.", "Wir verwenden nur Euro. Wir werden unsere Währungen in Zukunft aktualisieren", "Unbekanntes Problem beim Lesen der Produktbestände. Es tut uns leid :(", "Es gibt ", " übrig", "nummer ", "Die Maschine kann es nehmen ", " anderer Steckplatz", "Die Maschine kann nicht mehr Slots nehmen", "  Schlüssel", "Restbetrag: ", "Produktnummer ", " wurde abgegeben", "Danke, dass Sie unsere Maschine benutzen!", "Bitte nehmen Sie Ihr Wechselgeld: ", "Der Mindestpreis der Produkte beträgt 50 Cent Wenn Sie nicht fortfahren möchten, geben Sie RETURN COINS ein oder sonst etwas, um fortzufahren", "Nimm deine Münzen und danke! :)" };
        static string[] french = { "Veuillez insérer votre type de pièces : ENTER 0.15 (si vous insérez 15 cts ou 1.00 pour 1Euro). Si vous avez besoin d'aide, tapez --help", "Montant saisi: ", "Pour afficher tous les détails du produit, appuyez sur --show", "Si vous connaissez déjà votre numéro de produit, appuyez sur SELECT Number", "Si vous voulez entrer plus de pièces, appuyez sur COINS et vous obtiendrez les instructions", "Sie haben nicht genug Geld für dieses Produkt.", "Veuillez choisir un autre produit.", "Mauvaise syntaxe. Veuillez réessayer d'entrer des pièces !", "La machine accepte uniquement les pièces valables de 5cts à 2Euro", "Essayez à nouveau d'insérer des pièces...", "Nous ne pouvons pas vous aider pour le moment. La machine est cassée :(", "Réessayez plus tard.", "Nous n'utilisons que l'euro. Nous mettrons à jour nos devises à l'avenir", "Problème inconnu lors de la lecture des stocks de produits. Nous sommes désolés :(", "Il y a ", " restants", "numéro ", "La machine peut prendre ", " autre emplacement ", "La machine ne peut pas prendre plus de créneaux", " créneaux ", "Montant restant: ", "Numéro de produit ", " a été dispensé ", "Merci d'utiliser notre machine !", "Veuillez prendre votre monnaie : ", "Le prix minimum des produits est de 50 cents si vous ne voulez pas continuer, tapez RETURN COINS sinon chose à continuer", "Prenez vos pièces et merci! :)" };
        //Total coins
        static double totalAmountOfCoins = 0;
        //Total number of slots
        static int slots = 3;
        //Price of selected product
        static double productPrice = 0;
        //string indicating selected language
        static string lang = "en";

        //method that generates help commands
        static void ShowHelpCommands(List<string> selectedLanguage)
        {
            string line;
            string filePath;
            int path = AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin");
            string fileName = AppDomain.CurrentDomain.BaseDirectory.Substring(0, path);
            bool flag = true; //when flag is set to false it indicates that block of code has been executed without any errors or exceptions
            while (flag)
            {
                try
                {
                    switch (lang)
                    {

                        case "en":  
                            filePath = fileName + "Files\\HelpCommandsEN.txt";
                            break;

                        case "de":
                            filePath = fileName + "Files\\HelpCommandsDE.txt";
                            break;

                        case "fr":
                            filePath = fileName + "Files\\HelpCommandsFR.txt";
                            break;

                        default:
                            filePath = fileName + "Files\\HelpCommandsEN.txt";
                            break;
                    }

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

                    string helpCommandByUser = Console.ReadLine();
                    TypeHelpCommands(helpCommandByUser, selectedLanguage);

                    flag = false;
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(selectedLanguage[10]);
                    Console.WriteLine(selectedLanguage[11]);
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unknown Error Occured :(");
                }
            }
            flag = true;
        }

        //method that reads user command after help commands are shown
        static void TypeHelpCommands(string helpCommandByUser, List<string> selectedLanguage)
        {
            bool repeatHelp = true;
            while (repeatHelp)
            {
                switch (helpCommandByUser.ToLower())
                {
                    case "--currency":
                    case "currency":
                        Console.WriteLine(selectedLanguage[12]);
                        repeatHelp = false;
                        break;

                    case "--help":
                    case "help":
                        ShowHelpCommands(selectedLanguage);
                        repeatHelp = false;
                        break;

                    case "--slots":
                    case "slots":
                        DisplaySlotsAmount(selectedLanguage);
                        repeatHelp = false;
                        break;

                    case "--show":
                    case "show":
                        ReadProductFile("DisplayProducts", selectedLanguage, 0);
                        repeatHelp = false;
                        break;

                    default:
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
                lang = "en";
            }
            if (language == "d")
            {
                // Loop with foreach and write colors with string interpolation.
                for (int index = 0; index < deutsch.Length; index++)
                {
                    selectedLanguage.Add(deutsch[index]);
                }
                lang = "de";
            }
            if (language == "f")
            {
                // Loop with foreach and write colors with string interpolation.
                for (int index = 0; index < french.Length; index++)
                {
                    selectedLanguage.Add(french[index]);
                }
                lang = "fr";
            }
        }

        //method to read product file
        static void ReadProductFile(string fromFunction, List<string> selectedLanguage, int productId = 0)
        {
            String line, product, stock, price, displayStock;
            int productStock, index = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    int path = AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin");
                    string filePath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, path) + "Files\\Products.txt";
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
                                Console.WriteLine(selectedLanguage[13]);
                                continue;
                            }
                            Console.WriteLine(selectedLanguage[14] + stock + "  " + product + selectedLanguage[15]);
                        }
                        //if there are no left products write sold out
                        if (productStock == 0)
                        {
                            displayStock = "SOLD OUT";
                        }
                        else
                        {
                            displayStock = productStock + selectedLanguage[15];
                        }

                        if (fromFunction == "DisplayProducts") { DisplayProducts(index, product, price, displayStock, selectedLanguage); }
                        if (fromFunction == "GetProductDetails")
                        {
                            if (index == productId)
                            {
                                GetProductPrice(price);
                            }
                        }

                        //Read the next line
                        line = sr.ReadLine();
                    }
                    //close the file
                    sr.Close();
                    flag = false;

                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("We can not show you the stocks for the moment. Machine is broken :(");
                    Console.WriteLine("Try again later.");
                }

                flag = true;
            }
        }

        //method to display current product stocks
        static void DisplayProducts(int id, string product, string price, string displayStock, List<string> selectedLanguage)
        {
            Console.WriteLine(selectedLanguage[16] + id + " ." + product + " " + price + "Euro" + " - " + displayStock);
        }

        //method to display slots
        static void DisplaySlotsAmount(List<string> selectedLanguage)
        {
            if (slots == 1)
            {
                Console.WriteLine(selectedLanguage[17] + slots + selectedLanguage[18]);
            }
            else
            {
                if (slots == 0)
                {
                    Console.WriteLine(selectedLanguage[19]);
                }
                else
                {
                    Console.WriteLine(selectedLanguage[17] + slots + selectedLanguage[20]);
                }
            }

        }

        //method to get the price of the product user chooses from command line
        static void GetProductPrice(string price = "")
        {
            productPrice = Convert.ToDouble(price);
        }

        //method to indicate the end of buying process
        static void FinishBuying(double amountLeft, List<string> selectedLanguage, int id)
        {
            amountLeft = Math.Round(amountLeft, 2);
            Console.WriteLine(selectedLanguage[21] + amountLeft + " Euro");
            Console.WriteLine(selectedLanguage[22] + id + selectedLanguage[23]);
            Thread.Sleep(300);
            Console.WriteLine(selectedLanguage[24]);
            Thread.Sleep(300);
            Console.WriteLine(selectedLanguage[25] + amountLeft);
            Console.WriteLine(); Console.WriteLine();
            totalAmountOfCoins = 0;
            productPrice = 0;
        }

        //method to write file: remove 1 product stock after a successful buying
        static void MarkProductAsSold(int id)
        {
            int path = AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin");
            string fileName = AppDomain.CurrentDomain.BaseDirectory.Substring(0, path) + "Files\\Products.txt";
            string[] arrAllLines = File.ReadAllLines(fileName);
            string[] splitedLine = arrAllLines[id - 1].Split('-');
            int productStockStr = Convert.ToInt32(splitedLine[1])-1;
            string newLine = splitedLine[0] + "-" + productStockStr + "-" + splitedLine[2];
            arrAllLines[id - 1] = newLine;
            File.WriteAllLines(fileName, arrAllLines);
        }

        static void Main(string[] args)
        {
            //The selectedLanguage array will be filled with the informations in the language that user has chosen
            List<string> selectedLanguage = new List<string>();
            bool entry = true; bool enterCoins = false;

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
                        enterCoins = true;
                        FillLanguageArray("e", selectedLanguage);
                        break;

                    case "de":
                        entry = false;
                        enterCoins = true;
                        FillLanguageArray("d", selectedLanguage);
                        break;

                    case "fr":
                        entry = false;
                        enterCoins = true;
                        FillLanguageArray("f", selectedLanguage);
                        break;

                    case "--help":
                    case "help":
                        FillLanguageArray("e", selectedLanguage);
                        ShowHelpCommands(selectedLanguage);
                        entry = true;
                        continue;

                    default:
                        Console.WriteLine("Unknown command. Please read again the information below!");
                        Console.WriteLine("...");
                        Thread.Sleep(1000);
                        continue;
                }


                while (enterCoins)
                {
                    Console.WriteLine(selectedLanguage[0]);
                    //starting the machine to work with coins
                    string coins = Console.ReadLine();

                    if (coins.ToLower() == "--help" || coins.ToLower() == "help")
                    {
                        ShowHelpCommands(selectedLanguage);
                        break;
                    }

                    if (coins.Length > 9)
                    {
                        if (coins.ToLower().Substring(0, 5) == "enter" && coins.ToLower().Substring(7, 1) == ".")
                        {
                            //slots--;

                            //if (slots == 0)
                            //{
                            //    enterCoins = false;
                            //    Console.WriteLine("You have inserted coins 3 times");
                            //}
                            //else { Console.WriteLine("You can also insert " + slots + " times coins"); }

                            double amountEntered, pennys;
                            bool selectNumber = true;
                            try
                            {
                                amountEntered = Convert.ToDouble(coins.Substring(6, 4));
                                pennys = (int)(((decimal)amountEntered % 1) * 100);
                                if (pennys % 5 == 0)
                                {
                                    totalAmountOfCoins = totalAmountOfCoins + amountEntered;
                                    if (totalAmountOfCoins < 0.50)
                                    {
                                        Console.WriteLine(selectedLanguage[26]);
                                        string returnAnswer = Console.ReadLine();
                                        if (returnAnswer == "RETURN COINS" || returnAnswer == "return coins")
                                        {
                                            Console.WriteLine(selectedLanguage[27]);
                                            Console.WriteLine();
                                            Console.WriteLine();
                                            Console.WriteLine("--------------------");
                                            Thread.Sleep(2000);
                                            enterCoins = false;
                                            entry = true;
                                            totalAmountOfCoins = 0;
                                            break;
                                        }
                                    }
                                    BuyingProcess process = new BuyingProcess(totalAmountOfCoins);
                                    Console.WriteLine(selectedLanguage[1] + process.ShowTotalAmount() + " Euro");

                                    while (selectNumber)
                                    {
                                        Console.WriteLine(selectedLanguage[2]);
                                        Thread.Sleep(500);
                                        Console.WriteLine(selectedLanguage[3]);
                                        Thread.Sleep(500);
                                        // if (slots != 0)
                                        // {
                                        Console.WriteLine(selectedLanguage[4]);
                                        // } 
                                        string userCommand = Console.ReadLine();
                                        switch (userCommand.ToLower())
                                        {
                                            case "--show":
                                            case "show":
                                                ReadProductFile("DisplayProducts", selectedLanguage, 0);
                                                break;

                                            case "select 1":
                                                selectNumber = false;
                                                ReadProductFile("GetProductDetails", selectedLanguage, 1);
                                                process.CurrentProductPrice = productPrice;
                                                if (process.HasEnoughMoney())
                                                {
                                                    process.AmountDeduction();
                                                    FinishBuying(process.ShowTotalAmount(), selectedLanguage, 1);
                                                    MarkProductAsSold(1);
                                                    enterCoins = false;
                                                    entry = true;
                                                    //slots = 3;
                                                }
                                                else
                                                {
                                                    selectNumber = true;
                                                    Console.WriteLine(selectedLanguage[5]);
                                                    Thread.Sleep(500);
                                                    Console.WriteLine(selectedLanguage[6]);
                                                }
                                                break;

                                            case "select 2":
                                                selectNumber = false;
                                                ReadProductFile("GetProductDetails", selectedLanguage, 2);
                                                process.CurrentProductPrice = productPrice;
                                                if (process.HasEnoughMoney())
                                                {
                                                    process.AmountDeduction();
                                                    FinishBuying(process.ShowTotalAmount(), selectedLanguage, 2);
                                                    enterCoins = false;
                                                    entry = true;
                                                    //slots = 3;
                                                }
                                                else
                                                {
                                                    selectNumber = true;
                                                    Console.WriteLine(selectedLanguage[5]);
                                                    Thread.Sleep(500);
                                                    Console.WriteLine(selectedLanguage[6]);
                                                }
                                                break;

                                            case "select 3":
                                                selectNumber = false;
                                                ReadProductFile("GetProductDetails", selectedLanguage, 3);
                                                process.CurrentProductPrice = productPrice;
                                                if (process.HasEnoughMoney())
                                                {
                                                    process.AmountDeduction();
                                                    FinishBuying(process.ShowTotalAmount(), selectedLanguage, 3);
                                                    enterCoins = false;
                                                    entry = true;
                                                    //slots = 3;
                                                }
                                                else
                                                {
                                                    selectNumber = true;
                                                    Console.WriteLine(selectedLanguage[5]);
                                                    Thread.Sleep(500);
                                                    Console.WriteLine(selectedLanguage[6]);
                                                }
                                                break;

                                            case "--help":
                                            case "help":
                                                ShowHelpCommands(selectedLanguage);
                                                break;

                                            case "coins":
                                                selectNumber = false;
                                                break;

                                            default:
                                                Console.WriteLine(selectedLanguage[7]);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(selectedLanguage[8]);
                                    Thread.Sleep(500);
                                    Console.WriteLine(selectedLanguage[9]);
                                    Thread.Sleep(500);
                                }
                            }
                            catch (FormatException fe)
                            {
                                Console.WriteLine(selectedLanguage[7]);
                            }
                        }
                        else
                        {
                            if (coins.ToLower() == "return coins")
                            {
                                //coins being returns
                                Console.WriteLine(selectedLanguage[27]);
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("--------------------");
                                Thread.Sleep(2000);
                                entry = true;
                                break;
                            }

                            Console.WriteLine(selectedLanguage[7]);
                        }
                    }
                    else
                    {
                        Console.WriteLine(selectedLanguage[7]);
                    }
                }
            }
        }
    }
}
