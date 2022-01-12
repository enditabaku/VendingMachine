using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineConsoleApp
{
    class BuyingProcess
    {
        public double TotalAmount { set; get; }
        public double LeftAmount { get; set; }
        public double CurrentProductPrice { get; set; }
        public BuyingProcess(double enteredAmount, double currentProductPrice = 0.00)
        {
            TotalAmount = enteredAmount;
            LeftAmount = enteredAmount;
            CurrentProductPrice = currentProductPrice;
        }
        public void ShowTotalAmountEntered()
        {
            Console.WriteLine("Amount entered: " + TotalAmount + " Euro");
        }
        public void ShowTotalAmountLeft()
        {

        }
        public void AmountDeduction()
        {
            TotalAmount = TotalAmount - CurrentProductPrice;
        }
        public bool HasEnoughMoney()
        {
            if (LeftAmount > CurrentProductPrice)
            {
                return true;
            }
            return false;
        }
    }

}

