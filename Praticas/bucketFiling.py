from dataclasses import dataclass
from copy import deepcopy

@dataclass
class Bucket:
	max: int
	val: int

class Node:
	def __init__(self, bucket1, bucket2, parent):
		self.b1 = bucket1
		self.b2 = bucket2
		self.parent = parent
		self.children = []

	def addChild(self, bucket1, bucket2):
		self.children.append(Node(bucket1, bucket2))

	def addChild(self, node):
		self.children.append(node)

	def print(self):
		if (self.parent != None):
			self.parent.print()
			print(self.b1, self.b2)


def fill(bucket):
	if (bucket.val < bucket.max):
		bucket.val = bucket.max
		return True
	else:
		return False

def empty(bucket):
	if (bucket.val > 0):
		bucket.val = 0
		return True
	else:
		return False

def pour(bucket1, bucket2):
	if (bucket1.val > 0 and bucket2.val < bucket2.max):
		if (bucket1.val <= bucket2.max - bucket2.val):
			bucket2.val += bucket1.val
			bucket1.val = 0
		else:
			bucket1.val -= bucket2.max - bucket2.val
			bucket2.val = bucket2.max
		return True
	else:
		return False

def checkState(bucket1, value):
	return (bucket1.val == value)

queue = []
value = 2

def breathFirstSearch():
	initial = queue.pop(0)
	i = 0
	while(i < 6):
		bucket1 = deepcopy(initial.b1)
		bucket2 = deepcopy(initial.b2)
		executed = True
		if (i == 0):
			executed = fill(bucket1)
		elif (i == 1):
			executed = fill(bucket2)
		elif (i == 2):
			executed = empty(bucket1)
		elif (i == 3):
			executed = empty(bucket2)
		elif (i == 4):
			executed = pour(bucket1, bucket2)
		elif (i == 5):
			executed = pour(bucket2, bucket1)

		node = Node(bucket1, bucket2, initial)
		initial.addChild(node)

		if (checkState(bucket1, value)):
			node.print()
			return
		if (executed == True):
			queue.append(node)
		i += 1
	breathFirstSearch()

def depthFirstSearch(initial, limit):
	if (checkState(initial.b1, value)):
		initial.print()
		return True
	if (limit == 0):
		return False
	i = 0
	while(i < 6):
		bucket1 = deepcopy(initial.b1)
		bucket2 = deepcopy(initial.b2)
		if (i == 0):
			if not(fill(bucket1)):
				i += 1
				continue
		elif (i == 1):
			if not(fill(bucket2)):
				i += 1
				continue
		elif (i == 2):
			if not(empty(bucket1)):
				i += 1
				continue
		elif (i == 3):
			if not(empty(bucket2)):
				i += 1
				continue
		elif (i == 4):
			if not(pour(bucket1, bucket2)):
				i += 1
				continue
		elif (i == 5):
			if not(pour(bucket2, bucket1)):
				i += 1
				continue
		node = Node(bucket1, bucket2, initial)
		initial.addChild(node)
		if (depthFirstSearch(node, limit - 1)):
			return True
		i += 1
	return False

def iterativeDeepeningSearch(initial):
	limit = 0
	while(depthFirstSearch(initial, limit) == False):
		limit += 1


size1 = input("First container size: ")
size2 = input("Second container size: ")

b1 = Bucket(int(size1), 0)
b2 = Bucket(int(size2), 0)

initial = Node(b1, b2, None)
queue.append(initial)

#breathFirstSearch()

#depthFirstSearch(initial, 10)

#iterativeDeepeningSearch(initial)
