using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        transform.position = transform.position + new Vector3(0, 0, -3.0f);
        Debug.Log("entered");
    }

    private void OnMouseDown()
    {
        Debug.Log("here");
        if (tag.Equals("Credits"))
        {
            SceneManager.LoadScene("credits");
        }
        else if (tag.Equals("Exit"))
        {
            //Change for actual application if built
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else if (tag.Equals("Start"))
        {
            SceneManager.LoadScene("main");

        }
        else
        {
            SceneManager.LoadScene("titlescreen");
        }
    }

    void OnMouseExit()
    {
        transform.position = transform.position + new Vector3(0, 0, 3.0f);
    }
}
