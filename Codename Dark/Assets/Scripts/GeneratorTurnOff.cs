using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTurnOff : MonoBehaviour
{
    [Header("Generator Lights and Button")]
    public GameObject greenLight;
    public GameObject redLight;
    public bool button;

    [Header("Generator Sound Effects And Radius")]
    private float radius = 2f;
    public PlayerScript player;
    public Animator anim;

    [Header("Sounds")]
    public AudioClip objectiveCompletedSound;
    public AudioSource audioSource;

    private void Awake()
    {
        button = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetKeyDown("q") && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            button = true;
            anim.enabled = false;
            greenLight.SetActive(false);
            redLight.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot(objectiveCompletedSound);
            ObjectivesComplete.occurrence.GetObjectivesDone(false, false, false, true);
        }
        else if(button == false)
        {
            greenLight.SetActive(true);
            redLight.SetActive(false);
        }
    }
}
