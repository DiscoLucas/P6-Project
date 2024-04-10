using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EI_Information : EventInterface
{
    [SerializeField]
    TMP_Text header;
    [SerializeField]
    TMP_Text describtion;
    public override void fillOutInterface()
    {
        header.text = ye_event.name;
        describtion.text = ye_event.describtion;
        ye_event.is_done = true;
    }
}
