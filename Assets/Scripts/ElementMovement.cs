using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementMovement : MonoBehaviour,IPointerUpHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler
{
    [SerializeField] Transform _oldParent;
    [SerializeField] Transform _newParent;
    [SerializeField] List<int> _cells;

    public int level;

    private void Start()
    {
        transform.DOScale(3, 2f);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Cell")
        {
            if (!col.GetComponent<Cell>().cellOccupied)
            {
                _newParent = col.transform;
            } else if (col.GetComponent<Cell>().cellOccupied)
            {
                if (col.transform.childCount > 0 && col.transform.GetChild(0).tag == "Element")
                {
                    if (col.transform.GetChild(0).GetComponent<ElementMovement>().level == level)
                    {
                        // Join
                    } else
                    {
                        // Change
                        

                    }
                }
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.tag == "Cell")
        {
            if (other.transform == _newParent) 
            {
                _newParent = null;
            }
         }
    }

    private void Update()
    {
            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.right);
            //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.right, Color.red);


            //if (hit.collider != null)
            //{
            //    Debug.Log(hit.collider.name);
            //}
    }

    void FixedUpdate()
    {
       
    }

    void Ray()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 1000f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1000f);

        if (hit && hit.collider.CompareTag("Cell"))
        {
            if (hit.transform.childCount == 0)
            {
                transform.parent = hit.transform.parent;
                //тут ошибка, срабатывает при создании элемента

                if (hit.collider.transform.childCount == 0)
                {
                    transform.parent = hit.transform;

                    transform.DOLocalMove(Vector2.zero, .2f);
                }
            }
        }
    }


    void ChangeElements(Transform currentElement, Transform targetElement)
    {
        
    }

    void JoinElements(Transform currentElement, Transform targetElement)
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOKill();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_newParent == null)
        {
            // Если активный элемент, не выбрал пустую, или иную ячейку
            // то он возвращается обратно

            transform.parent = _oldParent;
            transform.DOLocalMove(Vector2.zero, 1f);
        }
        else
        {
            transform.parent = _newParent;

            _oldParent.GetComponent<Cell>().cellOccupied = false;
            _newParent.GetComponent<Cell>().cellOccupied = true;

            transform.DOLocalMove(Vector2.zero, 1f);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _oldParent = transform.parent;
        transform.parent = GameSettings.instance.canvas.transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<RectTransform>().anchoredPosition += eventData.delta / GameSettings.instance.canvas.scaleFactor;
    }
}