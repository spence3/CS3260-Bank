namespace ConsoleApp1;
///<summary>
/// This is the AccountBank Class. It's in charge of storing
/// multiple accounts in a dictionary. user has the
/// ability to find a specific account based on the account
/// number provided by the user using a dictionary
///</summary>
class AccountBank
{
    private Dictionary<string, Account> accDict;
    private static int accCount;
    private readonly int MAX_ACCOUNTS;

    ///<summary>
    /// Purpose: Construct AccountBank dictionary
    ///</summary>
    /// Params: None
    ///<returns>No return</returns>
    public AccountBank()
    {
        accDict = new Dictionary<string, Account>();
    }

    ///<summary>
    /// Purpose: Stores user accounts in a dictionary
    ///</summary>
    /// Params: Account object
    ///<returns>true or false</returns>
    public bool StoreAccount(Account acc)
    {
        if (acc != null)
        {
            accDict[acc.GetAccNum()] = acc;
            return true;
        }
        //else
        return false;
    }

    ///<summary>
    /// Purpose: Finds account in account array
    ///</summary>
    /// Params: Account number
    ///<returns>Account object found</returns>
    public Account FindAccount(string accNum)
    {
        if (accDict.ContainsKey(accNum))
        {
            return accDict[accNum];
        }
        return null;//might need to change. Maybe do a try exception
    }
}