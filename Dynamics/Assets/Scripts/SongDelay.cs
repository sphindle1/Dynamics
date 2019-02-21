using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDelay : MonoBehaviour
{
    AudioSource song;
    // Start is called before the first frame update
    void Start()
    {
        song = GetComponent<AudioSource>();
        song.PlayDelayed(1.2f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
