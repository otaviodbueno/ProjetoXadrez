using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using Xadrez;

namespace ProjetoXadrez.Xadrez
{
    class Peao : Peca
    {

        private PartidaDeXadrez partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) { 
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p == null || p.cor != cor;
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = tab.Peca(pos);
            return p != null && p.cor != cor;
        }

        private bool Livre(Posicao pos)
        {
            return tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linha, tab.coluna];

            Posicao pos = new Posicao(0, 0);

            if(cor == Cor.Branca)
            {
                pos.DefinirValores(posicao.linha - 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha - 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos) && qtdMovimento == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                // #Jogada Especial En Passant
                if(posicao.linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if(tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.Peca(esquerda) == partida.vulneravelenPassant)
                    {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.PosicaoValida(direita) && ExisteInimigo(direita) && tab.Peca(direita) == partida.vulneravelenPassant)
                    {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }

            }
            else
            {
                pos.DefinirValores(posicao.linha + 1, posicao.coluna);
                if (tab.PosicaoValida(pos) && Livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha + 2, posicao.coluna);
                if (tab.PosicaoValida(pos) && PodeMover(pos) && qtdMovimento == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.DefinirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.PosicaoValida(pos) && ExisteInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }


                // #Jogada Especial En Passant
                if (posicao.linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && tab.Peca(esquerda) == partida.vulneravelenPassant)
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.PosicaoValida(direita) && ExisteInimigo(direita) && tab.Peca(direita) == partida.vulneravelenPassant)
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}

