using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loan : MonoBehaviour
{
    internal double loanAmount;
    internal double interestRate;
    internal double volatility;
    internal double longTermRate;
    internal string clientName;
    [Tooltip("the length of the loan in months")] internal int loanTerm;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void CreateLoan(string clientName, int loanTerm)
    {

    }
    
}
