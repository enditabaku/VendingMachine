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
        public double CurrentProductPrice { get; set; }
        public BuyingProcess(double enteredAmount, double currentProductPrice = 0.00)
        {
            TotalAmount = TotalAmount + enteredAmount;
            CurrentProductPrice = currentProductPrice;
        }
        public double ShowTotalAmount()
        {
            TotalAmount = Math.Round(TotalAmount, 2);
            return TotalAmount;
        }
        public void AmountDeduction()
        {
            TotalAmount = TotalAmount - CurrentProductPrice;
        }
        public void StartAgain()
        {
            TotalAmount = 0;
        }
        public bool HasEnoughMoney()
        {
            if (TotalAmount > CurrentProductPrice)
            {
                return true;
            }
            return false;
        }
    }

}

