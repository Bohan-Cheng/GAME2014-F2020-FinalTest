using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SmartPlat : MonoBehaviour
{
    [SerializeField] float shrinkTime = 2.0f;
    [SerializeField] float UpDownRange = 5.0f;
    [SerializeField] AudioClip clip_Shrink;
    [SerializeField] AudioClip clip_Resize;

    AudioSource audio;
    bool ShouldShrink = false;
    Vector3 OSize;

    // Start is called before the first frame update
    void Start()
    {
        OSize = transform.localScale;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GoUpAndDown();
        if(ShouldShrink)
        {
            Shrink();
        }
        else
        {
            Resize();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            ShouldShrink = true;
            audio.clip = clip_Shrink;
            audio.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Invoke("ResetSize", 2.0f);
        }
    }

    void ResetSize()
    {
        ShouldShrink = false;
        audio.clip = clip_Resize;
        audio.Play();
    }

    public void Shrink()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, shrinkTime * Time.deltaTime);
    }

    public void Resize()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, OSize, shrinkTime * Time.deltaTime);
    }

    void GoUpAndDown()
    {
        Vector3 NewLocation = gameObject.transform.position;
        float RunningTime = Time.time;
        float DeltaHeight = (Mathf.Sin(RunningTime + Time.deltaTime) - Mathf.Sin(RunningTime));
        NewLocation.y += DeltaHeight * UpDownRange;       //Scale our height by a factor of 20
        transform.position = NewLocation;
    }
}
