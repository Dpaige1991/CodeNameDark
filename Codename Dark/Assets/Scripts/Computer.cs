using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [Header("Computer On/Off")]
    public bool lightsOn = true;
    private float radius = 2.5f;
    public Light lights;

    [Header("Computr Assign Things")]
    public PlayerScript player;
    [SerializeField] private GameObject computerUI;
    [SerializeField] private int showComputerUIfor = 5;

    private void Awake()
    {
        lights = GetComponent<Light>();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if(Input.GetKeyDown("q"))
            {
                StartCoroutine(ShowComputerUI());
                lightsOn = false;
                lights.intensity = 0;
            }
        }
    }

    IEnumerator ShowComputerUI()
    {
        computerUI.SetActive(true);
        yield return new WaitForSeconds(showComputerUIfor);
        computerUI.SetActive(false);
    }
}
