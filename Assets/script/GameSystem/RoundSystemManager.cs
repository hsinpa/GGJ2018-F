using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystemManager : MonoBehaviour {
	List<JSONObject> listRoundJSON = new List<JSONObject>();

	public int round_index;
	public string round_id, round_name;

	public JSONObject currentRound { get { return listRoundJSON[round_index]; } }

	//Call only during game activate
	public void SetUp(JSONObject p_roundJson, int initial_round = 0) {
		listRoundJSON = p_roundJson.list;
		SetRoundInfo(initial_round);
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

	//Return false if next round don't exist
	public bool SetNextRound() {
		if (round_index + 1 >= listRoundJSON.Count) return false;

		SetRoundInfo(round_index+1);
		return true;
	}

}
