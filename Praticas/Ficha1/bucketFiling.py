from dataclasses import dataclass
from copy import deepcopy

import sys
sys.path.append('../')

from searchs import *

@dataclass
class Bucket:
	max: int
	val: int

class BucketFill(State):
	def __init__(self, bucket1, bucket2, operator):
		self.b1 = bucket1
		self.b2 = bucket2
		self.operator = operator

	def getAllChildren(self):
		chidlren = []
		i = 0
		while(i < 6):
			bucket1 = deepcopy(self.b1)
			bucket2 = deepcopy(self.b2)
			node = None
			if (i == 0):
				fill(bucket1)
				node = BucketFill(bucket1, bucket2, "Fill bucket1")
			elif (i == 1):
				fill(bucket2)
				node = BucketFill(bucket1, bucket2, "Fill bucket2")
			elif (i == 2):
				empty(bucket1)
				node = BucketFill(bucket1, bucket2, "Empty bucket1")
			elif (i == 3):
				empty(bucket2)
				node = BucketFill(bucket1, bucket2, "Empty bucket2")
			elif (i == 4):
				pour(bucket1, bucket2)
				node = BucketFill(bucket1, bucket2, "Pour bucket1 > bucket2")
			elif (i == 5):
				pour(bucket2, bucket1)
				node = BucketFill(bucket1, bucket2, "Pour bucket2 > bucket1")
			chidlren.append(node)
			i += 1
		return chidlren

	def checkFinalState(self):
		return self.b1.val == value

	def print(self):
		print(self.operator)

	def __eq__(self, b):
		if (b == None):
			return False
		return (self.b1 == b.b1 and self.b2 == b.b2)


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

size1 = input("First container size: ")
size2 = input("Second container size: ")

b1 = Bucket(int(size1), 0)
b2 = Bucket(int(size2), 0)

value = 2
initial = SearchNode(BucketFill(b1, b2, None), None, 0)
queue = []
queue.append(initial)

#breathFirstSearch(queue).print()

#depthFirstSearch(initial, 6).print()

#iterativeDeepeningSearch(initial).print()
