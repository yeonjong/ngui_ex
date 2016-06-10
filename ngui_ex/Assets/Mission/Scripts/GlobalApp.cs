using UnityEngine;
using System.Collections;

public class GlobalApp : MonoBehaviour {

	private GlobalApp() { }
	private static GlobalApp inst;
	public static GlobalApp Inst {
		get {
			if (inst == null) {
				inst = new GlobalApp ();
			}
			return inst;
		}
	}

	public LocalGameData commData; //공통 정보.
	public LocalSaveData userData; //유저 정보.

}
