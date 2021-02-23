from dataclasses import dataclass
from copy import deepcopy

@dataclass
class Bucket:
	max: int
	val: int


size1 = input("First container size: ")
size2 = input("Second container size: ")

b1 = Bucket(int(size1), 0)
b2 = Bucket(int(size2), 0)

b1.val = 0
b2.val = 0

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
	(b1, b2) = queue.pop(0)
	i = 0
	while(i < 6):
		bucket1 = deepcopy(b1)
		bucket2 = deepcopy(b2)
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
		if (checkState(bucket1, value)):
			print("Done")
			return
		print(bucket1, bucket2)
		if (executed == True):
			queue.append((bucket1, bucket2))
		i += 1
	breathFirstSearch()

def depthFirstSearch(b1, b2, limit):
	if (checkState(b1, value)):
		print("Done")
		return True
	if (limit == 0):
		return False
	bucket1 = deepcopy(b1)
	bucket2 = deepcopy(b2)

	print(bucket1, bucket2)
	i = 0
	while(i < 6):
		bucket1 = deepcopy(b1)
		bucket2 = deepcopy(b2)
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
		if (depthFirstSearch(bucket1, bucket2, limit - 1)):
			return True
		i += 1
	return False

def iterativeDeepeningSearch():
	limit = 0
	while(depthFirstSearch(b1, b2, limit) == False):
		limit += 1

'''
queue.append((b1,b2))
breathFirstSearch()
'''
'''
depthFirstSearch(b1, b2, 5)
'''
iterativeDeepeningSearch()