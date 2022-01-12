using System;

namespace VendingMachineConsoleApp
{
    class UnpredictedInputException : Exception
    {
        public UnpredictedInputException()
        {
            Console.WriteLine("Your input is not one of the predicted inputs. Please read the text above again and make sure you enter an accepted input");
        }
    }
}
