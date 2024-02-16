using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeyNetwork
{
    public class KeyGateRegulator : MonoBehaviour
    {
        [Header("Animations")]
        private Animator gateAnimation;
        private bool openGate = false;
        [SerializeField] private string openAnimationName = "GateOpen";
        [SerializeField] private string closeAnimationName = "GateClose";

        [Header("Time and UI")]
        [SerializeField] private int timeToShowUI = 1;
        [SerializeField] private GameObject showGateLockedUI = null;
        [SerializeField] private KeyList keyList = null;
        [SerializeField] private int waitTimer = 1;
        [SerializeField] private bool pauseInteraction = false;

        [Header("Sounds And UI")]
        public AudioClip gateSound;
        public AudioSource audioSource;

        private void Awake()
        {
            gateAnimation = gameObject.GetComponent<Animator>();
        }

        public void StartAnimation()
        {
            if(keyList.hasKey)
            {
                Open_Gate();
            }
            else
            {
                StartCoroutine(showGateLocked());
            }
        }

        private IEnumerator StopGateInterconnection()
        {
            pauseInteraction = true;
            yield return new WaitForSeconds(waitTimer);
            pauseInteraction = false;
        }

        void Open_Gate()
        {
            if(!openGate && !pauseInteraction)
            {
                gateAnimation.Play(openAnimationName, 0, 0.0f);
                openGate = true;
                audioSource.PlayOneShot(gateSound);
                ObjectivesComplete.occurrence.GetObjectivesDone(true, false, false, false);
                StartCoroutine(StopGateInterconnection());
            }
            else if(openGate && !pauseInteraction)
            {
                gateAnimation.Play(closeAnimationName, 0, 0.0f);
                openGate = false;
                
                audioSource.PlayOneShot(gateSound);
                StartCoroutine(StopGateInterconnection());
            }
        }

        IEnumerator showGateLocked()
        {
            showGateLockedUI.SetActive(true);
            yield return new WaitForSeconds(timeToShowUI);
            showGateLockedUI.SetActive(false);
        }
    }
}
