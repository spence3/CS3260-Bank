using System.Net.Mail;
namespace ConsoleApp1;

///<summary>
/// Enum which is in charge of declaring a bank account as
/// New, Active, UnderAudit, Frozen and Closed. A different
/// Account State is assigned to every account.
///</summary>
public enum AccountState 
{
    New, Active, UnderAudit, Frozen, Closed
}

///<summary>
/// Summary Interface contains the public functions
/// used inside of the Account class
///</summary>
interface IAccount
{
    //name set and get
    public bool SetName(string name);
    public string GetName();

    //address set and get
    public bool SetAddress(string inAddress);
    public string GetAddress();

    //funds set and get
    public bool PayInFunds(decimal amount);
    public bool WithdrawFunds(decimal amount);

    //balance set and get
    public bool SetBalance(decimal inBalance);
    public decimal GetBalance();

    public void SetState(AccountState state);
}

///<summary>
/// This is the Account class. It inherits from
/// IAccount and gets the methods from there
/// It defines methods and allows a user to store
/// their banking information
///</summary>
abstract class Account:IAccount
{ 
    //protected variables so child classes can use member variables of Account
    protected string name;
    protected string address;
    protected int serviceFee { get; set; }
    protected int minimumBalance { get; set; }
    protected string accountNumber { get; set; }
    protected static int nextAccountNumber = 1000;
    protected decimal balance;
    protected const decimal initialBalance = 0.0m;
    protected AccountState state;


    ///<summary>
    /// Purpose: Construct Account class variables
    ///</summary>
    /// No Params
    ///<returns>No return</returns>
    public string GetAccNum()
    {
        return this.accountNumber;
    }
    public Account()
    {
        this.balance = initialBalance;
        this.state = AccountState.New;

        //increase account number every time new checking, savings or CD is created
    }

    ///<summary>
    /// Purpose: Output custom string for user account
    ///</summary>
    /// No Params
    ///<returns>No Return</returns>
    public override string ToString()
    {
        return $"Account information for {name}: \n Address: {address} \n " +
               $"Account Number: {accountNumber} \n Current Balance: ${balance} \n " +
               $"State of account: {state}";
    }

     ///<summary>
     /// Purpose: To return true/false based on user input
     /// If user inputs null or no name return false. else return true
     ///</summary>
     /// Params: user input name
     ///<returns.acc /Returns a new accounbt created</returns>
    public bool SetName(string name)
    {
        if (name == null || name == "")
        {
            return false;
        }
        else
        {
            this.name = name;
            return true;
        }
    }

     ///<summary>
     /// Purpose: Get user name
     ///</summary>
     /// No params
     ///<returns>Returns user name</returns>
    public string GetName()
    {
        return this.name;
    }

     ///<summary>
     /// Purpose: Return true or false based on
     /// if user inputs address correctly.
     ///</summary>
     /// String user address
     ///<returns>True or falase</returns>
    public bool SetAddress(string inAddress)
    {
        if (inAddress == null || inAddress == "")
        {
            return false;
        }
        else
        {
            this.address = inAddress;
            return true;
        }
    }

    ///<summary>
    /// Purpose: Return user address
    ///</summary>
    /// No Params
    ///<returns>this.address</returns>
    public string GetAddress()
    {
        return this.address;
    }

     ///<summary>
     /// Purpose: input money into account balance
     ///</summary>
     /// params: user input amount. If amount is less than 0 return false else return true
     ///<returns>True or false</returns>
    public bool PayInFunds(decimal amount)
    {
        if (amount < 0)
        {
            return false;
        }
        else
        {
            this.balance += amount;//incrament balance by amount passed in
            return true;
        }
    }

     ///<summary>
     /// Purpose: Withdraw from account
     ///</summary>
     /// params: User input amounty
     ///<returns>return true or false based on correct user input</returns>
    public bool WithdrawFunds(decimal amount)
    {
        this.balance -= amount;//subtract amount first
        //if amount subtracted makes account balance neg-->false
        if (amount < 0 || this.balance < 0)
        {

            this.balance += amount; //put money back in account
            return false;
        }
        //returns true once balance is found to be positive
        return true;
    }

     ///<summary>
     /// Purpose: Set account balance
     ///</summary>
     /// params: Balance amount to set it to.
     ///<returns>Returns true or false based on user input corectness</returns>
    public bool SetBalance(decimal inBalance)
    {
        if (inBalance < 0)
        {
            return false;
        }//user inputs a balance less than expected minimum inital balance
        else if (inBalance < this.minimumBalance)
        {
            return false;
        }
        else
        {
            this.balance = inBalance;
            return true;
        }
    }

     ///<summary>
     /// Purpose: Gets account balance
     ///</summary>
     /// No Params
     ///<returns>Account balance amount</returns>
    public decimal GetBalance()
    {
        return this.balance;
    }

    ///<summary>
    /// Purpose: Set the account state (New, Active, UnderAudit, Frozen, Closed)
    ///</summary>
    /// No Params
    ///<returns>No return. Sets account state</returns>
    public void SetState(AccountState state)
    {
        this.state = state;
    }
}