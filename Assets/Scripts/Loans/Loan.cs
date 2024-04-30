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
    internal double loanAmount { get; set; }
    internal double interestRate { get; set; }
    internal double volatility { get; set; }
    internal double longTermRate { get; set; }
    public ClientData clientData;
    [Tooltip("the length of the loan in months")] public int LoanTerm;
    [Tooltip("The month that the loan was started")] internal int initialMonth { get; set; }
    internal double RemainingLoanAmount { get; set; }
    internal double MonthlyPayment { get; set; }

    [SerializeField]
    internal List<double> IRForTime = new List<double>();

    public bool installment = false;

    /*
     loan.ClientName = clientName;
        loan.LoanTerm = loanTerm;
        loan.InitialMonth = curentMonth;
        loan.loanAmount = loanAmount;
        loan.interestRate = interestRate;
        loan.volatility = volatility;
        loan.longTermRate = longTermRate;
     */
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
    public List<double> getInterestRate() {
        return IRForTime;
    }

    public double getFirstInterestRate() {
        return interestRate;
    }

    
}
