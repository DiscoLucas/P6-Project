using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "Action", menuName = "Actions/MaxLoanAction", order = 1)]
public class MaxLoanAction : ActionTemplate
{
    public float debtFactor = 4;
}
