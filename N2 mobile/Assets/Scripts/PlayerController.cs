using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LevelManager levelManager;
    public ItemProps itemProp;
    public SoundManager soundManager;

    public bool canMove = true;

    public LayerMask itemLayer;
    private bool interagindoComItem = false; // Controle para evitar texto repetido

    public List<ItemProps> inventory = new List<ItemProps>();


    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SCRIPTS").GetComponent<SoundManager>();
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

        if (Input.touchCount > 0 && canMove == true)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Obtém a posição do toque na tela e converte para o mundo
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0; // Garante que a posição Z seja zero (2D)

                StopAllCoroutines(); // Para qualquer movimento anterior
                StartCoroutine(MoveToPosition(touchPosition));
            }
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float speed = 5f; // Velocidade do movimento
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f) // Checa se está perto do destino
        {
            // Move o jogador na direção do alvo com base na velocidade e no deltaTime
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null; // Espera o próximo frame
        }

        // Ajusta a posição final para garantir que seja exatamente no destino
        transform.position = targetPosition;
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
                canMove = false;

                levelManager.botoes.SetActive(true);
                levelManager.pnlInteracao.SetActive(true);

                // Atualiza o texto dinamicamente com base no item
                string textoInteracao = $"Você encontrou o item {itemProp.itemID}. Deseja coletar?";
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
            // Verifica se o item possui um pré-requisito
            if (itemProp.requiredItemID != -1) // Se -1, não há pré-requisito
            {
                // Verifica se o pré-requisito está no inventário
                bool temItemNecessario = inventory.Exists(item => item.itemID == itemProp.requiredItemID);
                if (!temItemNecessario)
                {
                    Debug.Log($"Você precisa do item {itemProp.requiredItemID} para pegar o item {itemProp.itemID}.");
                    levelManager.txtInteracao.text = $"Você precisa do item {itemProp.requiredItemID} para pegar este item.";
                    return; // Não permite a coleta
                }
            }

            // Verifica se o item já está no inventário
            bool itemJaColetado = inventory.Exists(item => item.itemID == itemProp.itemID);

            if (itemJaColetado)
            {
                levelManager.botoes.SetActive(false);
                levelManager.txtInteracao.text = "Você ja coletou este item";

                Debug.Log($"Você já coletou o item {itemProp.itemID}.");
                return; // Sai do método sem coletar o item novamente
            }

            // Adiciona o item ao inventário
            inventory.Add(itemProp);
            Debug.Log($"Item coletado: {itemProp.itemID}");

            // Atualiza a imagem correspondente no Canvas
            levelManager.AtualizarImagemInventario(itemProp);
            soundManager.PlaySound(SoundManager.SoundType.TypeCollect);

            itemProp.GetComponent<Renderer>().enabled = false;
            itemProp.GetComponent<CircleCollider2D>().enabled = false;

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

        interagindoComItem = false; // Reseta o estado de interação
        canMove = true;

        levelManager.pnlInteracao.SetActive(false);

    }
}
