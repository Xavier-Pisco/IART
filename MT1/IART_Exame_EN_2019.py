from random import randint
import math

def dist(place1, place2):
	return (math.sqrt((place1[0] - place2[0])*(place1[0] - place2[0])) + math.sqrt((place1[1] - place2[1])*(place1[1] - place2[1])))

def value(solution):
	result = dist(fab1, clientes[solution[0]]) + dist(fab2, clientes[solution[5]])
	for i in range(3):
		result += dist(clientes[solution[i]], clientes[solution[i + 1]])
	return result

def neighbour(solution):
	position = randint(0, 4)
	(solution[position], solution[position + 1]) = (solution[position + 1], solution[position])
	return solution

def hill_climbing():
	solution = [0,1,2,3,4,5]
	for i in range(100):
		nei = neighbour(solution)
		if (value(solution) < value(nei)):
			 solution = nei
	return solution

fab1 = [1, 1]
fab2 = [10, 10]
clientes = [[8,8], [4,4], [3,3], [7,7], [2,2], [9,9]]
solution = hill_climbing()
print(solution, value(solution))

fab1 = [1,1]
fab2 = [10, 10]
clientes = [[2,2], [2,8], [6,6], [1,6], [10,5], [5,8]]
solution = hill_climbing()
print(solution, value(solution))

fab1 = [1,1]
fab2 = [5, 2]
clientes = [[2,8], [1,4], [6,6], [6,1], [1,6], [5,8]]
solution = hill_climbing()
print(solution, value(solution))