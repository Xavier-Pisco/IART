# Recurso 2019

## Exercício 1

### 1.1

- **Representação de um estado**:
  - Origem, Atual, Destino, Matriz[3][4]
  - Na matriz cada 'casa' tem uma letra correspondente, ou x no caso de ser uma casa vazia.
  - Origem, Atual, Destion são letras de casas específicas da matriz
- **Estado Inicial**:
  - D(1,2), D(1,2), J(3,4), [[A, B, C], [D, E, x], [F, G, x], [H, I, J]]
- **Operadores**:
  - **Cima**:
    - Pré-condições:
      - Atual = Matriz[X][Y]
      - Y > 1
      - Matrix[X][Y - 1] != x
    - Efeitos:
      - Atual = Matriz[X][Y - 1]
    - Custo:
      - 1
  - **Direita**:
    - Pré-condições:
      - Atual = Matrix[X][Y]
      - X < 3
      - Matrix[X + 1][Y] != x
    - Efeitos:
      - Atual = Matriz[X + 1][Y]
    - Custo:
      - 1
  - **Baixo**:
    - Pré-condições:
      - Atual = Matriz[X][Y]
      - Y < 4
      - Matrix[X][Y + 1] != x
    - Efeitos:
      - Atual = Matriz[X][Y + 1]
	- Custo:
      - 1
  - **Direita**:
    - Pré-condições:
      - Atual = Matrix[X][Y]
      - X > 1
      - Matrix[X - 1][Y] != x
    - Efeitos:
      - Atual = Matriz[X - 1][Y]
    - Custo:
      - 1

### 1.2

i)

- ------------D--------------
- -----A------E---------F----
- -----B----B---G-----G---H--
- ---C--E--C--I--F----I-----I--
- -------G------J

ii)

- ----D
- ----A
- ----B
- --C---E
- -------G
- --------I
- -------J

### 1.3

iii)

A pesquisa gulosa expande sempre para o nó cuja avaliação dá um menor resultado, neste caso, vai expandir, preferencialmente, para a direita e, caso não seja possível, para cima e, posteriormente, para baixo. Por fim, vai para casas com uma pior avaliação, ou seja, para a esquerda.

A prioridade é definidia por:
1. Nó com melhor avaliação
2. Nó que é criado primeiro

iv)

O método de subida da colina gera todos os possíveis sucessores de um estado e escolhe o que estiver melhor avaliado, sendo a função a mesma da pesquisa gulosa, vai gerar os nós pela mesma ordem.

v)

A pesquisa gulosa expande sempre para o nó cuja avaliação dá um menor resultado, neste caso, vai expandir primeiro para a diretia, depois para baixo. Por fim, vai para nós com pior avaliação, começando por cima e depois esquerda.

### 1.4

O algoritmo apresentado efetua uma pesquisa em largura, uma vez que, está, constantemente, a adicionar os filhos de um nó a uma lista e, por isso, a lista fica ordenada pela ordem pela qual adiciona os nós, ou seja, percorre todos filhos do nó inicial, depois todos os netos, etc.

Para tornar este algoritmo num algoritmo de profundidade iterativa, o algoritmo teria de ser aplicado a todos os filhos de um nó antes de ser aplicado aos irmãos, ou seja, em vez de criar uma lista com todos os nós, chamar recursivamente o algoritmo para cada um dos sucessores. Para além disso, seria necessário verificar se o nó que estava a ser avaliado já estaria na profundidade limite e atualizar o valor da profundidade conforme necessário.

### 1.5

(Não está muito bem respondida, mas não sei bem o que era suposto dizer)

O espaço de estados seria (N x M), onde N é o número de colunas e M o número de linhas da matriz.

Um bom método para a resolução do problema seria usar o método de pesquisa A* e como heurística usar a "distância Manhattan" da posição atual à solução.

## Exercício 3

### 3.1

Limite de profundidade 1

- 1

Limite de profundidade 2

- 1
- 2

Limite de profundidade 3

- 1
- 2
- 3

Limite de profundidade

- 1
- 2
- 3
- 1 4

### 3.2

C) max(h1,h2)

Uma vez que são as duas admissíveis sabemos que o custo real será sempre <= que o valor de qualquer uma das hurísticas. Quanto maior for o valor da heurística mais perto do custo real vai estar, por isso, ao escolher a heurística que der um valor mais perto do valor real menos nós têm de ser expandidos e mais rapidamente se chega à solução.

### 3.3

A afirmação é verdadeira pois ambos os tipos de pesquisa expandem todos os filhos do estado atual e avaliam-nos 1 a 1 para escolher o que tiver o melhor valor com base na heurística. Ao utilizar a mesma função heurística os nós vão ter o mesmo valor e, por isso, vão ser expandidos os mesmos nós.

### 3.4

No algoritmo "arrefecimento simulado", uma solução que seja pior do que a atual pode ser aceite com uma probabilidade que depende da temperatura atual e da diferença entre as duas hipóteses. Como a probabilidade depende diretamente da temperatura e a temperatura desce constatemente com o número de estados avaliados, no ínicio da pesquisa existe uma maior probabilidade de aceitar estados que estejam mais afastados da solução.

### 3.6

- B = min(E,F,G) = min(6, 4, 1) = 1
- C = min(H,I,J) = min(13, 15, 20) = 13
- D = min(L,M,N) = min(8, ?, ?) <= 8
- A = max(B, C, D) = 13

### 3.7

- ------------A-----------
- ----C------B-------D----
- -H-I-J--E-F-G--L-M-N

Neste caso, após calcular os nós H, I e J, o nó C fica com o valor 13 e, por isso, ao calcular o nó E, o nó B pode ser cortado e, por isso, evita-se o cálculo dos nós F e G. O mesmo acontece no nó D, evitando os nós M e N.

Num caso genérico, o necessário é posicionar o nó que vai ser escolhido no lado esquerdo da árvore, para ser avaliado primeiro e, nos restantes nós, meter o nó com o filho que vai criar um corte no ínicio da árvore, no caso de uma maximização meter o filho com o menor valor e no caso de uma minimização meter o filho como maior valor.
