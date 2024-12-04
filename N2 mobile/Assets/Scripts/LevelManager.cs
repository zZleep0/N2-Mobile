using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;

    [SerializeField] private GameObject pnlVitoria;

    public GameObject botoes;

    [SerializeField] public GameObject pnlInteracao;
    public TextMeshProUGUI txtInteracao;
    public int item;

    public PlayerController playerController;

    [SerializeField] private List<Image> imagensInventario; // Lista das imagens no Canvas


    // Start is called before the first frame update
    void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizarTextoInteracao(string texto)
    {
        txtInteracao.text = texto;
        //Debug.Log($"Texto de intera��o atualizado para: {texto}");
    }

    public void AtualizarImagemInventario(ItemProps itemProp)
    {
        // Procura pela primeira imagem vazia no invent�rio
        foreach (Image imagem in imagensInventario)
        {
            if (imagem.sprite == null) // Se a imagem est� vazia
            {
                // Define o sprite do item na imagem
                imagem.sprite = itemProp.gameObject.GetComponent<SpriteRenderer>().sprite;
                Debug.Log($"Imagem atualizada com o item: {itemProp.itemID}");
                return; // Sai do loop ap�s atualizar
            }
        }
    }


    public void Sim()
    {

        soundManager.PlaySound(SoundManager.SoundType.TypeBTN);

        AtualizarTextoInteracao("Item " + item + " coletado! Continue explorando.");

        playerController.ColetarItemAtual();

        botoes.SetActive(false);

        playerController.canMove = true;
    }

    public void Nao()
    {
        soundManager.PlaySound(SoundManager.SoundType.TypeBTN);

        // Atualiza o texto para refletir que a intera��o foi cancelada
        AtualizarTextoInteracao("Intera��o cancelada.");

        botoes.SetActive(false);
        pnlInteracao.SetActive(false);

        playerController.canMove = true;
    }

    public void Vitoria()
    {
        soundManager.PlaySound(SoundManager.SoundType.TypeVictory);
        pnlVitoria.SetActive(true);
    }
}
