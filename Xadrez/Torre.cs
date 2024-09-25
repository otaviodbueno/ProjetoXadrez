using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "T";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linha, tab.coluna];

            Posicao pos = new Posicao(0, 0);

            //acima 
                pos.DefinirValores(posicao.linha - 1, posicao.coluna);
                while (tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                    if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                    {
                        break;
                    }
                    pos.linha = pos.linha - 1;
                }



            //direita
            //if (posicao.coluna + 1 <= 7 && posicao.coluna + 1 <= 0)
            //{
                pos.DefinirValores(posicao.linha, posicao.coluna + 1);
                while (tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                    if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                    {
                        break;
                    }
                    pos.coluna = pos.coluna + 1;
                    // Chumbando a validação
                    if (pos.linha == 8 || pos.coluna == 8)
                    {
                        break;
                    }
                }
            //}

            //abaixo
            //if (posicao.linha + 1 <= 7)
            //{
                pos.DefinirValores(posicao.linha + 1, posicao.coluna);
                while (tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                    if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                    {
                        break;
                    }
                    pos.linha = pos.linha + 1;
                }
          //}


            //esquerda
                pos.DefinirValores(posicao.linha, posicao.coluna - 1);
                while (tab.PosicaoValida(pos) && PodeMover(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                    if (tab.Peca(pos) != null && tab.Peca(pos).cor != cor)
                    {
                        break;
                    }
                    pos.coluna = pos.coluna - 1;
                }
            return mat;
        }
    }
}
