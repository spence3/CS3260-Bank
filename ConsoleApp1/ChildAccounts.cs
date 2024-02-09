using System.Text;

namespace ConsoleApp1;


///<summary>
/// This is the Savings clas which is a child class
/// of Account. Has an initial service fee of $0
/// An account balance minimum of $100 and increments
/// Account number when it's added so the next account
/// can have a unique #
///</summary>
class SavingsACcount: Account
{

    public readonly decimal INTEREST_RATE = 0.01m;
    
    
    ///<summary>
    /// Purpose: Construct SavingsAccount class variables
    ///</summary>
    /// No Params
    ///<returns>No return</returns>
    public SavingsACcount()
    {
        //Set account number and account type (Savings ,Checking CD)
        accountNumber = nextAccountNumber.ToString() + 'S';
        serviceFee = 0;
        minimumBalance = 100;
        nextAccountNumber++;
    }

    ///<summary>
    /// Purpose: Gets minimum balance
    ///</summary>
    /// No Params
    ///<returns>minimum balance required to start account</returns>
    public int GetMinimum()
    {
        return this.minimumBalance;
    }
    
    public override bool PayInFunds(decimal amount)
    {
        if (amount < 0)
        {
            return false;
        }
        else
        {
            this.balance += amount;//adding amount to balance first
            decimal interest = this.balance * INTEREST_RATE;//getting interest rate of amount entered
            this.balance += interest;
            return true;
        }
    }

}


///<summary>
/// This is the Checking clas which is a child class
/// of Account. Has an initial service fee of $5
/// An account balance minimum of $10 and increments
/// Account number when it's added so the next account
/// can have a unique #
///</summary>
class CheckingAccount : Account
{
    ///<summary>
    /// Purpose: Construct CheckingAccount class variables
    ///</summary>
    /// No Params
    ///<returns>No return</returns>
    public CheckingAccount()
    {
        //Set account number and account type (Savings ,Checking CD)
        accountNumber = nextAccountNumber.ToString() + 'C';
        serviceFee = 5;
        minimumBalance = 100;
        nextAccountNumber++;
    }

    ///<summary>
    /// Purpose: Gets minimum balance
    ///</summary>
    /// No Params
    ///<returns>minimum balance required to start account</returns>
    public int GetMinimum()
    {
        return this.minimumBalance;
    }
    
    public override bool WithdrawFunds(decimal amount)
    {
        const int CHARGE_FEE = 5;
        this.balance -= amount;
        //Do not allow a withdrawal if the additional service fee added to the withdrawal amount would cause the balance to drop below $0.
        if (this.balance - CHARGE_FEE < 0)
        {
            this.balance += amount; //put money back.
            return false;
        }
        
        //balance below minimum --> add charge fee
        else if (this.balance < minimumBalance && this.balance - CHARGE_FEE >= 0)
        {
            this.balance -= CHARGE_FEE;
            return true;
        }
        //amount to be withdrawn negative.. put money back in account
        else if (this.balance < 0)
        {
            this.balance += amount;
            return false;
        }

        return true;
    }
}

///<summary>
/// This is the CDAccount clas which is a child class
/// of Account. Has an initial service fee of $8
/// An account balance minimum of $500 and increments
/// Account number when it's added so the next account
/// can have a unique #
///</summary>
class CDAccount : Account
{
    ///<summary>
    /// Purpose: Construct CDAccount class variables
    ///</summary>
    /// No Params
    ///<returns>No return</returns>
    public CDAccount()
    {
        //Set account number and account type (Savings ,Checking CD)
        accountNumber = nextAccountNumber.ToString() + 'D';
        serviceFee = 8;
        minimumBalance = 500;
        nextAccountNumber++;
    }

    ///<summary>
    /// Purpose: Gets minimum balance
    ///</summary>
    /// No Params
    ///<returns>minimum balance required to start account</returns>
    public int GetMinimum()
    {
        return this.minimumBalance;
    }
}

