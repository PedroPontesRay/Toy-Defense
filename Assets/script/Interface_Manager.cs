using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interface_Manager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveTxt;

    public Spawn spawnSP;

    private void Start()
    {
        spawnSP = GetComponent<Spawn>();    

    }
    //Funcao para atualizar a wave

    // Update is called once per frame
    void Update()
    {
        
    }
}
