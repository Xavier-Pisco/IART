from abc import ABC, abstractmethod

class SearchNode:
	def __init__(self, state, parent, cost):
		self.state = state
		self.parent = parent
		if (parent != None):
			self.cost = parent.getCost() + cost
			self.numSteps = parent.getSteps() + 1
		else:
			self.cost = cost
			self.numSteps = 0

	def getCost(self):
		return self.cost

	def getSteps(self):
		return self.numSteps

	def getState(self):
		return self.state

	def compareAncient(self, node):
		if (self == node):
			return True
		elif (self.parent != None):
			return self.parent.compareAncient(node)

	def print(self):
		if (self.parent != None):
			self.parent.print()
			self.state.print()

	def __eq__(self, node):
		if (node == None):
			return False
		return self.state == node.state

class State(ABC):
	@abstractmethod
	def getAllChildren(self):
		pass

	@abstractmethod
	def checkFinalState(self):
		pass

	@abstractmethod
	def print(self):
		pass

	@abstractmethod
	def __eq__(self, object):
		pass

class StateWithCost(State):
	@abstractmethod
	def estimateCost(self):
		pass


def breathFirstSearch(queue):
	initial = queue.pop(0)
	children = initial.getState().getAllChildren()
	for i in range(0, len(children)):
		node = SearchNode(children[i], initial, 1)
		if (children[i].checkFinalState()):
			return node
		if not(initial.compareAncient(node)):
			queue.append(node)
	return breathFirstSearch(queue)

def depthFirstSearch(initial, limit):
	if (initial.getState().checkFinalState()):
		return initial
	if (limit == 0):
		return None
	children = initial.getState().getAllChildren()
	for i in range(0, len(children)):
		node = SearchNode(children[i], initial, 1)
		if not(initial.compareAncient(node)):
			result = depthFirstSearch(node, limit - 1)
			if (result != None):
				return result

def iterativeDeepeningSearch(initial):
	limit = 0
	result = None
	while(result == None):
		result = depthFirstSearch(initial, limit)
		limit += 1
	return result

def greedySearch(list):
	bestCost = float('inf')
	bestNode = None
	for i in range(len(list)):
		if (list[i].getState().checkFinalState()):
			return list[i]
		cost = list[i].getState().estimateCost()
		if (cost < bestCost):
			bestCost = cost
			bestNode = list[i]
	list.remove(bestNode)
	children = bestNode.getState().getAllChildren()
	for i in range(len(children)):
		child = SearchNode(children[i], bestNode, 1)
		if not(bestNode.compareAncient(child)):
			list.append(child)
	return greedySearch(list)

def aStarAlgorithm(list):
	bestCost = float('inf')
	bestNode = None
	for i in range(len(list)):
		if (list[i].getState().checkFinalState()):
			return list[i]
		cost = list[i].getCost() + list[i].getState().estimateCost()
		if (cost < bestCost):
			bestCost = cost
			bestNode = list[i]
	list.remove(bestNode)
	children = bestNode.getState().getAllChildren()
	for i in range(len(children)):
		child = SearchNode(children[i], bestNode, 1)
		if not(bestNode.compareAncient(child)):
			list.append(child)
	return aStarAlgorithm(list)

