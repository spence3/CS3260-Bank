namespace ConsoleApp1;


///<summary>
/// This is the AccountBank Class. It's in charge of storing
/// multiple accounts in an array. If it max's out, the account
/// will return false, else it adds an account. You are also
/// able to find a specific account based on the account
/// number provided by the user.
///</summary>
class AccountBank
{
    //using a list so we can append. Treat it like an array
    private List<Account> accArray;
    private static int accCount;
    private readonly int MAX_ACCOUNTS;

    ///<summary>
    /// Purpose: Construct AccountBank class variables
    ///</summary>
    /// Params: number of accounts user wants to create passed in by user
    ///<returns>No return</returns>
    public AccountBank(int numAccounts)
    {
        //An array of Account
        accArray = new List<Account>();
        MAX_ACCOUNTS = numAccounts;
    }

    ///<summary>
    /// Purpose: Stores user accounts in an array
    ///</summary>
    /// Params: Account object
    ///<returns>true or false</returns>
    public bool StoreAccount(Account acc)
    {
                            //if account array is full, return false
        if (acc == null || this.accArray.Count >= MAX_ACCOUNTS)
        {
            return false;
        }
        else
        {
            accArray.Add(acc);
            return true;
        }
    }

    ///<summary>
    /// Purpose: Finds account in account array
    ///</summary>
    /// Params: Account number
    ///<returns>Account object found</returns>
    public Account FindAccount(string accNum)
    {
        foreach (var account in accArray)
        {
            if(account.GetAccNum() == accNum)
            {
                return account;
            }
        }
        return null;
    }

    ///<summary>
    /// Purpose: Helper method to print account objects.
    ///</summary>
    /// Params: Index number. Number of accounts will incrrease
    /// and will allow the user to print all the accounts and
    /// their information.
    ///<returns>No return</returns>
    public Account PrintAccounts(int index)
    {
        return this.accArray[index];
    }
}