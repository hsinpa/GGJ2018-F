using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StoryBoardManager : MonoBehaviour {

	CanvasGroup canvasGroup { get { return transform.GetComponent<CanvasGroup>(); } }
	List<JSONObject> storyBoardJSONList = new List<JSONObject>();

	System.Action mCallback = null;

	int currentIndex;
	public void SetUp(JSONObject rawStoryBoardJSON, System.Action p_callback ) {
		storyBoardJSONList = rawStoryBoardJSON.list;
		mCallback = p_callback;

		//Reset position to center
		transform.localPosition = Vector3.zero;

		SetStoryByIndex(0);

		SwitchCanvas(true);
	}

	public void SetStoryByIndex(int p_index) {
		currentIndex = p_index;
		JSONObject cStoryJSON = storyBoardJSONList[currentIndex];

		Text s_text = transform.Find("dialogue_panel/description").GetComponent<Text>();
		Image avatarImage = transform.Find("dialogue_panel/avatar").GetComponent<Image>();

		Image backgroundImage = GetComponent<Image>(); 
		//Set text
		s_text.text = cStoryJSON.GetField("text").str;

		//Set background
		Sprite bg_sprite = Resources.Load<Sprite>("Sprite/Background/"+cStoryJSON.GetField("background").str);
		backgroundImage.sprite = bg_sprite;

		//Set avatar
		if (cStoryJSON.HasField("avatar") && cStoryJSON.GetField("avatar").str != "" ) {
			Sprite avatar_sprite = Resources.Load<Sprite>("Sprite/Avatar/"+cStoryJSON.GetField("avatar").str);
			avatarImage.enabled = (avatar_sprite != null);
			avatarImage.sprite = avatar_sprite;
		} else {
			avatarImage.enabled = false;
		}
	}

	public void Next() {
		if (currentIndex +1 >= storyBoardJSONList.Count) {
			Close();
			return;
		}

		SetStoryByIndex(currentIndex +1 );
	}
	
	public void SwitchCanvas(bool isOpen, bool wantAnimation = false) {
		if (wantAnimation) {
			// canvasGroup.
			DOTween.KillAll();
			transform.DOMoveX(2000, 1.5f);
		}

		// canvasGroup.alpha = (isOpen) ? 1 : 0;
		canvasGroup.interactable = (isOpen);
		canvasGroup.blocksRaycasts = (isOpen);
	}

	public void Close() {
		if (mCallback != null) mCallback();
		SwitchCanvas(false, true);
	}

}
