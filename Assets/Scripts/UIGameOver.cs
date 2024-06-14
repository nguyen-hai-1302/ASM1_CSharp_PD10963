using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    ASM_MN asm;
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        asm = FindObjectOfType<ASM_MN>();
        //asm = gameObject.AddComponent<ASM_MN>();
    }

    private void Start()
    {
        scoreText.text = "You Scored:\n" + ScoreKeeper.Instance.GetScore();
        
        ASM_MN.Instance.YC1();        
        ASM_MN.Instance.YC2();
        ASM_MN.Instance.YC3();
        ASM_MN.Instance.YC4();
        ASM_MN.Instance.YC5();
        ASM_MN.Instance.YC6();
        ASM_MN.Instance.YC7();
    }

    


}
