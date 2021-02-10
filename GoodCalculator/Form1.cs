using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GoodCalculator
{
    public partial class Form1 : Form
    {
        private List<double> Nums = new List<double>();
        private List<char> ops = new List<char>();

        public Form1()
        {
            InitializeComponent();
        }
        
        private void Teclado(object o,EventArgs e)
        {
            Button botao = (Button)o;

            if (txtVisor.Text.StartsWith("="))
                txtVisor.Text = "";

            switch (botao.Text)
            {
                case "+":
                    if (String.IsNullOrEmpty(txtVisor.Text))
                        return;
                    AddOnList();
                    ops.Add('+');         
                    break;
                case "-":
                    if (String.IsNullOrEmpty(txtVisor.Text))
                        return;
                    AddOnList();
                    ops.Add('-');
                    break;
                case "x":
                    if (String.IsNullOrEmpty(txtVisor.Text))
                        return;
                    AddOnList();
                    ops.Add('x');
                    break;
                case "/":
                    if (String.IsNullOrEmpty(txtVisor.Text))
                        return;
                    AddOnList();
                    ops.Add('/');
                    break;
                case ",":                  
                    VerificaVirgula();
                    return;
                case "=":
                    if (String.IsNullOrEmpty(txtVisor.Text))
                        return;
                    AddOnList();
                    if (Nums.Count()>1)
                        CalculaResultado();                    
                    return;
                case "<-":
                    if (String.IsNullOrEmpty(txtVisor.Text))
                        return;
                    txtVisor.Text = txtVisor.Text.Remove(txtVisor.Text.Count() - 1, 1);
                    return;

            }
            
            txtVisor.Text += botao.Text;
        }

        private void AddOnList()
        {
            if (Nums.Count() == 0)
                Nums.Add(Convert.ToDouble(txtVisor.Text));
            else
            {
                int lastSpecChar = txtVisor.Text.LastIndexOf(SpecChars().LastOrDefault());
                Nums.Add(Convert.ToDouble(txtVisor.Text.Substring(lastSpecChar + 1, txtVisor.Text.Count() - (lastSpecChar + 1))));
            }
        }

        private void VerificaVirgula()
        {
            if (String.IsNullOrEmpty(txtVisor.Text))
                return;

            if (!Char.IsNumber(txtVisor.Text.LastOrDefault()))
                return;

            int lastSpecChar = txtVisor.Text.LastIndexOf(SpecChars().LastOrDefault());
            string LastNumber = txtVisor.Text.Substring(lastSpecChar + 1, txtVisor.Text.Count() - (lastSpecChar + 1));

            if (LastNumber.Contains(","))
                return;

            txtVisor.Text += ",";
        }

        private List<char> SpecChars()
        {
            string VisorChars = txtVisor.Text.Replace(",", "");
            return VisorChars.Where(c => !char.IsNumber(c)).ToList();
        }

        private void CalculaResultado()
        {
            while (ops.Count>0)
            {
                int indexOperacao = 0;

                if (ops.Where(c => c == 'x' || c == '/').Count()>0)            
                    indexOperacao = ops.FindIndex(c => c == 'x' || c == '/');
               
                else
                    indexOperacao = ops.FindIndex(c => c == '+' || c == '-');

                int indexNum1 = indexOperacao;
                int indexNum2 = indexNum1 + 1;           

                IConta conta = null;

                switch (ops[indexOperacao])
                {
                    case '+':
                        conta = new ContaSoma();
                        break;
                    case '-':
                        conta = new ContaSubtracao();
                        break;
                    case 'x':
                        conta = new ContaMulti();
                        break;
                    case '/':
                        conta = new ContaDivisao();
                        break;
                    default:
                        conta = new ContaDefault();
                        break;
                }

                Nums[indexNum1] = conta.Calcula(Nums[indexNum1], Nums[indexNum2]);

                ops.RemoveAt(indexOperacao);
                Nums.RemoveAt(indexNum2);
            }
            txtVisor.Text = "=" + Nums[0];

            Nums.Clear();
            ops.Clear();
        }

       
    }
}
