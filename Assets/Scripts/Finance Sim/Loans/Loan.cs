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
    [Tooltip("The remaining amount of the loan")] internal double loanAmount { get; set; }
    public float debtAmount = 0;
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
    [Tooltip("The interest rates that are calulated over time. " +
        "4 steps are generated every month")] internal List<double> IRForTime = new List<double>();
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
        IRPForTime.Add((interestRate/20)+95); // wtf are these magic numbers? -Mitchell
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
    public void payLoan() 
    { 
        double monthlyPayment = CalculateMonthlyPayment();
        loanAmount -= monthlyPayment;
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

    /// <summary>
    /// Calculates the monthly payment of the loan
    /// </summary>
    /// <returns>The updated <see cref="MonthlyPayment"/></returns>
    private double CalculateMonthlyPayment()
    {
        double monthlyInterestRate;
        if (fixedIR || IRForTime.Count == 0)
        {
            monthlyInterestRate = interestRate / 12; // if it's fixed or if there are no interest rates, use the initial interest rate
        }
        else
        {
            double currentInterestRate = IRForTime[IRForTime.Count - 1]; // Get the latest interest rate
            monthlyInterestRate = currentInterestRate / 12; // Calculate the monthly interest rate
        }

        // calculate the monthly payment using the loan formula
        double monthlyPayment;
        double loanTermInMonths = LoanTerm;
        if (monthlyInterestRate > 0)
        {
            monthlyPayment = loanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -loanTermInMonths));
            Debug.Log(clientData.clientName + " paid of " + monthlyPayment + " of their loan, with a POSITVE interest");
        }
        else
        {
            monthlyPayment = loanAmount / loanTermInMonths;
            Debug.Log(clientData.clientName + " paid of " + monthlyPayment + " of their loan, with a NEGATIVE interest");
        }
        return MonthlyPayment = monthlyPayment;
    }

}
