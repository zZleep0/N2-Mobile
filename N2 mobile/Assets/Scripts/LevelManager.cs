using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] private SoundManager soundManager;

    [SerializeField] private GameObject pnlVitoria;

    [SerializeField] public GameObject pnlInteracao;
    public TextMeshProUGUI txtInteracao;

    public PlayerController playerController;

    public List<GameObject> itens;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sim()
    {
        playerController.SairInteracao();
    }

    public void Nao()
    {
        playerController.SairInteracao();
    }

    public void Vitoria()
    {
        //soundManager.PlaySound(SoundManager.SoundType.TypeVictory);
        pnlVitoria.SetActive(true);
    }
}
