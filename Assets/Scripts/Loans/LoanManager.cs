using System;
using System.Collections.Generic;

public class LoanManager : Loan // TODO: Make function for setting/updating loan properties.
{
    /*
    void Awake()
    {
        LowTierGod();
        
    }

    /// <summary>
    /// Committing soduku if the LoanManager already exists
    /// </summary>
    private void LowTierGod()
    {
        if (FindObjectsOfType<LoanManager>().Length > 1)
        {
            Destroy(gameObject);
        }
    }*/
    /// <summary>
    /// Dictionary to store loans, with the client name as the key
    /// </summary>
    private Dictionary<string, Loan> loanDict = new Dictionary<string, Loan>();

    public void CreateLoan(string clientName,
         int loanTerm,
         double loanAmount,
         double interestRate,
         double volatility,
         double longTermRate)
    {
        Loan loan = new();
        loan.ClientName = clientName;
        loan.loanTerm = loanTerm;
        loan.loanAmount = loanAmount;
        loan.interestRate = interestRate;
        loan.volatility = volatility;
        loan.longTermRate = longTermRate;

        loanDict[clientName] = loan;
    }

    /// <summary>
    /// Retrieves a property of a given client's loan
    /// </summary>
    /// <param name="clientName"></param>
    /// <param name="propertyName">The name of the property to retrieve. Valid values are: 
    /// "interestRate", "volatility", "longTermRate", "loanAmount".</param>
    /// <returns>The value of the specified property of the client's loan</returns>
    /// <exception cref="ArgumentException">Thrown when the client name is not found or the property name is invalid.</exception>
    public double GetLoanProperty(string clientName, string propertyName)
    {
        if (loanDict.TryGetValue(clientName, out Loan loan))
        {
            var property = typeof(Loan).GetProperty(propertyName);
            if (property != null && property.PropertyType == typeof(double))
            {
                return (double)property.GetValue(loan);
            }
            else
            {
                throw new ArgumentException(propertyName + " is not a double property of Loan");
            }
        }
        else throw new ArgumentException("No loan found for client: " + clientName);
    }
    public List<double> GetIRHistory(string clientName)
    {
        if (loanDict.TryGetValue(clientName, out Loan loan))
        {
            return loan.IRForTime;
        }
        else throw new ArgumentException("No loan found for client: " + clientName);
    }

    /// <summary>
    /// Add new interest rates to a client's loan
    /// </summary>
    /// <param name="clientName"></param>
    /// <param name="newInterest"></param>
    /// <exception cref="ArgumentException"></exception>
    public void UpdateIR(string clientName, double[] newInterest)
    {
        if (loanDict.TryGetValue(clientName, out Loan loan))
        {
            // Add each new interest rate to the loan's IRForTime list
            foreach (double ir in newInterest)
            {
                loan.IRForTime.Add(ir);
            }
        }
        else throw new ArgumentException("No loan found for client: " + clientName);
    }
    
}
