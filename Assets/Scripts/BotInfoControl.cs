using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BotInfoControl : MonoBehaviour
{
    [SerializeField] private TextMeshPro BotName;
    [SerializeField] private TextMeshPro BotOrder;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBotName(string name)
    {
        BotName.text = name;
    }

    public void SetBotOrder(string order)
    {
        BotOrder.text = order;
    }
}
