using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	public StoryBoardManager storyBoardManager { get { return transform.Find("StoryBoard").GetComponent<StoryBoardManager>(); } }
	public Animator roundTextAnimator { get { return transform.Find("round_info").GetComponent<Animator>(); }}
}
