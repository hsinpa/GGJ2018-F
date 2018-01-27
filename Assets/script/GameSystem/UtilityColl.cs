﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility {
	public class UtilityColl {

		/// <summary>
		/// Possibilities the match.
		/// </summary>
		/// <returns><c>true</c>, if match was possibilityed, <c>false</c> otherwise.</returns>
		public static bool FlipCoin(float rate) {
			float testValue = UnityEngine.Random.Range(0f ,1f);
			return ( rate >= testValue ) ? true : false;
		}


	}
}