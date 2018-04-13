# Changelog

Changelog das alterações relevantes no simulador (e no simulador apenas). Para ver a lista completa de commits entre versões, clique no link da versão desejada.

## [v0.4] - 13-04-2018
### Adicionado

*Editor*
- Componentes elétricos básicos: Contatora, Fonte 24V, GND (0V), Contatos, Sensores, Solenoide, PushButton
- Correlação de contatos com contatoras
- Correlação de sensores no sistema elétrico com sensores no sistema pneumático
- Correlação de solenoides no sistema elétrico com solenoides de válvulas pneumáticas


## [v0.3] - 12-04-2018
### Adicionado

*Editor*
- Seleção múltipla:
  - Clicar e arrastar a partir de espaço vazio ativa a caixa de seleção, para selecionar múltiplos
    objetos
  - Segurar shift enquanto seleciona qualquer novo objeto, e de qualquer maneira, adiciona os objetos alvo à seleção atual
  - Edições (mover, deletar, rotacionar) para múltiplos objetos ao mesmo tempo são considerados como uma única ação

## [v0.2] - 20-03-2018
### Adicionado

*Editor*
- Desenho de "fios" (tubulação) entre conectores pneumáticos (linhas retas simples)
- Fios são selecionáveis e podem ser deletados
- Fios são atualizados com ações de mover e deletar componentes

## [v0.1] - 06-07-2017
### Adicionado

*Editor*
- Componentes distintos, sem ajuste de parâmetros internos
- Seleção de componentes
- Posicionamento de componentes no canvas
- Conexão de componentes pneumáticos
- Rotação de componentes
- Remover componentes
- Função de desfazer/refazer irrestrito para todas as ações
- Atalhos de teclado para comandos básicos:
  - Desfazer/refazer: ctrl+z / ctrl+y
  - Girar componente: ctrl+r
  - Remover componente: del
- Interface:
  - Botões de desfazer/refazer, aparecem na tela quando a opção está disponível
  - Lista simplificada de componentes com scroll view

*Lista de componentes*
- Válvula 3/2 simples solenóide
- Válvula 5/2 simples solenóide
- Cilindro de dupla ação
- Sensor de proximidade
- Fonte de pressão pneumática
- Exaustor
- Contator
- Fonte de tensão (24 V)
- Referência (0 V)
- Contato normalmente aberto

[v0.1]: https://github.com/gabrielnaves/TG/compare/2adb1b50f7405e099494ee41dddea7cf6715895e...v0.1
[v0.2]: https://github.com/gabrielnaves/TG/compare/v0.1...v0.2
[v0.3]: https://github.com/gabrielnaves/TG/compare/v0.2...v0.3
[v0.4]: https://github.com/gabrielnaves/TG/compare/v0.3...v0.4
