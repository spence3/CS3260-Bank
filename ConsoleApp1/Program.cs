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
        
        Console.WriteLine("Lets create some accounts.");
        AccountBank accounts = new AccountBank();
        CreateMultipleAccounts(accounts);
    }
    
    static void CreateMultipleAccounts(AccountBank accounts)
    {
        while (true)
        {
            Console.WriteLine("Enter q to quit or any other button to create an account");
            string usrInput = Console.ReadLine();
            //allow user to quit before making an account
            if (usrInput.ToLower() == "q")
            {
                break;
            }
            //else
            Account newAccount = CreateAccount();
            accounts.StoreAccount(newAccount);
        }
        
        Console.WriteLine("If you would like to find an account press 'y'. If you want to quit press any other button.");
        string findAccount = Console.ReadLine();
        if (findAccount == "y")
        {
            AccountSelection(accounts);
        }
    }

    static void AccountSelection(AccountBank accounts)
    {
        Console.WriteLine("Lets find some accounts");
        Console.WriteLine("Enter q to quit or an account number to find account.");
        while (true)
        {
            string accNum = Console.ReadLine();
            if (accNum == "q")
            {
                break;
            }
            
            Account selectedAcc = accounts.FindAccount(accNum);
            //non-existing account
            if (selectedAcc == null)
            {
                Console.WriteLine("Not a correct input. Try another account number or q to quit");
                continue;
            }
            //found account
            else
            {
                AccountProcessing(selectedAcc);
            }
            Console.WriteLine("Enter another account number to find another account or q to quit");
        }
        //go back to creating multiple accounts
        Console.WriteLine("If you would like to create another account press 'y'. If you want to quit press any other button.");
        string usrInput = Console.ReadLine();
        if (usrInput == "y")
        {
            CreateMultipleAccounts(accounts);
        }
        
    }

    ///<summary>
    /// Purpose: Deposit or Withdraw from account.
    ///</summary>
    /// Parmams: account object
    ///<returns>Write to console amount withdrawn or deposited, update bank balance</returns>
    static void AccountProcessing(Account acc)
    {
        if (acc is CDAccount)
        {
            Console.WriteLine("CD accounts can only withdraw. Enter w for withdraw or e to exit");
        }
        else
        {
            Console.WriteLine("Input w for withdraw or d for deposit. Enter e to exit");
        }

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
                    Console.WriteLine($"${withdrawAmmount} will exceed $0.0... Remember there is a service" +
                                      $"fee of $5 if your account balance is below $100");
                }
                else//didn't exceed balance. Print new balanc
                {
                    Console.WriteLine($"Your new balance: ${acc.GetBalance()}");
                }

                //deposit handling
            }
            else if(depositOrWithdraw == "d" && acc is not CDAccount)
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

            if (acc is CDAccount)
            {
                Console.WriteLine("Enter w for withdraw or e to exit\"?");
            }
            else
            {
                Console.WriteLine("Enter w for withdraw or d for deposit. Enter e for exit");
            }

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
