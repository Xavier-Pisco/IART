# Exame 2018

## Exercício 1

### a

A árvore é gigante para escrever em md.

Expandir a1[]20 para: b8[X]12 (custo 8), d5[Y]13 (custo 7), g4[Z]11 (custo 9), h6[]20 (custo 12)

Expandir d5[Y]13 para: d8[]10 (custo 3), g4[Y,Z]7 (custo 4), h6[Y]20 (custo 5)

Expandir b8[X]12 para: b2[]6 (custo 6), d5[X,Y]7 (custo 5), g4[X,Z]3 (custo 9), h6[X]20 (custo 8)

Expandir g4[Z]11 para: h1[]7 (custo 4), h6[Z]20 (custo 3)

Expandir d8[]10 para: b8[X]8 (custo 2), g4[Z]6 (custo 7), h6[]20 (custo 6)

### b

- h1: É admissível pois o número de objetos na posição inicial vai ser sempre maior do que o custo do robô para se mexer até ao objeto e levá-lo para a posição final.
- h2: É admissível. Quer um objeto esteja na posição inicial ou esteja com o robô o custo de chegar à posição final é sempre >= 1.
- h3: É falso pois o robô pode ter 2 objetos consigo e, por isso, ter menor custo do que a distância. e.g. a2[Z,X], com o objeto Y na posição final tem uma heurística de 1 + 8 = 9, mas com apenas 8 movimentos consegue descarregar os 2 objetos nas posições finais.
- h4: É admissível pois, no mínimo, o robô tem de percorrer tantas casas como a maior distância ao destino de um objeto.

### c

A heurística h4 é a melhor pois é a que atribui valores mais altos e, consequentemente, valores mais próximos da realidade, sem ultrapassar os valores reais, ou seja, sem deixar de ser admissível.


## Exercício 2

### a

Neste caso o número em binário equivale a número da máquina em binário

M1 = 01, M2 = 10, M3 = 11

ii) 01 11 10 11 11

### b

A função de adaptação seria o custo máximo menos o custo real de modo a passar de um problema de minimização para um problema de maximização

Custo máximo = 10+7+11+12+8 = 48

Função de adaptação = 48 - tempo total de produção

- i) 48 - 25 = 23
- ii) 48 - 27 = 21
- iii) 48 - 31 = 17
- iv) 48 - 41 = 7

### c)

Probabilidades:
- i) P = 23/68 = 0.338 - ]0, 0.338]
- ii) P = 21/68 = 0.309 - ]0.338, 0.647]
- iii) P = 17/68 = 0.25 - ]0.647, 0.897]
- iv) P = 7/68 = 0.103 - ]0.897, 1]

- i) escolhido por elitismo
- 0.22 in ]0, 0.338], i) é escolhido
- 0.4 in ]0.338, 0.647], ii) é escolhido
- 0.88 in ]0.647, 0.897], iii) é escolhido


### d)

Fazer cruzamento em que se cruzam as últimas 2 máquinas

- i) mantém-se por elitismo
- i) 0.35 < 0.7, logo cruza
- ii) 0.75 > 0.7, logo mantém
- iii) 0.5 < 0.7, logo cruza

|Indivíduo|Inicial|Cruzamento|Mutação|
|-|-|-|-|
|i  |01 01 10 - 10 01|01 01 10 - 10 01|01 01 10 - 10 01|
|i  |01 01 10 - 10 01|01 01 10 - **10 10**|01 01 10 - 10 10|
|ii |01 11 10 - 11 11|01 11 10 - 11 11|01 11 10 - 11 11|
|iii|01 01 10 - 10 10|01 01 10 - **10 01**|0**1** 01 10 - 10 01|

Mutação não ocorre porque geraria um código inválido.

## Exercício 4

### a)

Uma heurística admissível é uma heurística em que o seu valor nunca é superior ao custo real.

Uma heurística consistente é uma heurística cuja soma do valor com o custo do último passo é sempre menor ou igual à heurística do nó anterior.

Uma heurística admissível pode não ser consistente.

### c)

- Delta = 12-15 = -3
- P = e^(-3/0.8) = 0.0235 = 2.35%
