using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProps : MonoBehaviour
{
    [SerializeField] public int itemID;
    public int requiredItemID = -1; // ID do item necessário para coletar (-1 significa nenhum pré-requisito)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
