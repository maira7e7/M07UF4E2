using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class VictoryFinished : StateMachineBehaviour 
{
  private const bool V = true;
  public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo,int layerIndex)
  {
    animator.gameObject.transform.parent.gameObject.GetComponent<PlayerMover>().canMove = true;
    Camera.main.GetComponent<CinemachineBrain>().enabled = false;
  }
}
