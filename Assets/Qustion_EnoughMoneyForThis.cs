using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Qustion_EnoughMoneyForThis : Qustion_FeildBoolean
{
    [Header("House")]
    public HouseData[] houses;
    public HouseData house;
    public RawImage houseImage;
    public TMP_Text name, sqm, adres, price;
    public override void fillOutHeaderAndDescribtion()
    {
        base.fillOutHeaderAndDescribtion();
        house = houses[UnityEngine.Random.Range(0, houses.Length)];
        house.getDescribtion(name, sqm, adres, price, houseImage);
    }
    public override void calcCorrectAnswer()
    {
        float loanMax = client.Finance.monthlyIncome * 12 * client.Finance.debtFactor + client.Finance.totalSavings;
        correctAnswer = (loanMax >= house.price);
        
    }
}

[Serializable]
public class HouseData {
    public string name;
    public float sqm;
    public string addresse;
    public int houseNumber = 0;
    public float price;
    public Sprite houseImage;

    public void getDescribtion(TMP_Text nameT, TMP_Text sqmT, TMP_Text adres, TMP_Text priceT, RawImage image) {
        if (houseNumber <= 0)
            houseNumber = UnityEngine.Random.Range(1, 30);
        nameT.text = name;
        sqmT.text = sqm + "Kvadrat meter";
        adres.text = adres.text + houseNumber;
        priceT.text = price + " KR";
        image.texture = houseImage.texture;
    }
}
