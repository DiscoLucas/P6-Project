using TMPro;
using UnityEngine;
public class EI_Information : actionsDisplay
{
    [SerializeField]
    TMP_Text header;
    [SerializeField]
    TMP_Text describtion;
    public override void fillOutDisplay()
    {
        header.text = aName;
        describtion.text =description;
        isDone = true;
    }
}
