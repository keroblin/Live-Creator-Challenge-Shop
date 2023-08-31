using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable : MonoBehaviour
{
    public GameObject visuals;
    public Speech speech;
    public Item item;
    public int failedBuyIndex;
    public int succeededBuyIndex;
    public void EvaluatePurchase()
    {
        if(Manager.Instance.Buy(item)) 
        {
            speech.SwitchChapter(succeededBuyIndex);
            visuals.SetActive(false);

        }
        else
        {
            speech.SwitchChapter(failedBuyIndex);
        }
    }
}
