using ProjetoXadrez;
using tabuleiro;
using Xadrez;

try
{
    Tabuleiro tab = new Tabuleiro(8, 8);

    tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
    tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
    tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(5, 0)); ;


    tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(0, 1));
    tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(1, 4));

    Tela.ImprimirTabuleiro(tab);


}
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}

//PosicaoXadrez pos = new PosicaoXadrez('a', 1);

//Console.WriteLine(pos);

//Console.WriteLine(pos.ToPosicao());