using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State
{
    public enum States
    {
       IDLE,
       PIECE_CLICKED,
       TILE_CLICKED,
       GAME_OVER
    }

    public States state = States.IDLE;

	List<Vector2> whitePieces = new List<Vector2>();
	List<Vector2> blackPieces = new List<Vector2>();

	public int player;

    List<Vector2> possibleMoves = new List<Vector2>();
    Vector2 lastPiece;
    List<Vector2> lastPieces = new List<Vector2>();
    List<Vector2> lastTiles = new List<Vector2>();

    Bot bot;

	public State(Vector3 w1, Vector3 w2, Vector3 w3, Vector3 b1, Vector3 b2, Vector3 b3, int player) {
		whitePieces.Add(new Vector2(w1.x, w1.z));
		whitePieces.Add(new Vector2(w2.x, w2.z));
		whitePieces.Add(new Vector2(w3.x, w3.z));
		blackPieces.Add(new Vector2(b1.x, b1.z));
		blackPieces.Add(new Vector2(b3.x, b3.z));
		blackPieces.Add(new Vector2(b2.x, b2.z));
		this.player = player;
	}

    public State(Vector3 w1, Vector3 w2, Vector3 w3, Vector3 b1, Vector3 b2, Vector3 b3, List<Vector2> lastPieces, List<Vector2> lastTiles, int player, States state) {
		whitePieces.Add(new Vector2(w1.x, w1.z));
		whitePieces.Add(new Vector2(w2.x, w2.z));
		whitePieces.Add(new Vector2(w3.x, w3.z));
		blackPieces.Add(new Vector2(b1.x, b1.z));
		blackPieces.Add(new Vector2(b3.x, b3.z));
		blackPieces.Add(new Vector2(b2.x, b2.z));
        this.lastPieces = lastPieces;
        this.lastTiles = lastTiles;
        this.state = state;
		this.player = player;
	}

	public State deepCopy() {
		State s = new State(new Vector3(whitePieces[0].x, 0, whitePieces[0].y),
							new Vector3(whitePieces[1].x, 0, whitePieces[1].y),
							new Vector3(whitePieces[2].x, 0, whitePieces[2].y),
							new Vector3(blackPieces[0].x, 0, blackPieces[0].y),
							new Vector3(blackPieces[1].x, 0, blackPieces[1].y),
							new Vector3(blackPieces[2].x, 0, blackPieces[2].y),
                            lastPieces.ConvertAll(piece => new Vector2(piece.x, piece.y)),
                            lastTiles.ConvertAll(tile => new Vector2(tile.x, tile.y)),
							player, state);
		return s;
	}

	public float Value(int heuristic) {
		if (checkVictory()) {
			return (player == 0) ? 1000 : -1000;
		}
        if (tie())
            return 0;
        switch (heuristic) {
            case 1:
                return Value1();
            case 2:
                return Value2();
            case 3:
                return Value3();
            case 4:
                return Value4();
            case 5:
                return Value5();
            case 6:
                return Value6();
            case 7:
                return Value7();
            default:
                Debug.Log("Invalid heuristic");
                break;
        }
        return 0;
	}

    public float Value1() {
		float result = 0;
		result += calculatePlayerValue1(0);
		result -= calculatePlayerValue1(1);
		return result;
    }

    public float Value2() {
		float result = 0;
		result += calculatePlayerValue2(0);
		result -= calculatePlayerValue2(1);
		return result;
    }

    public float Value3() {
		float result = 0;
		result += calculatePlayerValue3(0);
		result -= calculatePlayerValue3(1);
		return result;
    }

    public float Value4() {
		float result = 0;
		result += calculatePlayerValue4(0);
		result -= calculatePlayerValue4(1);
		return result;
    }

    public float Value5() {
		float result = 0;
		result += calculatePlayerValue5(0);
		result -= calculatePlayerValue5(1);
		return result;
    }

    public float Value6() {
		float result = 0;
		result += calculatePlayerValue6(0);
		result -= calculatePlayerValue6(1);
		return result;
    }

    public float Value7() {
		float result = 0;
		result += calculatePlayerValue7(0);
		result -= calculatePlayerValue7(1);
		return result;
    }

	public float calculatePlayerValue1(int player) {
		List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
		List<Vector2> opponentPieces = (player == 1) ? blackPieces : whitePieces;
		float result = 0;

		if (piecesTogether(playerPieces[0], playerPieces[1]))
			result += 10;
		if (piecesTogether(playerPieces[0], playerPieces[2]))
			result += 10;
		if (piecesTogether(playerPieces[1], playerPieces[2]))
			result += 10;

		if (oneSpaceBetween(playerPieces[0], playerPieces[1], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[0], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[1], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;

		return result;

	}

    public float calculatePlayerValue2(int player) {
		List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
		List<Vector2> opponentPieces = (player == 1) ? blackPieces : whitePieces;
		float result = 0;

		if (piecesTogether(playerPieces[0], playerPieces[1]))
			result += 10;
		if (piecesTogether(playerPieces[0], playerPieces[2]))
			result += 10;
		if (piecesTogether(playerPieces[1], playerPieces[2]))
			result += 10;

		if (oneSpaceBetween(playerPieces[0], playerPieces[1], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[0], playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[1], playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;

        if (threePiecesOneSpaceBetween(playerPieces[0],playerPieces[1],playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
            result += 10;

		return result;
	}

    public float calculatePlayerValue3(int player) {
		List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
		List<Vector2> opponentPieces = (player == 1) ? blackPieces : whitePieces;
		float result = 0;


		if (piecesTogether(playerPieces[0], playerPieces[1]))
			result += 10;
		if (piecesTogether(playerPieces[0], playerPieces[2]))
			result += 10;
		if (piecesTogether(playerPieces[1], playerPieces[2]))
			result += 10;

		if (oneSpaceBetween(playerPieces[0], playerPieces[1], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[0], playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[1], playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;

        if (spaceBetweenPieceMiddle(playerPieces[0], playerPieces[1], playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
            result += 2;
        else if (spaceBetweenPieceMiddle(playerPieces[0], playerPieces[2], playerPieces[1], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
            result += 2;
        else if (spaceBetweenPieceMiddle(playerPieces[1], playerPieces[2], playerPieces[0], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
            result += 2;

		return result;
	}

    public float calculatePlayerValue4(int player) {
		List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
		List<Vector2> opponentPieces = (player == 1) ? blackPieces : whitePieces;
		float result = 0;


		if (piecesTogether(playerPieces[0], playerPieces[1]))
			result += 10;
		if (piecesTogether(playerPieces[0], playerPieces[2]))
			result += 10;
		if (piecesTogether(playerPieces[1], playerPieces[2]))
			result += 10;

		if (oneSpaceBetween(playerPieces[0], playerPieces[1], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[0], playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[1], playerPieces[2], opponentPieces[0],opponentPieces[1],opponentPieces[2]))
			result += 5;

        result += distanceFromCenter(playerPieces[0]) * -2;
        result += distanceFromCenter(playerPieces[1]) * -2;
        result += distanceFromCenter(playerPieces[2]) * -2;

		return result;
	}

	public float calculatePlayerValue5(int player) {
		List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
		List<Vector2> opponentPieces = (player == 1) ? blackPieces : whitePieces;
		float result = 0;

		if (piecesTogether(playerPieces[0], playerPieces[1]))
			result += 10;
		if (piecesTogether(playerPieces[0], playerPieces[2]))
			result += 10;
		if (piecesTogether(playerPieces[1], playerPieces[2]))
			result += 10;

		return result;
	}

	public float calculatePlayerValue6(int player) {
		List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
		List<Vector2> opponentPieces = (player == 1) ? blackPieces : whitePieces;
		float result = 0;

		if (piecesTogether(playerPieces[0], playerPieces[1]))
			result += 10;
		if (piecesTogether(playerPieces[0], playerPieces[2]))
			result += 10;
		if (piecesTogether(playerPieces[1], playerPieces[2]))
			result += 10;

		if (oneSpaceBetween(playerPieces[0], playerPieces[1], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[0], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[1], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;

        if (threePiecesOneSpaceBetween(playerPieces[0], playerPieces[1], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
            result += 10;

		return result;
	}

	public float calculatePlayerValue7(int player) {
		List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
		List<Vector2> opponentPieces = (player == 1) ? blackPieces : whitePieces;
		float result = 0;

		if (piecesTogether(playerPieces[0], playerPieces[1]))
			result += 10;
		if (piecesTogether(playerPieces[0], playerPieces[2]))
			result += 10;
		if (piecesTogether(playerPieces[1], playerPieces[2]))
			result += 10;

		if (oneSpaceBetween(playerPieces[0], playerPieces[1], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[0], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;
		if (oneSpaceBetween(playerPieces[1], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
			result += 5;

        if (threePiecesOneSpaceBetween(playerPieces[0], playerPieces[1], playerPieces[2], opponentPieces[0], opponentPieces[1], opponentPieces[2]))
            result += 15;

		return result;
	}

    // bot with this has no difference
    public int distanceFromCenter(Vector2 p) {
        int result = (int) Math.Abs(p.x - 2);
        result += (int) Math.Abs(p.y - 2);
        return result;
    }

    // bot without this wins more
    public bool spaceBetweenPieceMiddle(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 o1, Vector2 o2, Vector2 o3) {
        if (oneSpaceBetween(p1,p2,o1,o2,o3)) {
            if (p1.y == p2.y) {
                if (p3.x == (p1.x + p2.x) / 2 && // p3 in same line as space between p1 and p2
                    (o1.x != p3.x || o1.y - p1.y > p3.y - p1.y || (o1.y < p1.y && p3.y > p1.y) || (o1.y > p1.y && p3.y < p1.y)) &&  // o1 is not in the way of p3
                    (o2.x != p3.x || o2.y - p1.y > p3.y - p1.y || (o2.y < p1.y && p3.y > p1.y) || (o2.y > p1.y && p3.y < p1.y)) &&
                    (o3.x != p3.x || o3.y - p1.y > p3.y - p1.y || (o3.y < p1.y && p3.y > p1.y) || (o3.y > p1.y && p3.y < p1.y))) return true;
            }
            else if (p1.x == p2.x) {
                if ((p3.y == (p1.y + p2.y) / 2) &&  // p3 in same column as space between p1 and p2
                    (o1.y != p3.y || o1.x - p1.x > p3.x - p1.x || (o1.x < p1.x && p3.x > p1.x) || (o1.x > p1.x && p3.x < p1.x)) && // o1 is not in the way of p3
                    (o2.y != p3.y || o2.x - p1.x > p3.x - p1.x || (o2.x < p1.x && p3.x > p1.x) || (o2.x > p1.x && p3.x < p1.x)) &&
                    (o3.y != p3.y || o3.x - p1.x > p3.x - p1.x || (o3.x < p1.x && p3.x > p1.x) || (o3.x > p1.x && p3.x < p1.x))) return true;
            }
            else if (p3.y - p3.x == (p1.y + p2.y) / 2 - (p1.x + p2.x) / 2 && // p3 in same diagonal as space between p1 and p2
                    (o1.y - o1.x != p3.y - p3.x || // o1 is not in the way for p3
                        (o1.x < (p1.x + p2.x) / 2 && p3.x > (p1.x + p2.x) / 2) || (o1.x > (p1.x + p2.x) / 2 && p3.x < (p1.x + p2.x) / 2)) &&
                    (o2.y - o2.x != p3.y - p3.x ||
                        (o2.x < (p1.x + p2.x) / 2 && p3.x > (p1.x + p2.x) / 2) || (o2.x > (p1.x + p2.x) / 2 && p3.x < (p1.x + p2.x) / 2)) &&
                    (o3.y - o3.x != p3.y - p3.x ||
                        (o3.x < (p1.x + p2.x) / 2 && p3.x > (p1.x + p2.x) / 2) || (o3.x > (p1.x + p2.x) / 2 && p3.x < (p1.x + p2.x) / 2))) return true;
            else if (p3.y + p3.x == (p1.y + p2.y) / 2 + (p1.x + p2.x) / 2 && // p3 in same diagonal as space between p1 and p2
                    (o1.y + o1.x != p3.y + p3.x || // o1 is not in the way for p3
                        (o1.x < (p1.x + p2.x) / 2 && p3.y > (p1.y + p2.y) / 2) || (o1.y > (p1.y + p2.y) / 2 && p3.y < (p1.y + p2.y) / 2)) && // o1 is not on same diagonal as p3
                    (o2.y + o2.x != p3.y + p3.x ||
                        (o1.x < (p1.x + p2.x) / 2 && p3.y > (p1.y + p2.y) / 2) || (o1.y > (p1.y + p2.y) / 2 && p3.y < (p1.y + p2.y) / 2)) &&
                    (o3.y + o1.x != p3.y + p3.x ||
                        (o1.x < (p1.x + p2.x) / 2 && p3.y > (p1.y + p2.y) / 2) || (o1.y > (p1.y + p2.y) / 2 && p3.y < (p1.y + p2.y) / 2))) return true;
        }
        return false;
    }

	public bool deadEnd(Vector2 p1, Vector2 p2) {
		// Estes casos são casos que o resto detecta como bons mas na realidade são maus
		// Exemplo: [1,0] [0,1] as duas peças estão juntas mas é impossível formar uma linha de 3 com elas
		return ((p1.x == 1 && p1.y == 0 && p2.x == 0 && p2.y == 1) // [1,0] [0,1]
				|| (p1.x == 0 && p1.y == 1 && p2.x == 1 && p2.y == 0) // [0,1] [1,0]
				|| (p1.x == 3 && p1.y == 0 && p2.x == 4 && p2.y == 1) // [3,0] [4,1]
				|| (p1.x == 4 && p1.y == 1 && p2.x == 3 && p2.y == 0) // [4,1] [3,0]
				|| (p1.x == 1 && p1.y == 4 && p2.x == 0 && p2.y == 3) // [1,4] [0,3]
				|| (p1.x == 0 && p1.y == 3 && p2.x == 1 && p2.y == 4) // [0,3] [1,4]
				|| (p1.x == 3 && p1.y == 4 && p2.x == 4 && p2.y == 3) // [3,4] [4,3]
				|| (p1.x == 4 && p1.y == 3 && p2.x == 3 && p2.y == 4) // [4,3] [3,4]
			);
	}

	public bool piecesTogether(Vector2 p1, Vector2 p2) {
		return ((((p1.x == p2.x + 1 || p1.x == p2.x - 1) && p1.y == p2.y) // same y and x is the next tile
				|| ((p1.y == p2.y + 1 || p1.y == p2.y - 1) && p1.x == p2.x) // Same x and y is the next tile
				|| ((p1.y == p2.y + 1 && p1.x == p2.x + 1)) // one diagonal
				|| ((p1.y == p2.y - 1 && p1.x == p2.x - 1)) // one diagonal
				|| ((p1.y == p2.y + 1 && p1.x == p2.x - 1)) // one diagonal
				|| ((p1.y == p2.y - 1 && p1.x == p2.x + 1)) // one diagonal
				) && !deadEnd(p1, p2));
	}

	public bool oneSpaceBetween(Vector2 p1, Vector2 p2, Vector2 o1, Vector2 o2, Vector2 o3) {
        if (((p1.x == p2.x + 2 || p1.x == p2.x - 2) && p1.y == p2.y) // same y and x has 1 space
				|| ((p1.y == p2.y + 2 || p1.y == p2.y - 2) && p1.x == p2.x) // same x and y has 1 space
				|| ((p1.y == p2.y - 2 && p1.x == p2.x - 2)) // one diagonal
				|| ((p1.y == p2.y + 2 && p1.x == p2.x - 2)) // one diagonal
				|| ((p1.y == p2.y - 2 && p1.x == p2.x + 2)) // one diagonal
				|| ((p1.y == p2.y + 2 && p1.x == p2.x + 2))) // one diagonal
        {
            int middleX = (int) (p1.x + p2.x) / 2;
            int middleY = (int) (p1.y + p2.y) / 2;
            if ((o1.x != middleX || o1.y != middleY) &&
                (o2.x != middleX || o2.y != middleY) &&
                (o3.x != middleX || o3.y != middleY)) return true;
        }
        return false;
	}

	public bool twoSpaceBetween(Vector2 p1, Vector2 p2, Vector2 o1, Vector2 o2, Vector2 o3) {
        if (((p1.x == p2.x + 3 || p1.x == p2.x - 3) && p1.y == p2.y) // same y and x has 2 spaces
				|| ((p1.y == p2.y + 3 || p1.y == p2.y - 3) && p1.x == p2.x) // same x and y has 2 spaces
				|| ((p1.y == p2.y - 3 && p1.x == p2.x - 3)) // one diagonal
				|| ((p1.y == p2.y + 3 && p1.x == p2.x - 3)) // one diagonal
				|| ((p1.y == p2.y - 3 && p1.x == p2.x + 3)) // one diagonal
				|| ((p1.y == p2.y + 3 && p1.x == p2.x + 3))) // one diagonal
        {
            float x1 = (p1.x == p2.x) ? p1.x : Math.Max(p1.x, p2.x) - 1;
            float x2 = (p1.x == p2.x) ? p1.x : Math.Max(p1.x, p2.x) - 2;
            float y1 = (p1.y == p2.y) ? p1.y : Math.Max(p1.y, p2.y) - 1;
            float y2 = (p1.y == p2.y) ? p1.y : Math.Max(p1.y, p2.y) - 2;
            if (((o1.x != x1 && o1.x != x2) || (o1.y != y1 && o1.y != y2)) &&
                ((o2.x != x1 && o2.x != x2) || (o2.y != y1 && o2.y != y2)) &&
                ((o3.x != x1 && o3.x != x2) || (o3.y != y1 && o3.y != y2))) return true;
        }
        return false;
	}

    public bool threePiecesOneSpaceBetween(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 o1, Vector2 o2, Vector2 o3) {
        //Check if there are 3 pieces are in same column without opponent pieces
        if (p1.y == p2.y && p2.y == p3.y &&
            (oneSpaceBetween(p1,p2,o1,o2,o3) || oneSpaceBetween(p2,p3,o1,o2,o3) || oneSpaceBetween(p1,p3,o1,o2,o3) ||
                twoSpaceBetween(p1,p2,o1,o2,o3) || twoSpaceBetween(p2,p3,o1,o2,o3) || twoSpaceBetween(p1,p3,o1,o2,o3))) {
            return true;
        }
        //Check if there are 3 pieces are in same tile without opponent pieces
        else if (p1.x == p2.x && p2.x == p3.x &&
            (oneSpaceBetween(p1,p2,o1,o2,o3) || oneSpaceBetween(p2,p3,o1,o2,o3) || oneSpaceBetween(p1,p3,o1,o2,o3)  ||
                twoSpaceBetween(p1,p2,o1,o2,o3) || twoSpaceBetween(p2,p3,o1,o2,o3) || twoSpaceBetween(p1,p3,o1,o2,o3))) {
            return true;
        }

        else if ((p1.x - p1.y == p2.x - p2.y && p1.x - p1.y == p3.x - p3.y)
            || (p1.x + p1.y == p2.x + p2.y && p1.x + p1.y == p3.x + p3.y)) {
            if (oneSpaceBetween(p1,p2,o1,o2,o3) || oneSpaceBetween(p2,p3,o1,o2,o3) || oneSpaceBetween(p1,p3,o1,o2,o3)  ||
                twoSpaceBetween(p1,p2,o1,o2,o3) || twoSpaceBetween(p2,p3,o1,o2,o3) || twoSpaceBetween(p1,p3,o1,o2,o3)) return true;
        }

        return false;
    }


	public bool checkVictory() {
        List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;

        int minZ = 5;
        int maxZ = -1;
        int minX = 5;
        int maxX = -1;
        for (int i = 0; i < 3; i++) {
            int z = (int) playerPieces[i].y;
            if (z < minZ)
                minZ = z;
            if (z > maxZ)
                maxZ = z;

            int x = (int) playerPieces[i].x;
            if (x < minX)
                minX = x;
            if (x > maxX)
                maxX = x;
        }
        //line
        if (maxZ - minZ == 2 && playerPieces[0].x == playerPieces[1].x && playerPieces[1].x == playerPieces[2].x) {
            return true;
        }
        //column
        if (maxX - minX == 2 && playerPieces[0].y == playerPieces[1].y && playerPieces[1].y == playerPieces[2].y){
            return true;
        }
        //diagonal
        if (maxZ - minZ != 2 || maxX - minX != 2)
            return false;
        for (int i = 0; i < 3; i++) {
            int z = (int) playerPieces[i].y;
            int x = (int) playerPieces[i].x;

            if (z == (minZ + maxZ) / 2 && x == (minX + maxX) / 2)
                return true;
        }

        return false;
    }

	public Vector2 CurrentPlayerPiece(int piece) {
		return (player == 0) ? blackPieces[piece] : whitePieces[piece];
	}

	public bool checkMove(Vector2 initial, Vector2 final) {
		List<Vector2> possibleMoves = this.PossibleMoves(initial);
		for (int i = 0; i < possibleMoves.Count; i++) {
			if (possibleMoves[i] == final) {
				return true;
			}
		}
		return false;
	}

    public bool checkMove(Vector2 final) {
        for (int i = 0; i < possibleMoves.Count; i++) {
			if (possibleMoves[i] == final) {
				return true;
			}
		}
		return false;
    }

    public void changePlayer() {
        player = (player + 1) % 2;
    }

    public bool movePiece(Vector2 final) {
        if (state == States.GAME_OVER) return false;
        if (!checkMove(final)) return false;
        List<Vector2> playerPieces = (player == 0) ? blackPieces : whitePieces;
        for (int i = 0; i < playerPieces.Count; i++) {
            if (playerPieces[i] == lastPiece)
                playerPieces[i] = final;
        }
        if (lastPieces.Count == 6) {
            lastPieces.RemoveAt(0);
            lastTiles.RemoveAt(0);
        }
        lastPieces.Add(lastPiece);
        lastTiles.Add(final);
        return true;
    }

	public void movePiece(Vector2 initial, Vector2 final) {
        if (state == States.GAME_OVER) return;
		for (int i = 0; i < 3; i++) {
			if (player == 0) {
				if (initial == blackPieces[i]){
					blackPieces[i] = final;
					break;
				}
			} else {
				if (initial == whitePieces[i]){
					whitePieces[i] = final;
					break;
				}
			}
		}
        if (lastPieces.Count == 6) {
            lastPieces.RemoveAt(0);
            lastTiles.RemoveAt(0);
        }
        lastPieces.Add(initial);
        lastTiles.Add(final);
	}

	public List<Vector2> PossibleMoves(Vector2 initial) {
        lastPiece = initial;
        possibleMoves = new List<Vector2>();
        possibleMoves.Add(moveUp(initial));
        possibleMoves.Add(moveDown(initial));
        possibleMoves.Add(moveRight(initial));
        possibleMoves.Add(moveLeft(initial));
        possibleMoves.Add(moveUpLeft(initial));
        possibleMoves.Add(moveUpRight(initial));
        possibleMoves.Add(moveDownLeft(initial));
        possibleMoves.Add(moveDownRight(initial));
        for (int i = 0; i < possibleMoves.Count;i++) {
            if (possibleMoves[i] == initial)
                possibleMoves.RemoveAt(i--);
        }
		return possibleMoves;
	}

    // Only use after making the play
    public bool tie() {
        if (lastPieces.Count < 6) return false;
        return ((lastPieces[0] == lastPieces[4] && lastTiles[0] == lastTiles[4]) // third to last move from oponent == last move
        && (lastPieces[1] == lastPieces[5] && lastTiles[1] == lastTiles[5])); // second to last move from playr == current move
    }
    public bool tileEmpty(Vector2 position) {
        if (position.x < 0 || position.y < 0 || position.x > 4 || position.y > 4)
            return false;
        for (int i = 0; i < 3; i++) {
            if (position == whitePieces[i]){
                return false;
            }
            if (position == blackPieces[i]){
                return false;
            }
        }
        return true;
    }

    public Vector2 moveUp(Vector2 position) {
        if(tileEmpty(position + new Vector2(-1, 0))) {
            return moveUp(position + new Vector2(-1, 0));
        } else {
            return position;
        }
    }
    public Vector2 moveDown(Vector2 position) {
        if(tileEmpty(position + new Vector2(+1, 0))) {
            return moveDown(position + new Vector2(+1, 0));
        } else {
            return position;
        }
    }
    public Vector2 moveRight(Vector2 position) {
        if(tileEmpty(position + new Vector2(0, 1))) {
            return moveRight(position + new Vector2(0, 1));
        } else {
            return position;
        }
    }
    public Vector2 moveLeft(Vector2 position) {
        if(tileEmpty(position + new Vector2(0, -1))) {
            return moveLeft(position + new Vector2(0, -1));
        } else {
            return position;
        }
    }
    public Vector2 moveUpLeft(Vector2 position) {
        if(tileEmpty(position + new Vector2(-1, -1))) {
            return moveUpLeft(position + new Vector2(-1, -1));
        } else {
            return position;
        }
    }
    public Vector2 moveUpRight(Vector2 position) {
        if(tileEmpty(position + new Vector2(-1, 1))) {
            return moveUpRight(position + new Vector2(-1, 1));
        } else {
            return position;
        }
    }
    public Vector2 moveDownLeft(Vector2 position) {
        if(tileEmpty(position + new Vector2(1, -1))) {
            return moveDownLeft(position + new Vector2(1, -1));
        } else {
            return position;
        }
    }
    public Vector2 moveDownRight(Vector2 position) {
        if(tileEmpty(position + new Vector2(1, 1))) {
            return moveDownRight(position + new Vector2(1, 1));
        } else {
            return position;
        }
    }


}