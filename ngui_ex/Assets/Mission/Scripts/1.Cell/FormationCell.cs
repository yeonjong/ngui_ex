using UnityEngine;
using System.Text;

public class FormationCell : MonoBehaviour {

	/* TODO: make super class for xxxCell.cs */
	private PanelBase m_panelBase;
	public void SetBtnsTarget(PanelBase panelBase) {
		m_panelBase = panelBase;
	}

	private CELL_TYPE m_cellType; // set at Awake().
	private int m_cellIndex;	// set at Set().

	static public FormationCell current;
	void OnClick() {
		if (m_panelBase == null) {
			Debug.Log ("panelBase is null");
			return;
		}

		if (current == null && UICamera.currentTouchID != -2 && UICamera.currentTouchID != -3) {
			current = this;
			m_panelBase.OnClickXXXCell (m_cellType, m_cellIndex);
			current = null;
		}
	}
	/* todo end */


	/* original */
	private UISprite m_sprite;

	void Awake() {
		m_cellType = CELL_TYPE.formation; // must do.

		m_sprite = GetComponent<UISprite> ();
	}

	// make override method.
	public void Set (int cellIndex, string spriteName) { // TODO: param => (int cellIndex, template & args)
		if (m_sprite == null) Awake();

		m_cellIndex = cellIndex; // must do.

		m_sprite.spriteName = spriteName;
	}
	/* original end */

}
