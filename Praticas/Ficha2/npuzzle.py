from copy import deepcopy

import sys
sys.path.append('../')
sys.setrecursionlimit(10000)

from searchs import *

class Board:
	def __init__(self, board, operator):
		(matrix, x, y) = board
		self.matrix = matrix
		self.n = len(matrix) - 1
		self.x = x
		self.y = y
		self.operator = operator

	def get(self, x, y):
		return self.matrix[y][x]

	def set(self, x, y, value):
		self.matrix[y][x] = value

	def getAllChildren(self):
		children = []
		i = 0
		while(i < 4):
			temp = deepcopy(self)
			child = None
			if (i == 0):
				if (up(temp)):
					child = Board((temp.matrix, temp.x, temp.y), "Up")
			elif (i == 1):
				if (down(temp)):
					child = Board((temp.matrix, temp.x, temp.y), "Down")
			elif (i == 2):
				if (left(temp)):
					child = Board((temp.matrix, temp.x, temp.y), "Left")
			elif (i == 3):
				if (right(temp)):
					child = Board((temp.matrix, temp.x, temp.y), "Right")
			if (child != None):
				children.append(child)
			i += 1
		return children

	def checkFinalState(self):
		number = 1
		for i in range(len(self.matrix)):
			for j in range(len(self.matrix[i])):
				if (i == len(self.matrix) - 1 and j == len(self.matrix[i]) - 1):
					return True
				if (self.matrix[i][j] != number):
					return False
				number += 1
		return True

	def estimateCost(self):
		estimate = 0
		for i in range(len(self.matrix)*len(self.matrix)):
			number = self.matrix[i//len(self.matrix)][i%len(self.matrix)]
			if (number != i + 1 and number != 0):
				estimate += 1
		return estimate

	def print(self):
		print(self.operator)

	def __eq__(self, b):
		if (b == None):
			return False
		for i in range(len(self.matrix)):
			for j in range(len(self.matrix[i])):
				if (self.matrix[i][j] != b.matrix[i][j]):
					return False
		return True

def up(board):
	x = board.x
	y = board.y
	if (y > 0):
		board.set(x, y, board.get(x, y-1))
		board.set(x, y-1, 0)
		board.x = x
		board.y = y-1
		return True
	return False

def down(board):
	x = board.x
	y = board.y
	if (y < board.n):
		board.set(x, y, board.get(x, y+1))
		board.set(x, y+1, 0)
		board.x = x
		board.y = y+1
		return True
	return False

def left(board):
	x = board.x
	y = board.y
	if (x > 0):
		board.set(x, y, board.get(x-1, y))
		board.set(x-1, y, 0)
		board.x = x-1
		board.y = y
		return True
	return False

def right(board):
	x = board.x
	y = board.y
	if (x < board.n):
		board.set(x, y, board.get(x+1, y))
		board.set(x+1, y, 0)
		board.x = x+1
		board.y = y
		return True
	return False

matrix1 = ([[1,2,3],[5,0,6],[4,7,8]], 1, 1)
matrix2 = ([[1,3,6],[5,2,0],[4,7,8]], 2, 1)
matrix3 = ([[1,6,2],[5,7,3],[0,4,8]], 0, 2)
matrix4 = ([[5,1,3,4],[2,0,7,8],[10,6,11,12],[9,13,14,15]], 1, 1)
initial = SearchNode(Board(matrix4,None), None, 0)
queue = [initial]

#breathFirstSearch(queue).print()
#aStarAlgorithm(queue).print()