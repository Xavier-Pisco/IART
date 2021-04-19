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


## Exercício 2

### a

Uma possível representação seria uma lista com o número da máquina utilizada por cada um dos produtos, sendo que a cada posição correspondia a letra equivalente (e.g A = 1, B = 2...)

ii) [1,3,2,3,3]

### b

(Provavelmente isto não está certo)

Uma possível função de adaptação seria fazer um crossover entre os dois primeiros indíviduos e outro crossover entre os indivídios iii) e iv). Esse crossover poderia ser executado trocando os as máquinas correspondentes aos últimos 2 produtos.

Neste caso esta função de adaptação originaria os indivíduos:
- i) A-M1, B-M1, C-M2, D-M3, E-M3
- ii) A-M1, B-M3, C-M2, D-M2, E-M1
- iii) A-M1, B-M1, C-M2, D-M2, E-M2
- iv) A-M2, B-M1, C-M2, D-M2, E-M2

### c)

- i) custo = 25
- ii) custo = 27
- iii) custo = 31
- iv) custo = 41

(Isto está errado)

Probabilidades:
- i) P = 25/124 = 0.201
- ii) P = 27/124 = 0.218
- iii) P = 31/124 = 0.250
- iv) P = 41/124 = 0.331

- i) Como 0.201 < 0.22, este indivíduo é rejeitado
- ii) Como 0.218 < 0.4, este indivíduo é rejeitado
- iii) Como 0.25 < 0.88, este indivíduo é rejeitado
- iv) Este indivíduo é aceite por exclusão de partes

### d)

Fazer cruzamento entre o primeiro e o último produto pois é um conjunto que é diferente em todos os indivídos da população inicial.

Pai: A-M2, B-M1, C-M2, D-M2, E-M2

(O que está abaixo provavelmente está errado)

Filhos:
1. A-M2, B-M1, C-M2, D-M2, E-M2
2. 0.35 < 0.7, logo houve cruzamento, A-M1, B-M1, C-M2, D-M2, E-M1
3. 0.75 > 0.7, logo não houve cruzamento, A-M2, B-M1, C-M2, D-M2, E-M2
4. 0.5 < 0.7, logo houve cruzamento, A-M1, B-M1, C-M2, D-M2, E-M2

Como a probabilidade de mutação ocorreu no 22º número mas só existem 20 valore (5 por cada filho) não houve mutação.

## Exercício 4

### a)

Uma heurística admissível é uma heurística em que o seu valor nunca é superior ao custo real.

Uma heurística consistente é uma heurística cuja soma do valor com o custo do último passo é sempre menor ou igual à heurística do nó anterior.

Uma heurística admissível pode não ser consistente.

### c)

- Delta = 12-15 = -3
- P = e^(-3/0.8) = 0.0235 = 2.35%

### e)


