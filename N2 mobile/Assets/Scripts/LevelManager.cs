using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //[SerializeField] private SoundManager soundManager;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizarTextoInteracao(string texto)
    {
        txtInteracao.text = texto;
        Debug.Log($"Texto de interação atualizado para: {texto}");
    }

    public void AtualizarImagemInventario(ItemProps itemProp)
    {
        // Procura pela primeira imagem vazia no inventário
        foreach (Image imagem in imagensInventario)
        {
            if (imagem.sprite == null) // Se a imagem está vazia
            {
                // Define o sprite do item na imagem
                imagem.sprite = itemProp.gameObject.GetComponent<SpriteRenderer>().sprite;
                Debug.Log($"Imagem atualizada com o item: {itemProp.itemID}");
                return; // Sai do loop após atualizar
            }
        }
    }


    public void Sim()
    {
        AtualizarTextoInteracao("Item " + item + " coletado! Continue explorando.");

        playerController.ColetarItemAtual();

        botoes.SetActive(false);
        
        
    }

    public void Nao()
    {
        // Atualiza o texto para refletir que a interação foi cancelada
        AtualizarTextoInteracao("Interação cancelada.");

        botoes.SetActive(false);
        pnlInteracao.SetActive(false);
    }

    public void Vitoria()
    {
        //soundManager.PlaySound(SoundManager.SoundType.TypeVictory);
        pnlVitoria.SetActive(true);
    }
}
