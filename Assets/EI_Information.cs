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
        header.text = yeEvent.aName;
        describtion.text = yeEvent.description;
        yeEvent.isDone = true;
    }
}
