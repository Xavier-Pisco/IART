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
