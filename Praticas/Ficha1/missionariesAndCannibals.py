from dataclasses import dataclass
from copy import deepcopy

@dataclass
class Person:
	side: bool # True if at end side of the river
	type: str # missionary or cannibal

class Voyage:
	def __init__(self, person1, person2, destination):
		self.person1 = person1
		self.person2 = person2
		self.destination = destination
	def print(self):
		if (self.destination == True):
			if (self.person2 == None):
				print("To final side: " + self.person1.type)
			else:
				print("To final side: " + self.person1.type + " and " + self.person2.type)
		else:
			if (self.person2 == None):
				print("To inital side: " + self.person1.type)
			else:
				print("To initial side: " + self.person1.type + " and " + self.person2.type)

class Node:
	def __init__(self, voyage, parent):
		self.voyage = voyage
		self.parent = parent
		self.children = []

	def addChild(self, node):
		self.children.append(node)

	def print(self):
		if (self.parent != None):
			self.parent.print()
			self.voyage.print()

def move(boatSide, person1, person2=None):
	if (person2 == None):
		if (person1.side != boatSide):
				return False
		if (person1.side == False):
			person1.side = True
		else:
			person1.side = False
		return True
	else:
		if (person1.side != boatSide or person2.side != boatSide):
			return False
		if (person1.side == False and person2.side == False):
			person1.side = True
			person2.side = True
		else:
			person1.side = False
			person2.side = False
		return True

def checkFinalState(people):
	for person in people:
		if (person.side == False):
			return False
	return True

def checkValidState(people):
	missionaryInitial = 0
	missionaryFinal = 0
	cannibalInitial = 0
	cannibalFinal = 0
	for person in people:
		if (person.type == "missionary"):
			if (person.side == False):
				missionaryInitial += 1
			else:
				missionaryFinal += 1
		else:
			if (person.side == False):
				cannibalInitial += 1
			else:
				cannibalFinal += 1
	return not((missionaryInitial != 0 and cannibalInitial > missionaryInitial) or (missionaryFinal != 0 and cannibalFinal > missionaryFinal))

def getPeopleOnSide(side, people):
	result = []
	for person in people:
		if (person.side == side):
			result.append(person)
	return result

def get2PeopleOnSide(side, type, people):
	person1 = None
	person2 = None
	for person in people:
		if (person.side == side and person.type == type):
			if (person1 == None):
				person1 = person
			else:
				person2 = person
				return (person1, person2)
	return (person1, person2)

# Removes same exact voyages back and forth
# Ex: 1 cannibal goes, 1 cannibal comes back, 1 cannibal goes ...
def equalNodes(node1, node2):
	if (node1.voyage == None or node2.voyage == None):
		return False
	if (node1.voyage.person1 != None and node2.voyage.person1 != None
		and node1.voyage.person1.type == node2.voyage.person1.type):
		if ((node1.voyage.person2 == None and node2.voyage.person2 == None) or
				(node1.voyage.person2 != None and node2.voyage.person2 != None
				and node1.voyage.person2.type == node2.voyage.person2.type)):
			return True

def breathFirstSearch():
	(initial, people) = queue.pop(0)
	boatSide = initial.voyage.destination
	i = 0
	while (i < 5):
		temp = deepcopy(people)
		(missionary1, missionary2) = get2PeopleOnSide(boatSide, "missionary", temp)
		(cannibal1, cannibal2) = get2PeopleOnSide(boatSide, "cannibal", temp)
		node = None
		if (i == 0):
			if (missionary1 != None):
				move(boatSide, missionary1)
				node = Node(Voyage(missionary1, None, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 1):
			if (missionary1 != None and missionary2 != None):
				move(boatSide, missionary1, missionary2)
				node = Node(Voyage(missionary1, missionary2, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 2):
			if (cannibal1 != None):
				move(boatSide, cannibal1)
				node = Node(Voyage(cannibal1, None, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 3):
			if (cannibal1 != None and cannibal2 != None):
				move(boatSide, cannibal1, cannibal2)
				node = Node(Voyage(cannibal1, cannibal2, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 4):
			if (cannibal1 != None and missionary1 != None):
				move(boatSide, cannibal1, missionary1)
				node = Node(Voyage(missionary1, cannibal1, not(boatSide)), initial)
				initial.addChild(node)
		i += 1
		if (checkFinalState(temp)):
			node.print()
			return
		if (checkValidState(temp) and node != None and not(equalNodes(node, initial))):
			queue.append((node, temp))
	breathFirstSearch()

def depthFirstSearch(initial, people, limit):
	if (checkFinalState(people)):
		initial.print()
		return True
	if (limit == 0):
		return False
	boatSide = initial.voyage.destination
	i = 0
	while(i < 5):
		temp = deepcopy(people)
		(missionary1, missionary2) = get2PeopleOnSide(boatSide, "missionary", temp)
		(cannibal1, cannibal2) = get2PeopleOnSide(boatSide, "cannibal", temp)
		if (missionary1 == None and missionary2 == None and cannibal1 == None and cannibal2 == None):
			return False
		node = None
		if (i == 0):
			if (missionary1 != None):
				move(boatSide, missionary1)
				node = Node(Voyage(missionary1, None, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 1):
			if (missionary1 != None and missionary2 != None):
				move(boatSide, missionary1, missionary2)
				node = Node(Voyage(missionary1, missionary2, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 2):
			if (cannibal1 != None):
				move(boatSide, cannibal1)
				node = Node(Voyage(cannibal1, None, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 3):
			if (cannibal1 != None and cannibal2 != None):
				move(boatSide, cannibal1, cannibal2)
				node = Node(Voyage(cannibal1, cannibal2, not(boatSide)), initial)
				initial.addChild(node)
		elif (i == 4):
			if (cannibal1 != None and missionary1 != None):
				move(boatSide, cannibal1, missionary1)
				node = Node(Voyage(missionary1, cannibal1, not(boatSide)), initial)
				initial.addChild(node)
		i += 1
		if (checkValidState(temp) and node != None and not(equalNodes(initial, node))):
			if (depthFirstSearch(node, temp, limit - 1)):
				return True

def iterativeDeepeningSearch(initial, people):
	limit = 0
	while not(depthFirstSearch(initial, people, limit)):
		limit += 1
	print(limit)

m1 = Person(False, "missionary")
m2 = Person(False, "missionary")
m3 = Person(False, "missionary")
c1 = Person(False, "cannibal")
c2 = Person(False, "cannibal")
c3 = Person(False, "cannibal")

people = [m1,m2,m3,c1,c2,c3]
initial = Node(Voyage(None, None, False), None)

queue = [(initial, people)]

#breathFirstSearch()

#depthFirstSearch(initial, people, 15)

#iterativeDeepeningSearch(initial, people)