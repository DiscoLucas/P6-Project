using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailSystem : MonoBehaviour
{
    public Mail mail;
    public RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
