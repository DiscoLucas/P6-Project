using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loan // TODO: remove MonoBehaviour
{
    internal double loanAmount { get; set; }
    internal double interestRate { get; set; }
    internal double volatility { get; set; }
    internal double longTermRate { get; set; }
    internal string clientName { get; set; }
    [Tooltip("the length of the loan in months")] internal int loanTerm { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    /*public double GetIR()
    {
        return interestRate;
    }*/
    /*
    public void GetLøveIR()
    {
        Debug.Log("Løve's interest rate is: " + loanDict["Løve"].interestRate);
    }*/

}
