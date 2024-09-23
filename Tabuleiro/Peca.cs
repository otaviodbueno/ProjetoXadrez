using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;

namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; set; }
        public int qtdMovimento { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            this.posicao = null;
            this.cor = cor;
            this.tab = tabuleiro;
            this.qtdMovimento = 0;
        }

        public void IncrementarQtdMovimentos()
        {
            qtdMovimento++;
        }
    }
}
