using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Loan Template", menuName = "Loans/LoanTemplate", order = 0)]

public class LoanTemplate : ScriptableObject
{
    [Header("Loan Info")]
    public string loanID;
    public string Issuer;

    [Header("Loan financial info")]
    public float faceValue;
    public float interestRate;
    public string recieveDate = "yyyy-MM-dd";
    public string expirationDate = "yyyy-MM-dd";
    public float yieldValue;
}
