using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class QuizTrigger : MonoBehaviour
{
public CinemachineVirtualCamera VCamDisable;
    public CinemachineVirtualCamera VCamEnable;
    public GameObject UI;
    private PlayerMover _playerMover;
    private bool _canBuy = true;
    private float time = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (_canBuy)
        {
            VCamDisable.gameObject.SetActive(false);
            VCamDisable.gameObject.SetActive(true);
            Camera.main.GetComponent<CinemachineBrain>().enabled = true;
            Camera.main.cullingMask &= ~(1 << 10); 
            _playerMover = other.GetComponent<PlayerMover>();
            _playerMover.canMove = false;
            UI.SetActive(true);
            _canBuy = false;
        }
    }
    
    private void OnTriggerExit(Collider other) 
    {
        StartCoroutine(WaitForABit());    
    }

    public void ExitStore()
    {
        _playerMover.canMove = true;
        VCamDisable.gameObject.SetActive(true);
        VCamDisable.gameObject.SetActive(false);
        Camera.main.GetComponent<CinemachineBrain>().enabled = false;
        Camera.main.cullingMask |= (1 << 10);
        UI.SetActive(false);
    }

    private IEnumerator WaitForABit()
    {
        yield return new WaitForSeconds(time);
        _canBuy = true;
    }
}

