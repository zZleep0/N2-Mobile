using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LevelManager levelManager;
    public ItemProps itemProp;

    private bool isDragging = false;
    private Vector3 offset;

    public LayerMask itemLayer;
    private bool interagindoComItem = false; // Controle para evitar texto repetido

    public List<ItemProps> inventory = new List<ItemProps>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Moviment();

        Interacao();
    }

    void Moviment()
    {
        //if (Input.touchCount > 0 && canMove == true)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //    touchPosition.z = 0;
        //    gameObject.transform.position = touchPosition;

        //    //transform.Translate(new Vector2(moveDirection.transform.position.x, moveDirection.transform.position.y) * Time.deltaTime * velocidade);
        //}

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition);
                    if (touchedCollider != null && touchedCollider.transform == transform)
                    {
                        isDragging = true;
                        offset = transform.position - touchPosition;
                    }
                    break;
                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        transform.position = touchPosition + offset;
                    }
                    break;
                case TouchPhase.Ended:
                    {
                        if (isDragging)
                        {
                            isDragging = false;
                        }
                        break;
                    }
            }
        }
    }

    void Interacao()
    {
        Collider2D itemCollider = Physics2D.OverlapCircle(transform.position, 1f, itemLayer);
        if (itemCollider)
        {
            itemProp = itemCollider.GetComponent<ItemProps>();

            if (itemProp != null && !interagindoComItem)
            {
                interagindoComItem = true; // Marca que estamos interagindo
                levelManager.botoes.SetActive(true);
                levelManager.pnlInteracao.SetActive(true);

                // Atualiza o texto dinamicamente com base no item
                string textoInteracao = $"Voc� encontrou o item {itemProp.itemID}. Deseja coletar?";
                levelManager.AtualizarTextoInteracao(textoInteracao);

                levelManager.item = itemProp.itemID;

            }
            
        }
        else
        {
            if (interagindoComItem)
            {
                SairInteracao();
            }
        }

    }
    public void ColetarItemAtual()
    {
        if (itemProp != null)
        {
            // Verifica se o item possui um pr�-requisito
            if (itemProp.requiredItemID != -1) // Se -1, n�o h� pr�-requisito
            {
                // Verifica se o pr�-requisito est� no invent�rio
                bool temItemNecessario = inventory.Exists(item => item.itemID == itemProp.requiredItemID);
                if (!temItemNecessario)
                {
                    Debug.Log($"Voc� precisa do item {itemProp.requiredItemID} para pegar o item {itemProp.itemID}.");
                    levelManager.txtInteracao.text = $"Voc� precisa do item {itemProp.requiredItemID} para pegar este item.";
                    return; // N�o permite a coleta
                }
            }

            // Verifica se o item j� est� no invent�rio
            bool itemJaColetado = inventory.Exists(item => item.itemID == itemProp.itemID);

            if (itemJaColetado)
            {
                levelManager.botoes.SetActive(false);
                levelManager.txtInteracao.text = "Voc� ja coletou este item";

                Debug.Log($"Voc� j� coletou o item {itemProp.itemID}.");
                return; // Sai do m�todo sem coletar o item novamente
            }

            // Adiciona o item ao invent�rio
            inventory.Add(itemProp);
            Debug.Log($"Item coletado: {itemProp.itemID}");

            // Atualiza a imagem correspondente no Canvas
            levelManager.AtualizarImagemInventario(itemProp);

            if (itemProp.itemID == 5)
            {
                levelManager.Vitoria();
            }

            
        }
        else
        {
            Debug.Log("Nenhum item detectado para coletar.");
        }
    }

    public void SairInteracao()
    {
        Debug.Log("Saiu da interacao");
        interagindoComItem = false; // Reseta o estado de intera��o
        levelManager.pnlInteracao.SetActive(false);

    }
}
