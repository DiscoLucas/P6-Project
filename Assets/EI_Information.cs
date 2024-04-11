using TMPro;
using UnityEngine;
public class EI_Information : EventInterface
{
    [SerializeField]
    TMP_Text header;
    [SerializeField]
    TMP_Text describtion;
    public override void fillOutInterface()
    {
        header.text = yeEvent.aName;
        describtion.text = yeEvent.description;
        yeEvent.isDone = true;
    }
}
