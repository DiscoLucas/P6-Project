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
    [Tooltip("The amount that have been loaned")] internal double loanInitialAmount { get; set; }
    [Tooltip("The amount that have been loaned and is back")] internal double loanAmount { get; set; }
    [Tooltip("The first interest rate and what is used as a start for the simulation")] internal double interestRate { get; set; }
    [Tooltip("The voliatility of the loan (How much the price change)")] internal double volatility { get; set; }
    internal double longTermRate { get; set; }
    [Tooltip("The client that have the loan")] public ClientData clientData;
    [Tooltip("the length of the loan in months")] public int LoanTerm;
    [SerializeField][Tooltip("The month that the loan was started")] internal int initialMonth { get; set; }
    [SerializeField][Tooltip("The month that the loan currentPeriodStarted")] internal int periodStartMonth { get; set; }
    [Tooltip("[Not in use] The mount that still need to be payed")] internal double RemainingLoanAmount { get; set; }
    [Tooltip("How much the client pay per mounth ")] internal double MonthlyPayment { get; set; }

    [SerializeField]
    [Tooltip("The intersate rates that is calulatede over time ")] internal List<double> IRForTime = new List<double>();
    public List<double> IRPForTime = new List<double>();

    [Tooltip("Do the loan have installment")] public bool installment = false;
    public bool fixedIR = false;
    public bool lastPeriod = false;

    public LoanTypes loanTypes;
    public Loan(ClientData client,int LoanTerm, double loanAmount, double interestRate, double volatility, double longTermRate, int startMount, bool installment, LoanTypes loanTypes)
    {
        this.clientData = client;
        this.LoanTerm = LoanTerm;
        this.loanAmount = loanAmount;
        this.interestRate = interestRate;
        this.volatility = volatility;
        this.longTermRate = longTermRate;
        this.initialMonth = startMount;
        this.periodStartMonth = startMount;
        this.installment = installment;
        IRPForTime.Add(100);
        this.loanTypes = loanTypes;
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

    /// <summary>
    /// used to pay back some of the loan . the amount is the given variable
    /// </summary>
    /// <param name="amount"></param>
    public void payLoan(int amount) { 
        loanAmount -= amount;
    }

    /// <summary>
    /// Convert the loan into a different type
    /// </summary>
    /// <param name="currentMount"></param>
    /// <param name="LoanTerm"></param>
    /// <param name="interestRate"></param>
    /// <param name="volatility"></param>
    /// <param name="longTermRate"></param>
    public void convertLoan(int currentMount, int LoanTerm, double interestRate, double volatility,LoanTypes loanTypes) {
        periodStartMonth = currentMount;
        this.LoanTerm = LoanTerm;
        this.interestRate = interestRate;
        this.volatility = volatility;
        this.loanTypes = loanTypes;

    }


    public double getPriceRate() {
        return IRPForTime[IRPForTime.Count-1];
    }   


}
