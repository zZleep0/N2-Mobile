using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LevelManager levelManager;

    private bool isDragging = false;
    private Vector3 offset;

    public LayerMask itemLayer;

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
        if (Physics2D.OverlapCircle(transform.position, 1f, itemLayer))
        {
            Debug.Log("ta no layer item");

            levelManager.pnlInteracao.SetActive(true);
            levelManager.txtInteracao.text = "Voce achou um item, deseja pega-lo?";
            
        }
        else
        {
            SairInteracao();
        }

    }
    public void SairInteracao()
    {
        Debug.Log("Saiu da interacao");
        levelManager.pnlInteracao.SetActive(false);

    }
}
