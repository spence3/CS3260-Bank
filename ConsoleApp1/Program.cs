// See https://aka.ms/new-console-template for more information
// Project Prologue
//
// Name: Spencer Miller
// Class: CS3260 Section X01
// Project: Lab_04
// Date: 2/2/24
// Purpose: This lab is meant to allow the user to pick what kind of account they want
//          based on input (c for Checking, s for Savings and d for Certificate of Deposit).
//          The user must put in the minimum balance for each account (checking $10, savings $100
//          CD $500). If there are multiple accounts we have a class that handles that called AccountBank
//          Where however many accounts the user wants to store is in there. If the user wants to store 3
//          accounts then 3 accounts and no more than 3 accounts will be stored within an array in AccountBank
//          We are able to also look up accounts based on the account number and return that account.
//
// I declare that the following code was written by me or provided
// by the instructor for this project. I understand that copying source
// code from any other source constitutes plagiarism, and that I will receive
// a zero on this project if I am found in violation of this policy.
// ---------------------------------------------------------------------------
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using ConsoleApp1;


///<summary>
/// This is the Program Class where Everything is tested
/// </summary>
class Program
{
    ///<summary>
    /// The main program where all the functionality is held
    /// to test the program
    ///</summary>
    static void Main()
    {
        
         // creating account(s)
         Console.WriteLine("How many accounts?");
         string numAccounts = Console.ReadLine();
         
         while(!int.TryParse(numAccounts, out int result))
         {
             Console.WriteLine("Please enter a number");
             numAccounts = Console.ReadLine();
         }
         
         AccountBank accounts = new AccountBank(int.Parse(numAccounts));

         for (int i = 0; i < int.Parse(numAccounts); i++)
         {
             Account newAcc = CreateAccount();
             accounts.StoreAccount(newAcc);
         }

         //printing account(s)
         for (int i = 0; i < int.Parse(numAccounts); i++)
         {
             Console.WriteLine($"\n{accounts.PrintAccounts(i)}\n");
         }
    }


    ///<summary>
    /// Purpose: Deposit or Withdraw from account.
    ///</summary>
    /// Parmams: account object
    ///<returns>Write to console amount withdrawn or deposited, update bank balance</returns>
    static void DepositWithdraw(Account acc)
    {
        Console.WriteLine("Input w for withdraw or d for deposit. Input e or return for exit");
        string depositOrWithdraw = Console.ReadLine();

        while (depositOrWithdraw.ToLower() != "e")
        {
            //withdraw handling
            if (depositOrWithdraw == "w")
            {
                Console.WriteLine("How much would you like to withdraw?");
                string withdrawAmmount = Console.ReadLine();

                //check if input is an int. If not run loop again.
                if (!int.TryParse(withdrawAmmount, out int result))
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }

                if (!acc.WithdrawFunds(int.Parse(withdrawAmmount)))
                {//exceeds balance
                    Console.WriteLine($"${withdrawAmmount} will exceed $0.0 ");
                }
                else//didn't exceed balance. Print new balanc
                {
                    Console.WriteLine($"Your new balance: ${acc.GetBalance()}");
                }

                //deposit handling
            }else if(depositOrWithdraw == "d")
            {
                Console.WriteLine("How much would you like to deposit?");
                string depositAmmount = Console.ReadLine();

                //check if input is an int. If not run loop again.
                if (!int.TryParse(depositAmmount, out int result))
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }

                if (!acc.PayInFunds(int.Parse(depositAmmount)))
                {//if the user doesn't put positive for deposit
                    Console.WriteLine("Deposit must be greater than 0");
                }
                else
                {//if user inputs correctly. Output balance
                    Console.WriteLine($"Your new balance: ${acc.GetBalance()}");
                }
            }else
            {//user input is incorrect
                Console.WriteLine("Please input the correct information");
            }
            Console.WriteLine("Enter w for withdraw or d for deposit. Enter e for exit");
            depositOrWithdraw = Console.ReadLine();
        }
    }


    ///<summary>
    /// Purpose: To create an Account type based in input
    /// c for checking, s for savings, or d for CD account
    ///</summary>
    /// Parmams: No params
    ///<returns>Returns Account based on type (checking, savings, CD)</returns>
    static Account AccountType()
    {
        Console.WriteLine("What kind of account do you want? enter c for checking" +
                          "s for savings or d for certificate of deposit");

        string userInput = Console.ReadLine();
        while (true)
        {
            if (userInput.ToLower() == "c")
            {
                CheckingAccount checking = new CheckingAccount();
                Console.WriteLine("how much do you want to put into your account?");
                string initBalance = Console.ReadLine();
                //check if input is an int. If not run loop again.
                if (!int.TryParse(initBalance, out int result))
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }
                if (!checking.SetBalance(int.Parse(initBalance)))
                {
                    Console.WriteLine($"Should be at least ${checking.GetMinimum()}");
                    continue;
                }

                return checking;
            }else if(userInput.ToLower() == "s")
            {
                SavingsACcount savings = new SavingsACcount();
                Console.WriteLine("how much do you want to put into your account?");
                string initBalance = Console.ReadLine();
                //check if input is an int. If not run loop again.
                if (!int.TryParse(initBalance, out int result))
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }
                if (!savings.SetBalance(int.Parse(initBalance)))
                {
                    Console.WriteLine($"Should be at least ${savings.GetMinimum()}");
                    continue;
                }
                return savings;

            }else if (userInput.ToLower() == "d")
            {

                CDAccount cdAccount = new CDAccount();
                Console.WriteLine("how much do you want to put into your account?");
                string initBalance = Console.ReadLine();
                //check if input is an int. If not run loop again.
                if (!int.TryParse(initBalance, out int result))
                {
                    Console.WriteLine("Please enter a number");
                    continue;
                }
                if (!cdAccount.SetBalance(int.Parse(initBalance)))
                {
                    Console.WriteLine($"Should be at least ${cdAccount.GetMinimum()}");
                    continue;
                }
                return cdAccount;
            }
            else
            {
                Console.WriteLine("Enter c for checking s for savings or d for certificate of deposit");
                userInput = Console.ReadLine();
            }
        }

    }

    ///<summary>
    /// Purpose: To create a new account based on user input
    ///</summary>
    /// No Params
    ///<returns.acc /Returns a new accounbt created</returns>

    static Account CreateAccount()
    {
        Account acc = AccountType();


        while (true)
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            if(!(acc.SetName(name))){
                Console.WriteLine("Please enter your name");
                continue;
            }
            Console.WriteLine("Enter an address: ");
            string address = Console.ReadLine();
            if(!acc.SetAddress(address))
            {
                Console.WriteLine("Please enter your address");
                continue;
            }
            return acc;
        }
    }
}
