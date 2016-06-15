using UnityEngine;
using System.Collections;

public class PanelBase : MonoBehaviour {

	public virtual void OnClickXXXBtn(string btnName) {	}

	public virtual void OnClickXXXCell(CELL_TYPE cellType, int cellIndex) { }

}

public enum CELL_TYPE {
	character,
	formation,
}
