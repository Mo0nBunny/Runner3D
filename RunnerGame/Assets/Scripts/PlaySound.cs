using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]

public class PlaySound : MonoBehaviour {

    public AudioClip cliked;
    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }


	void Start () {
        gameObject.AddComponent<AudioSource>();
        source.clip = cliked;
        source.playOnAwake = false;
        button.onClick.AddListener(() => PlayCliked());
	}
	
	void PlayCliked()
    {
        source.PlayOneShot(cliked);
    }
   
}
