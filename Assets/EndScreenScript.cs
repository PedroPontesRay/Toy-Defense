using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenScript : MonoBehaviour
{
    [Header("Win or Defeat")]
    public Sprite[] sprites;
    public Image _imageComponent;

    [Header("QTN Deafeat and Damage")]
    public TextMeshProUGUI _txtEnemyDied;
    public TextMeshProUGUI _txtDamageGive;
    public TextMeshProUGUI _txtCurrentPlace;

    [Header("Names")]
    private string[] names;//TO.DO aplicar playfab
    public TextMeshProUGUI[] _txtComponents; 



    public void Open(int enemyDied,int damageGive,int winOrDefeat,int place)
    {
        _imageComponent.sprite = sprites[winOrDefeat];
        _txtEnemyDied.text = (enemyDied + " inimigos derrotados");
        _txtDamageGive.text = (damageGive + " de Dano causado");
        _txtCurrentPlace.text = ("Sua posição atual " + place +  "° lugar");


        gameObject.SetActive(true);
    }




}
