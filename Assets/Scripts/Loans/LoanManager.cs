using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoanManager : Loan
{
    // Start is called before the first frame update
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
    }
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
        Loan loan = new Loan();
        loan.clientName = clientName;
        loan.loanTerm = loanTerm;
        loan.loanAmount = loanAmount;
        loan.interestRate = interestRate;
        loan.volatility = volatility;
        loan.longTermRate = longTermRate;

        loanDict[clientName] = loan;
    }
    public double GetInterestRate(string clientName)
    {
        if (loanDict.TryGetValue(clientName, out Loan loan))
        {
            return loan.interestRate;
        }
        return loanDict[clientName].interestRate;
    }

    
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
        Debug.Log("Peter's interest rate is: " + GetInterestRate("Peter"));
    }
    public void LøveTestPrint()
    {
        Debug.Log("Løve's interest rate is: " + GetInterestRate("Løve"));
        Debug.Log("Løve's loan amount is: " + loanDict["Løve"].loanAmount);
        Debug.Log("Løve's loan term is: " + loanDict["Løve"].loanTerm);
        
    }
}
