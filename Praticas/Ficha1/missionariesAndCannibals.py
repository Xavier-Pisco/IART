from copy import deepcopy

import sys
sys.path.append('../')

from searchs import *

class Person:
	def __init__(self, side, type):
		self.side = side
		self.type = type

	def __eq__(self, p):
		if (p == None):
			return False
		return (self.side == p.side and self.type == p.type)

class Voyage:
	def __init__(self, people, boatSide, operator):
		self.people = people
		self.boatSide = boatSide
		self.operator = operator

	def getAllChildren(self):
		children = []
		i = 0
		message = "To initial side: " if self.boatSide == True else "To final side: "
		while (i < 5):
			temp = deepcopy(self.people)
			(missionary1, missionary2) = get2PeopleOnSide(self.boatSide, "missionary", temp)
			(cannibal1, cannibal2) = get2PeopleOnSide(self.boatSide, "cannibal", temp)
			child = None
			if (i == 0):
				if (missionary1 != None):
					move(self.boatSide, missionary1)
					child = Voyage(temp, not(self.boatSide), message + " 1 missionary")
			elif (i == 1):
				if (missionary1 != None and missionary2 != None):
					move(self.boatSide, missionary1, missionary2)
					child = Voyage(temp, not(self.boatSide), message + " 2 missionaries")
			elif (i == 2):
				if (cannibal1 != None):
					move(self.boatSide, cannibal1)
					child = Voyage(temp, not(self.boatSide), message + " 1 cannibal")
			elif (i == 3):
				if (cannibal1 != None and cannibal2 != None):
					move(self.boatSide, cannibal1, cannibal2)
					child = Voyage(temp, not(self.boatSide), message + " 2 cannibals")
			elif (i == 4):
				if (cannibal1 != None and missionary1 != None):
					move(self.boatSide, cannibal1, missionary1)
					child = Voyage(temp, not(self.boatSide), message + " 1 missionary and 1 cannibal")
			i += 1
			if (child != None and child.checkValidState()):
				children.append(child)
		return children

	def checkFinalState(self):
		for i in range(len(self.people)):
			if (self.people[i].side == False):
				return False
		return True

	def checkValidState(self):
		missionaryInitial = 0
		missionaryFinal = 0
		cannibalInitial = 0
		cannibalFinal = 0
		for person in self.people:
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

	def print(self):
		print(self.operator)

	def __eq__(self, b):
		if (b == None):
			return False
		initialMissionary = 0
		initialCannibal = 0
		for i in range(len(self.people)):
			if (self.people[i] != b.people[i]):
				return False
		return True



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

m1 = Person(False, "missionary")
m2 = Person(False, "missionary")
m3 = Person(False, "missionary")
c1 = Person(False, "cannibal")
c2 = Person(False, "cannibal")
c3 = Person(False, "cannibal")

people = [m1,m2,m3,c1,c2,c3]
initial = SearchNode(Voyage(people, False, None), None, 0)

queue = [initial]

#breathFirstSearch(queue).print()

#depthFirstSearch(initial, 15).print()

#iterativeDeepeningSearch(initial).print()