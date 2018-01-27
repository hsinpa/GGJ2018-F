using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystemManager : MonoBehaviour {
	List<JSONObject> listRoundJSON = new List<JSONObject>();

	public int round_index;
	public string round_id, round_name;

	JSONObject currentRound { get { return listRoundJSON[round_index]; } }

	//Call only during game activate
	public void SetUp(JSONObject p_roundJson) {
		listRoundJSON = p_roundJson.list;
		SetRoundInfo(2);
	}

	public void SetRoundInfo(int p_round_index) {
		round_index = p_round_index;
		round_id = currentRound.GetField("id").str;
		round_name = currentRound.GetField("name").str;
	}


	public List<JSONObject> GetCharacterJSON() {
		return currentRound.GetField("character_setting").list;
	}

	public List<JSONObject> GetVehecleJSON() {
		return currentRound.GetField("vehecle_setting").list;
	}

	public void SetNextRound() {
		SetRoundInfo(round_index+1);
	}

}
