using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class LoanManager : Loan // TODO: Make function for setting/updating loan properties.
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
    public readonly Dictionary<string, Loan> loanDict = new Dictionary<string, Loan>();

    public void CreateLoan(string clientName,
         int loanTerm,
         double loanAmount,
         double interestRate,
         double volatility,
         double longTermRate, 
         int curentMonth)
    {
        Loan loan = new();
        loan.ClientName = clientName;
        loan.LoanTerm = loanTerm;
        loan.InitialMonth = curentMonth;
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
    /// <param name="type">The type of the property to retrieve. Valid values are: typeof(double), typeof(int)</param>
    /// <param name="propertyName">The name of the property to retrieve. Valid values can be seen in <see cref="Loan"/>:
    /// <returns>The value of the specified property of the client's loan</returns>
    /// <exception cref="ArgumentException">Thrown when the client name is not found or the property name is invalid.</exception>
    public double GetLoanProperty(string clientName, Type type, string propertyName)
    {
        if (loanDict.TryGetValue(clientName, out Loan loan))
        {
            var property = typeof(Loan).GetProperty(propertyName);
            if (property != null)
            {
                if (type == typeof(double) && property.PropertyType == typeof(double))
                {
                    return (double)property.GetValue(loan);
                }
                else if (type == typeof(int) && property.PropertyType == typeof(int))
                {
                    return (int)property.GetValue(loan);
                }
                else
                {
                    throw new ArgumentException(propertyName + " is not a " + type.Name + "property of Loan");
                }
            }
            else throw new ArgumentException("Property: " + propertyName + "not found in loan");
            
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
