using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int count;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
    }
}
