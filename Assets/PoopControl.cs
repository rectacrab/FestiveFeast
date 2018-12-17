using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopControl : MonoBehaviour
{
    private AudioSource m_audio;
    [SerializeField] private AudioClip[] m_clips;

    // Start is called before the first frame update
    void Start()
    {
        m_audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D (Collision2D collObj)
    {
        //player audio when touching stuff.
        m_audio.PlayOneShot(m_clips[Random.Range(0, m_clips.Length)]);
    }
}
