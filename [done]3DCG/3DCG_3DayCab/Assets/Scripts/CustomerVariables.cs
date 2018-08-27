using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerVariables : MonoBehaviour {

    public static CustomerVariables CusVars;

    public GameObject C1_Button;
    public GameObject C2_Button;
    public GameObject C3_Button;
    public GameObject C4_Button;

    [Header("Customer1(BUsinessMan) Variables")] //businessman
    public int Customer1Pay_Min = 300;
    public int Customer1Pay_Max = 500;
    public int Customer1ExtraTips_Min = 50;
    public int Customer1ExtraTips_Max = 100;
    public int Customer1GoodEndPay = 2500;
    public int C1_DisplayedPay;//set the price before customer selection

    [Header("Customer2(FStudent) Variables")] //female Student
    public int Customer2Pay_Min = 200;
    public int Customer2Pay_Max = 400;
    public int Customer2ExtraTips_Min = 10;
    public int Customer2ExtraTips_Max = 80;
    public int Customer2GoodEndPay = 2000;
    public int C2_DisplayedPay;//set the price before customer selection

    [Header("Customer3(Gramps) Variables")] //old gramps
    public int Customer3Pay_Min = 100;
    public int Customer3Pay_Max = 300;
    public int Customer3ExtraTips_Min = 70;
    public int Customer3ExtraTips_Max = 150;
    public int Customer3GoodEndPay = 3000;
    public int C3_DisplayedPay;//set the price before customer selection

    [Header("Customer4(PartTimer) Variables")] //part-timer
    public int Customer4Pay_Min = 300;
    public int Customer4Pay_Max = 500;
    public int Customer4ExtraTips_Min = 30;
    public int Customer4ExtraTips_Max = 50;
    public int Customer4GoodEndPay = 2000;
    public int C4_DisplayedPay;//set the price before customer selection

    [Header("Customer5(secret) Variables")] //???
    public bool canAppear = false;

    // Use this for initialization
    void Awake () {
        CusVars = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //customer selection functions + set price per ride
    public void CustomersPriceSet()
    {
        //randomize values for each customer's pay
        Text C1_RandomPayText = C1_Button.GetComponentInChildren<Text>();
        C1_DisplayedPay = Random.Range(Customer1Pay_Min, Customer1Pay_Max + 1);
        C1_RandomPayText.text = "$ " + C1_DisplayedPay.ToString();

        Text C2_RandomPayText = C2_Button.GetComponentInChildren<Text>();
        C2_DisplayedPay = Random.Range(Customer2Pay_Min, Customer2Pay_Max + 1);
        C2_RandomPayText.text = "$ " + C2_DisplayedPay.ToString();

        Text C3_RandomPayText = C3_Button.GetComponentInChildren<Text>();
        C3_DisplayedPay = Random.Range(Customer3Pay_Min, Customer3Pay_Max + 1);
        C3_RandomPayText.text = "$ " + C3_DisplayedPay.ToString();

        Text C4_RandomPayText = C4_Button.GetComponentInChildren<Text>();
        C4_DisplayedPay = Random.Range(Customer4Pay_Min, Customer4Pay_Max + 1);
        C4_RandomPayText.text = "$ " + C4_DisplayedPay.ToString();
    }
}
