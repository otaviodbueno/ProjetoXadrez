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
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        private HashSet<Peca> pecas;

        private HashSet<Peca> capturadas;
        public bool xeque {  get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada( Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if(EstaEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if(EstaEmXeque(Adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            turno++;
            MudaJogador();
        }


        private void MudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public void ValidarPosicaodeOrigem(Posicao pos)
        {
            if(tab.Peca(pos) == null)
            {
                throw new TabuleiroException("   Não existe peça na posição de origem escolhida! Digite enter para continuar.");
            }
            if(jogadorAtual != tab.Peca(pos).cor)
            {
                throw new TabuleiroException("   A peça de origem escolhida não é sua! Digite enter para continuar.");
            }
            if(!tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("   Não há movimentos possíveis para a peça escolhida! Digite enter para continuar.");
            }
        }

        public void ValidarPosicaodeDestino(Posicao origem, Posicao destino)
        {
            if(!tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("   Posição de destino inválida! Digite enter para continuar.");
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
           aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('h', 2, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('a', 2, new Torre(tab, Cor.Branca));

            ColocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));


            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 1).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c', 2).ToPosicao());
            //tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d', 1).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 2).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d', 2).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e', 1).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('h', 2).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('a', 2).ToPosicao());


            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            //tab.ColocarPeca(new Rei(tab, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            //tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
        }
    }
}

