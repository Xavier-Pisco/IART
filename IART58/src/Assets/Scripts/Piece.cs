using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

	void start() {
	}

	void OnMouseDown() {
		PieceHandler pieceHandler = this.transform.parent.transform.parent.GetComponent<PieceHandler>();
		pieceHandler.pieceClicked(this);
	}
}