using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is used to store the property values of a loan.
/// </summary>
[Serializable]
public class Loan
{
    [SerializeField]
    [Tooltip("The amount that have been loaned")] internal double loanAmount { get; set; }
    [Tooltip("The first interest rate and what is used as a start for the simulation")] internal double interestRate { get; set; }
    [Tooltip("The voliatility of the loan (How much the price change)")] internal double volatility { get; set; }
    internal double longTermRate { get; set; }
    [Tooltip("The client that have the loan")] public ClientData clientData;
    [Tooltip("the length of the loan in months")] public int LoanTerm;
    [Tooltip("The month that the loan was started")] internal int initialMonth { get; set; }
    [Tooltip("[Not in use] The mount that still need to be payed")] internal double RemainingLoanAmount { get; set; }
    [Tooltip("[Not in use] How much the client pay per mounth ")] internal double MonthlyPayment { get; set; }

    [SerializeField]
    [Tooltip("The intersate rates that is calulatede over time ")] internal List<double> IRForTime = new List<double>();

    [Tooltip("Do the loan have installment")] public bool installment = false;
    public Loan(ClientData client,int LoanTerm, double loanAmount, double interestRate, double volatility, double longTermRate, int startMount,bool installment)
    {
        this.clientData = client;
        this.LoanTerm = LoanTerm;
        this.loanAmount = loanAmount;
        this.interestRate = interestRate;
        this.volatility = volatility;
        this.longTermRate = longTermRate;
        this.initialMonth = startMount;
        this.installment = installment;
    }
    /// <summary>
    /// Return all the interest rate over time
    /// </summary>
    /// <returns></returns>
    public List<double> getInterestRate() {
        return IRForTime;
    }

    /// <summary>
    /// Return the first interestrate
    /// </summary>
    /// <returns></returns>
    public double getFirstInterestRate() {
        return interestRate;
    }

    
}
