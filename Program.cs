using tabuleiro;
using Xadrez;

try
{
  PartidaDeXadrez partida = new PartidaDeXadrez();

    while(!partida.terminada)
    {
        try
        {
            Console.Clear();
            Tela.ImprimirPartida(partida);

            //Tela.ImprimirTabuleiro(partida.tab);
            //Console.WriteLine();
            //Console.WriteLine("Turno: " + partida.turno);
            //Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);

            Console.WriteLine();
            Console.Write("Origem: ");
            Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
            partida.ValidarPosicaodeOrigem(origem);

            bool[,] posicoesPossiveis = partida.tab.Peca(origem).MovimentosPossiveis();

            Console.Clear();
            Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

            Console.WriteLine();
            Console.Write("Destino: ");
            Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
            partida.ValidarPosicaodeDestino(origem, destino);

            partida.RealizaJogada(origem, destino);
        }
        catch(TabuleiroException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
    }
    Console.Clear();
    Tela.ImprimirPartida(partida);

  }
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}

