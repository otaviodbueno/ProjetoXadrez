using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno { get; set; }
        public Cor jogadorAtual { get; set; }
        public bool terminada { get; set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 1).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 1).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 2).ToPosicao());

        }
    }
}
