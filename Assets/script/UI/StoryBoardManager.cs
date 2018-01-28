using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryBoardManager : MonoBehaviour {

	CanvasGroup canvasGroup { get { return transform.GetComponent<CanvasGroup>(); } }
	List<JSONObject> storyBoardJSONList = new List<JSONObject>();

	System.Action mCallback = null;

	int currentIndex;
	public void SetUp(JSONObject rawStoryBoardJSON, System.Action p_callback ) {
		storyBoardJSONList = rawStoryBoardJSON.list;
		mCallback = p_callback;

		SetStoryByIndex(0);

		SwitchCanvas(true);
	}

	public void SetStoryByIndex(int p_index) {
		currentIndex = p_index;
		JSONObject cStoryJSON = storyBoardJSONList[currentIndex];

		Text s_text = transform.Find("dialogue_panel/description").GetComponent<Text>();
		Image backgroundImage = GetComponent<Image>(); 

		//Set text
		s_text.text = cStoryJSON.GetField("text").str;

		//Set background
		Sprite sprite = Resources.Load<Sprite>("Background/"+cStoryJSON.GetField("background").str);
		backgroundImage.sprite = sprite;
	}

	public void Next() {
		if (currentIndex +1 >= storyBoardJSONList.Count) {
			Close();
			return;
		}

		SetStoryByIndex(currentIndex +1 );
	}
	
	public void SwitchCanvas(bool isOpen) {
		canvasGroup.alpha = (isOpen) ? 1 : 0;
		canvasGroup.interactable = (isOpen);
		canvasGroup.blocksRaycasts = (isOpen);
	}

	public void Close() {
		if (mCallback != null) mCallback();
		SwitchCanvas(false);
	}

}
