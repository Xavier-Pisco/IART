using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bot
{
	State state;
	PieceHandler pieceHandler;
	int heuristic;

	int depth;

	public Bot(State s, PieceHandler pieceHandler, int h, int depth)
	{
		this.state = s;
		this.pieceHandler = pieceHandler;
		this.heuristic = h;
		this.depth = depth;
	}

	public void Play()
	{
    	DateTime StartTime = DateTime.Now;
		//List<float> data = minimax(state, depth);
		List<float> data = alphabeta(state, depth, -10000, 10000);
		Vector2 bestInitial = new Vector2(data[0], data[1]);
		Vector2 bestFinal = new Vector2(data[2], data[3]);

		DateTime StopTime = DateTime.Now;

		TimeSpan elapsed = StopTime.Subtract(StartTime);
		Debug.Log("Game Time: " + elapsed);
		pieceHandler.movePiece(bestInitial, bestFinal);
	}

	public List<float> minimax(State newstate, int depth)
	{
		float bestResult = (newstate.player == 0) ? -10000 : 10000;
		List<float> bestdata = new List<float>{0,0,0,0,bestResult};

		if(depth == 0 || newstate.checkVictory()){
			bestdata[4] = newstate.Value(heuristic)  + (newstate.player == 0 ? depth : -depth);
			return bestdata;
		}

		for (int j = 0; j < 3; j++)
		{
			Vector2 initial = newstate.CurrentPlayerPiece(j);
			List<Vector2> possibleFinal = newstate.PossibleMoves(initial);
			for (int i = 0; i < possibleFinal.Count; i++)
			{

				State temp = newstate.deepCopy();
				temp.movePiece(initial, possibleFinal[i]);
				if(temp.player == 0){
					temp.player = 1;
				}else{
					temp.player = 0;
				}
				List<float> data = minimax(temp, depth - 1);
				if (newstate.player == 0){
					if(data[4] > bestdata[4]){
						bestdata[0] = initial[0];
						bestdata[1] = initial[1];
						bestdata[2] = possibleFinal[i][0];
						bestdata[3] = possibleFinal[i][1];
						bestdata[4] = data[4];
					}
				}else{
					if(data[4] < bestdata[4]){
						bestdata[0] = initial[0];
						bestdata[1] = initial[1];
						bestdata[2] = possibleFinal[i][0];
						bestdata[3] = possibleFinal[i][1];
						bestdata[4] = data[4];
					}
				}

			}
		}

		return bestdata;
	}

	public List<float> alphabeta(State newstate, int depth, float alpha, float beta)
	{
		float bestResult = (newstate.player == 0) ? -10000 : 10000;
		List<float> bestdata = new List<float>{0,0,0,0,bestResult};

		if(depth == 0 || newstate.checkVictory()){
			bestdata[4] = newstate.Value(heuristic)  + (newstate.player == 0 ? depth : -depth);
			return bestdata;
		}

		for (int j = 0; j < 3; j++)
		{
			Vector2 initial = newstate.CurrentPlayerPiece(j);
			List<Vector2> possibleFinal = newstate.PossibleMoves(initial);
			for (int i = 0; i < possibleFinal.Count; i++)
			{
				State temp = newstate.deepCopy();
				temp.movePiece(initial, possibleFinal[i]);
				if(temp.player == 0){
					temp.player = 1;
				}else{
					temp.player = 0;
				}
				List<float> data = alphabeta(temp, depth - 1, alpha, beta);
				if (newstate.player == 0){
					if(data[4] > bestdata[4]){
						bestdata[0] = initial[0];
						bestdata[1] = initial[1];
						bestdata[2] = possibleFinal[i][0];
						bestdata[3] = possibleFinal[i][1];
						bestdata[4] = data[4];
						alpha = Math.Max(alpha, bestdata[4]);
						if (alpha >= beta) {
							return bestdata;
						}
					}
				}else{
					if(data[4] < bestdata[4]){
						bestdata[0] = initial[0];
						bestdata[1] = initial[1];
						bestdata[2] = possibleFinal[i][0];
						bestdata[3] = possibleFinal[i][1];
						bestdata[4] = data[4];
						beta = Math.Min(beta, bestdata[4]);
						if (alpha >= beta) {
							return bestdata;
						}
					}
				}
			}
		}
		return bestdata;
	}
}