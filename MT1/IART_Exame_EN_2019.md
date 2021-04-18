# Exame 2019

## Exercício 1

Assumir custo m3 = 3

### 1.1

* **Representção de um estado**:
  * [X1,X2,X3,X4,X5,X6,X7]
  * X1: V/B/x
  * X2: V/B/x
  * X3: V/B/x
  * X4: V/B/x
  * X5: V/B/x
  * X6: V/B/x
  * X7: V/B/x

* **Estado Inicial**:
  * [V,V,V,x,B,B,B]

* **Teste Objetivo**:
  * [B,B,B,x,V,V,V]

* **Operadores**
  * V1:
    * **Precondições**: i < 7, State[i] = V, State[i+1] = x
    * **Efeitos**: State[i] = x, State[i+1] = V
    * **Custo**: 1
  * B1:
    * **Precondições**: i > 1, State[i] = B, State[i-1] = x
    * **Efeitos**: State[i] = x, State[i-1] = B
    * **Custo**: 1
  * V2:
    * **Precondições**: i < 6, State[i] = V, State[i+1] = V / B, State[i+2] = x
    * **Efeitos**: State[i] = x, State[i+2] = V
    * **Custo**: 2
  * B2:
    * **Precondições**: i > 2, State[i] = B, State[i+1] = V / B, State[i+2] = x
    * **Efeitos**: State[i] = x, State[i-2] = B
    * **Custo**: 2
  * V3:
    * **Precondições**: i < 5, State[i] = V, State[i+1] = V / B, State[i+2] = V / B, State[i+3] = x
    * **Efeitos**: State[i] = x, State[i+2] = V
    * **Custo**: 2
  * B3:
    * **Precondições**: i > 3, State[i] = B, State[i+1] = V / B, State[i+2] = V / B, State[i+3] = x
    * **Efeitos**: State[i] = x, State[i+2] = B
    * **Custo**: 2

* **Função de custo**: Soma do custo de todas os movimentos.

### 1.2

* **h1**:
  * Admissível: Como a heurstíca vale 0, o custo estimado para chegar ao fim da solução vai ser sempre 0 e, por isso, é sempre menor que o valor real.
* **h2**:
  * Não admissível: No caso de faltar apenas uma jogada (e.g. [B,B,B,V,V,V,x]) o custo estimado é superior ao valor real.
* **h3**:
  * Admissível: No caso inicial a heurística tem valor 6 e o número de jogadas necessárias é maior que 6. Numa posição anterior a uma final (e.g [B,B,x,B,V,V,V]) a heurística vai ter sempre um valor 0 e é sempre menor que o custo.

### 1.3

* **h4**:
  * **Escolha**: Como o custo de uma jogada é sempre igual è distância da peça à casa para onde vai, podemos utilizar uma heurística que seja a soma de todas as distâncias das peças que não estão numa casa final à casa do estado objetivo mais no centro, ou seja, distância de uma peça vermelha à casa 5 e de uma peça branca à casa 3.
  * **Cálculo**: Para todas as posições, se a posição for menor que 5 e estiver ocupada por uma peça vermelha, somar 5 - posição. Se a posição for maior que 3 e estiver ocupada por uma peça branca, somar posição - 3.
  * **Pseudo-código**:
	```
	result = 0
	for (posição em [1,7]):
		if (posição < 5 and State[posição] = V):
			result += 5 - posição
		if (posição > 3 and State[posição] = B):
			result += posição - 3
	return result
	```

### 1.4

##### a)

[B B V B V V x]

[B B V B x V V] [B B V B V x V]

[B B x B V V V]

[B B B x V V V]

##### b)

[B B V B V V x]

[B B V B x V V] [B B V B V x V]

[B B x B V V V] [B B x B V V V] [B B V B x V V]

[B B B x V V V]

## Exercício 2

### 2.1

- **Definição do problema**:
  - A posição de cada fábrica ou cliente é dada pelas cordenadas (x,y) e a distância corresponde à distância cartesiana entre eles.
- **Solução do problema**
  - Lista com os números de cada cliente ordenados pela ordem que ocorre a visita. (e.g. [2,4,3,1,5,6]).
- **Função de avaliação**
  - A função avaliação vai ser a soma das distâncias entre todos os clientes consecutivos com a distância entre a Fábrica1 e o 1º cliente da lista e a distância entre o último cliente e a Fábrica2.
	```
	result = dist(fabrica1, solução[1]) + dist(fabrica2, solução[n])
	for (i in [1, n-1]):
		result += dist(solução[i], solução[i+1])
	```

### 2.2

Trocar a posição de dois clientes consecutivos, de modo a, na nova solução, ir primeiro ao cliente que visitava mais tarde na solução anterior.

```
posição = random(1, n-1)
temp = solução[posição]
solução[posição] = solução[posição + 1]
solução[posição + 1] = solução[posição]
```

### 2.3

**Basic Hill-Climbing**

```
solução = [1,2,3,4,5,6]
for (i in [1, 100]):
	vizinho <- vizinho aleatório da solução
	if (avaliar(solução) < avaliar(vizinho)) solução = vizinho
return solução
```


### 2.4

```python
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
```

Resultados:
- 1:
  - Custo: 34.0
  - Solução: [5, 3, 1, 4, 2, 0]
- 2:
  - Custo: 43.0
  - Solução: [5, 0, 2, 3, 4, 1]
- 3:
  - Custo: 25.0
  - Solução: [4, 5, 2, 3, 0, 1]

## Exercício 3

### 3.1

É possível pois o jogo do galo tem um espaço de estados reduzido. O agente escolheria a melhor jogada possível com base no estado atual, que seria constituído por uma matrix de valores (X, O ou _).

### 3.2

Como todas as soluções têm custos diferentes, a melhor solução é única. Como o algoritmo A* tem uma heurística admissível encontra sempre a melhor solução. Assim, o algoritmo A* vai encontrar sempre a mesma solução pois é a melhor solução e essa solução é única.

### 3.3

a) C, como o último nó a ser expandido foi o nó B, o próximo nó em largura é o próximo nó no mesmo nível, ou seja, o nó C.

b) E, o próximo nó a ser expandido é o nó que é filho do último nó expandido, ou seja, o nó E.

c) D, a pesquisa de custo uniforme começa pelos nós com menor distância ao nó de origem, após o nó B, o próximo é o nó D, com um distância de 3.

d) C, nesta pesquisa começa-se por expandir os nós que têm uma menor heurística, neste caso o nó C, com heurística 1

e) G, o próxmo nó a ser expandido é o nó com menor soma de custo e heurística, neste caso é o nó G, distância = 3 + 5, heurística = 2, total = 10

### 3.4

A ordenação dos nós gerados pode ter muita influência na quantidade de nós gerados e avalaiados. Na pior ordenação possível, o algoritmo seria equivalente ao minimax clássico e, por isso, teria de percorrer b^d (branch^depth) nós. No melhor caso possível, o número de nós é reduzido para b^(d/2) (branch^(depth / 2)) nós.

### 3.5

B = 3, min(E = 3, F = 8, G = 7)

C <= 1, min(H = 1, I = ?) //cut

D = 8, min(J = 8, K = 10)

A = 8, max(B, C, D)

### 3.6

a)

delta = 18 - 20 = -2

probabilidade = exp(delta / t) = exp(-2 / 0.9) = 0.1084 = 10.84%

b)

delta = 20 - 18 = 2

probabilidade = 100% (delta > 0)