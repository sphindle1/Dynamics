using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text component that is displaying the score. The text value " +
      "of this component will change with the score.")]
    private Text m_UIText;
    // Start is called before the first frame update
    void Start()
    {
        m_UIText.text = "" + Values.Score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
