using tabuleiro;
using Xadrez;

try
{
  PartidaDeXadrez partida = new PartidaDeXadrez();

    while(!partida.terminada)
    {
        Console.Clear();
        Tela.ImprimirTabuleiro(partida.tab);

        Console.Write("Origem: ");
        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

        bool[,] posicoesPossiveis = partida.tab.Peca(origem).MovimentosPossiveis();

        Console.Clear();
        Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

        Console.WriteLine();
        Console.Write("Destino: ");
        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

        partida.ExecutarMovimento(origem, destino);
    }


}
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}

