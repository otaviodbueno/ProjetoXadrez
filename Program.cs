using ProjetoXadrez;
using tabuleiro;
using Xadrez;

try
{
    Tabuleiro tab = new Tabuleiro(8, 8);

    tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
    tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
    tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(5, 0)); ;


    Tela.ImprimirTabuleiro(tab);


}
catch (TabuleiroException ex)
{
    Console.WriteLine(ex.Message);
}