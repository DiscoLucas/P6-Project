using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoanManager : Loan
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
        loan.clientName = clientName;
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


    #region Test implementations
    public void CreateTestLoanPeter()
    {
        loanAmount = 1000000;
        interestRate = 0.05;
        volatility = 0.01;
        longTermRate = 0.05;
        clientName = "Peter";
        loanTerm = 12;
        CreateLoan(clientName, loanTerm, loanAmount, interestRate, volatility, longTermRate);
    }
    public void CreateTestLoanLøve()
    {
        loanAmount = 3500000;
        interestRate = 0.03;
        volatility = 0.01;
        longTermRate = 0.05;
        clientName = "Løve";
        loanTerm = 24;
        CreateLoan(clientName, loanTerm, loanAmount, interestRate, volatility, longTermRate);
    }

    public void PeterTestPrint()
    {
        Debug.Log("Peter's interest rate is: " + GetLoanProperty("Peter", "interestRate"));
    }
    public void LøveTestPrint()
    {
        Debug.Log("Løve's interest rate is: " + GetLoanProperty("Løve", "interestRate"));
    }
    #endregion
}
