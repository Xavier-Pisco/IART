# Recurso 2 2018

## Exercício 1

### a)

i) Pesquisa em largura

<img src="./IART_Exame_ER2_2018_1.png" height=200>

Solução = F1

ii)

<img src="./IART_Exame_ER2_2018_2.png" height=200>

Solução = F3

iii)

<img src="./IART_Exame_ER2_2018_3.png" height=200>

Solução = F3

### b)

A heurística é admissível pois não existe nenhum nó cujo custo real seja maior do que o custo da heurística desse nó.

### c)

O algoritmo de pesquisa em largura pode encontrar uma solução que esteja num nível mais acima na árvore mas que pode ser dispendiosa do que uma solução que estivesse num nível mais baixo. Não é ótimo.

O algoritmo de pesquisa gulosa está depentente apenas da heurística utilizada e, por isso, pode encontrar uma solução que tem um custo maior, principalmente se a heurística utilizada não for boa. Não é ótimo.

O algoritmo A*, assumindo uma heurística admissível, vai sempre na direção do menor custo possível, uma vez que, expande sempre primeiro os nós com a soma do custo e heurística menor, pelo que, num só avalia um nó final quando o custo total deste nó for menor que a soma custo com heurística de todos os outros nós. É um algoritmo ótimo.

