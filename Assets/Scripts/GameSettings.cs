using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] UI ui;

    public List<GameObject> elements = new List<GameObject>();
    public GameObject grid;
    public Canvas canvas;
    public int speedGame = 2;

    public static GameSettings instance;

    #region properties

    public UI GetUI
    {
        get { return ui; }
    }

    #endregion

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }

        Time.timeScale = speedGame;
    }

    private void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up / 2);
        //Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.up / 2, Color.red);


        //if (hit)
        //{
        //    Debug.Log(hit.collider);
        //}
    }

}
