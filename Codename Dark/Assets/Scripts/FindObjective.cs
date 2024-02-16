using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindObjective : MonoBehaviour
{
    [Header("Vehicle Button")]
    [SerializeField] private KeyCode vehicleButton = KeyCode.F;

    [Header("Generator Sound Effects And Radius")]
    private float radius = 3f;
    public PlayerScript player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(vehicleButton) && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
            ObjectivesComplete.occurrence.GetObjectivesDone(true, true, true, false);
        }
    }
}
