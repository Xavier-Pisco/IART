using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceHandler : MonoBehaviour
{
    public Material selected;
    public Material boardLight;
    public Material boardDark;

    GameObject[] whitePieces;
    GameObject[] blackPieces;
    GameObject[] tiles;

    Piece lastClickedPiece = null;

    State s;
    Attributes.GameType gameType = Attributes.GetGameType();
    Bot blackBot;
    Bot whiteBot;

    void Start()
    {
        blackPieces = GameObject.FindGameObjectsWithTag("BlackPiece");
        whitePieces = GameObject.FindGameObjectsWithTag("WhitePiece");
        GameObject[] tempTiles = GameObject.FindGameObjectsWithTag("Tile");
        sortTiles(tempTiles);

        createState();
        createBot();
    }

    private void sortTiles(GameObject[] tempTiles) {
        tiles = new GameObject[25];
        for (int i = 0; i < 25; i++) {
            int x = (int)tempTiles[i].transform.position.x;
            int y = (int)tempTiles[i].transform.position.z;
            tiles[x*5 + y] = tempTiles[i];
        }
    }

    private void createState() {
        s = new State(whitePieces[0].transform.position, whitePieces[1].transform.position,
                        whitePieces[2].transform.position, blackPieces[0].transform.position,
                        blackPieces[1].transform.position, blackPieces[2].transform.position,
                        0);
    }

    private void createBot() {
        if (gameType == Attributes.GameType.PvC)
            whiteBot = new Bot(s, this, Attributes.heuristic, Attributes.depth);
        else if (gameType == Attributes.GameType.CvC) {
            blackBot = new Bot(s, this, Attributes.heuristic, Attributes.depth);
            whiteBot = new Bot(s, this, Attributes.heuristic2, Attributes.depth2);
            Invoke("invokeBot", 0.5f);
        }

    }

    public void pieceClicked(Piece p) {
        if ((p.CompareTag("BlackPiece") && this.s.player == 1) || (p.CompareTag("WhitePiece") && this.s.player == 0))
            return;
        if (this.s.state == State.States.GAME_OVER) {
            return;
        }

        for (int i = 0; i < 25; i++) {
            tiles[i].GetComponent<MeshRenderer>().material = (i % 2 == 0) ? boardDark : boardLight;
        }

        this.s.state = State.States.PIECE_CLICKED;
        this.lastClickedPiece = p;
        List<Vector2> stateTiles = s.PossibleMoves(new Vector2(p.transform.position.x, p.transform.position.z));
        for (int i = 0; i < stateTiles.Count; i++) {
            int x = (int) stateTiles[i].x;
            int y = (int) stateTiles[i].y;
            this.tiles[x*5 + y].GetComponent<MeshRenderer>().material = selected;
        }
    }

    public void tileClicked(Tile t) {
        for (int i = 0; i < 25; i++) {
            tiles[i].GetComponent<MeshRenderer>().material = (i % 2 == 0) ? boardDark : boardLight;
        }
        if (this.s.state != State.States.PIECE_CLICKED)
            return;
        this.s.state = State.States.TILE_CLICKED;
        if (this.s.movePiece(new Vector2(t.transform.position.x, t.transform.position.z))) {
            this.lastClickedPiece.transform.localPosition = t.transform.position;
            if (this.s.checkVictory()) {
                Debug.Log(this.s.player + " wins");
                this.s.state = State.States.GAME_OVER;
            } else if (this.s.tie()) {
                Debug.Log("tie");
                this.s.state = State.States.GAME_OVER;
            } else {
                this.s.changePlayer();
                Invoke("invokeBot", 0.5f);
                this.s.state = State.States.IDLE;
            }
        }
    }

    public void movePiece(Vector2 initial, Vector2 final) {
        if (this.s.state == State.States.GAME_OVER) return;
        GameObject[] playerPieces = (s.player == 0) ? blackPieces : whitePieces;

        for (int i = 0; i < 3; i++) {
            if (initial.x == playerPieces[i].transform.position.x && initial.y == playerPieces[i].transform.position.z) {
                this.pieceClicked(playerPieces[i].GetComponent<Piece>());
                this.tileClicked(tiles[(int)(final.x * 5 + final.y)].GetComponent<Tile>());
                break;
            }
        }
    }

    private void invokeBot() {
        if (this.gameType == Attributes.GameType.PvP) return;
        if (this.gameType == Attributes.GameType.PvC && s.player == 1) whiteBot.Play();
        else if (this.gameType == Attributes.GameType.CvC) {
            if (s.player == 0) blackBot.Play();
            else whiteBot.Play();
        }
    }
}
