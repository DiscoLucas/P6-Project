using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is used to store the property values of a loan.
/// </summary>
public class Loan
{
    internal double loanAmount { get; set; }
    internal double interestRate { get; set; }
    internal double volatility { get; set; }
    internal double longTermRate { get; set; }
    internal string ClientName { get; set; }
    [Tooltip("the length of the loan in months")] internal int loanTerm { get; set; }
    internal double RemainingLoanAmount { get; set; }
    internal double MonthlyPayment { get; set; }
    internal List<double> IRForTime { get; set; }

    
}
