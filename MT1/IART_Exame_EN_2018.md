# Exame 2018

## Exercício 1

### a

A árvore é gigante para escrever em md.

Expandir a1[]20 para: b8[X]12 (custo 8), d5[Y]13 (custo 7), g4[Z]11 (custo 9), h6[]20 (custo 12)

Expandir d5[Y]13 para: b8[Y,X]7 (custo 5), d8[]10 (custo 3), g4[Y,Z]7 (custo 4), h6[Y]20 (custo 5)

Expandir b8[X]12 para: b2[]6 (custo 6), d5[X,Y]7 (custo 5), g4[X,Z]3 (custo 9), h6[X]20 (custo 8)

Expandir g4[Z]11 para: b8[Z,X]2 (custo 9), d5[Z,Y]7 (custo 4), h1[]7 (custo 4), h6[Z]20 (custo 3)

Expandir d8[]10 para: b8[X]8 (custo 2), g4[Z]6 (custo 7), h6[]20 (custo 6)

### b

- h1: É admissível pois o número de objetos na posição inicial vai ser sempre maior do que o custo do robô para se mexer até ao objeto e levá-lo para a posição final.
- h2: É admissível. Quer um objeto esteja na posição inicial ou esteja com o robô o custo de chegar à posição final é sempre >= 1.
- h3: É falso pois o robô pode ter 2 objetos consigo e, por isso, ter menor custo do que a distância. e.g. a2[Z,X], com o objeto Y na posição final tem uma heurística de 1 + 8 = 9, mas com apenas 8 movimentos consegue descarregar os 2 objetos nas posições finais.
- h4: É admissível pois, no mínimo, o robô tem de percorrer tantas casas como a maior distância ao destino de um objeto.

### c

A heurística h4 é a melhor pois é a que atribui valores mais altos e, consequentemente, valores mais próximos da realidade, sem ultrapassar os valores reais, ou seja, sem deixar de ser admissível.
