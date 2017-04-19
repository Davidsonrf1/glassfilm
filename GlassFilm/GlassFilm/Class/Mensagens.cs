using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlassFilm
{
    // Struct é uma forma mais simples, 
    public struct Mensagens
    {
        public static void Atencao(string msg)
        {
            MessageBox.Show(msg, "A t e n ç ã o", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void Informacao(string msg)
        {
            MessageBox.Show(msg, "A t e n ç ã o", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void Sair(string msg)
        {
            MessageBox.Show("Tem certeza que deseja sair da rotina?", " A t e n ç ã o ", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        public static void Salvar(string msg)
        {
            MessageBox.Show("Confirma gravação dos dados digitados?", " A t e n ç ã o ", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
        public static DialogResult PeruntaSimNao(string Pergunta)
        {
            return MessageBox.Show(Pergunta,"A t e n ç ã o", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static void Gravado(string msg)
        {
            MessageBox.Show(msg, " C o n f i r m a ç ã o ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
