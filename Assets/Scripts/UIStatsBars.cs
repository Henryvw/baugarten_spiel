using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsBars : MonoBehaviour
{
	Text moneyBar;

    // Start is called before the first frame update
    void Start()
    {
	    moneyBar = gameObject.GetComponent<Text>();
	    Debug.Log(moneyBar.text);
	//moneyBar = "hey";
	     //Debug.Log(moneyBar.textDisplay);
//
	     //moneyBar.textDisplay.text = EconomyManager.Instance.totalMoney;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	    moneyBar.text = EconomyManager.Instance.totalMoney.ToString();
        
    }
}
