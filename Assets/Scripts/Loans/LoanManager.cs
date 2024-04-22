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
        var instance = FindObjectOfType<LoanManager>();
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private Dictionary<string, Loan> loanDict = new Dictionary<string, Loan>();

    public double GetInterestRate(string clientName)
    {
        return loanDict[clientName].interestRate;
    }
}
