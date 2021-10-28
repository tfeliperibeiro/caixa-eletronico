using System;
using System.Collections.Generic;

namespace caixa_eletronico
{
  class Program
  {
    public class Extrato
    {
      public List<string> Tipo = new List<string>();
      public List<string> Valor = new List<string>();
    }
    static void Main()
    {
      Console.WriteLine("Olá, seja bem vindo(a)!");
      Console.WriteLine("-----------------------");
      Console.WriteLine("Digite seu nome: ");
      Console.WriteLine("-----------------------");

      string nome = Console.ReadLine();
      Console.WriteLine("");

      Random dadosBancarios = new Random();

      int conta = dadosBancarios.Next(0, 99999);
      int agencia = dadosBancarios.Next(0, 500);

      Console.WriteLine("Olá " + nome + " seja bem vindo(a) ao caixa do banco Newbie Finance.");
      Console.WriteLine("Conta: " + conta);
      Console.WriteLine("Agencia: " + agencia);
      Console.WriteLine("-----------------------");

      double saldo = 0;
      bool sair = true;

      Extrato extratoCliente = new Extrato();

      while (sair)
      {
        Console.WriteLine("Digite 1 para consultar seu saldo");
        Console.WriteLine("Digite 2 para fazer um saque");
        Console.WriteLine("Digite 3 para fazer um deposito");
        Console.WriteLine("Digite 4 para ver o extrato");
        Console.WriteLine("Digite 5 para simular um empréstimo");
        Console.WriteLine("Digite 0 para sair");

        string opcao = Console.ReadLine();

        switch (opcao)
        {
          case "1":
            consultaSaldo(saldo);
            break;
          case "2":
            saldo = fazerSaque(saldo, extratoCliente);
            break;
          case "3":
            saldo = fazerDeposito(saldo, extratoCliente);
            break;
          case "4":
            consultaExtrato(extratoCliente);
            break;
          case "5":
            saldo += simularEmprestimo(saldo, extratoCliente);
            break;
          case "0":
            Console.WriteLine("Obrigado por usar nosso banco " + nome + "!");
            sair = false;
            break;
          default:
            Console.WriteLine("-----------------------");
            Console.WriteLine("Operação inválida! Tente outra opção.");
            Console.WriteLine("-----------------------");
            break;
        }
      }
    }

    static double consultaSaldo(double saldo)
    {
      Console.WriteLine("-----------------------");
      Console.WriteLine("Seu saldo é: " + saldo.ToString("C"));
      Console.WriteLine("-----------------------");
      return saldo;
    }

    static double fazerDeposito(double saldo, Extrato extratoCliente)
    {
      Console.WriteLine("-----------------------");
      Console.WriteLine("Digite o valor do deposito");
      Console.WriteLine("-----------------------");

      string deposito = Console.ReadLine();

      if (Int32.Parse(deposito) <= 0)
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("Saldo inválido para deposito!");
        Console.WriteLine("-----------------------");
        return saldo;
      }
      else
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("Deposito realizado com sucesso!");
        Console.WriteLine("-----------------------");
      }

      saldo += double.Parse(deposito);
      extratoCliente.Tipo.Add("Deposito:");
      extratoCliente.Valor.Add(deposito);
      return saldo;
    }

    static double fazerSaque(double saldo, Extrato extratoCliente)
    {
      if (saldo == 0)
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("ATENÇÃO: Você não tem saldo para saque!");
        Console.WriteLine("Digite 3 para depositar.");
        Console.WriteLine("-----------------------");
        return saldo;
      }
      Console.WriteLine("-----------------------");
      Console.WriteLine("Digite o valor do saque");
      Console.WriteLine("-----------------------");
      string saque = Console.ReadLine();
      Console.WriteLine("-----------------------");
      Console.WriteLine("Saque realizado com sucesso!");
      Console.WriteLine("-----------------------");

      if (saldo < double.Parse(saque))
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("ATENÇÃO: Seu saldo é menor que o valor do saque!");
        Console.WriteLine("Digite 3 para depositar.");
        Console.WriteLine("-----------------------");
        return saldo;
      }

      extratoCliente.Tipo.Add("Saque:");
      extratoCliente.Valor.Add("-" + saque);
      saldo -= double.Parse(saque);
      return saldo;
    }

    static void consultaExtrato(Extrato extratoCliente)
    {
      Console.WriteLine("Extrato:");
      Console.WriteLine("-----------------------");
      for (int i = 0; i < extratoCliente.Tipo.Count; i++)
      {
        Console.WriteLine(extratoCliente.Tipo[i]);
        Console.WriteLine(extratoCliente.Valor[i]);
        Console.WriteLine("-----------------------");
      }
    }

    static double simularEmprestimo(double saldo, Extrato extratoCliente)
    {
      Console.WriteLine("-----------------------");
      Console.WriteLine("Digite o valor que deseja fazer o empréstimo");
      Console.WriteLine("-----------------------");

      string valorEmprestimo = Console.ReadLine();

      if (Int32.Parse(valorEmprestimo) <= 0)
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("Digite o valor acima de R$ 0.");
        Console.WriteLine("-----------------------");
        return saldo;
      }

      double valorComJuros = 0.2 * Int32.Parse(valorEmprestimo);

      Console.WriteLine("-----------------------");
      Console.WriteLine("Em quantas vezes você deseja parcelar?");
      Console.WriteLine("ATENÇÃO: Parcelamos somente em até 12 vezes.");
      Console.WriteLine("-----------------------");

      string parcelas = Console.ReadLine();

      if (Int32.Parse(parcelas) <= 0 || Int32.Parse(parcelas) > 12)
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("Digite um número de parcelas válidas.");
        Console.WriteLine("-----------------------");
        return saldo;
      }

      double valorFinalEmprestimo = (valorComJuros * Int32.Parse(parcelas)) + Int32.Parse(valorEmprestimo);

      Console.WriteLine("-----------------------");
      Console.WriteLine("O valor final da dívida será de " + valorFinalEmprestimo.ToString("C"));
      Console.WriteLine("Digite 1 para CONFIRMAR e 2 para CANCELAR!");
      Console.WriteLine("-----------------------");

      string opcao = Console.ReadLine();

      if (Int32.Parse(opcao) == 1)
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("Operação realizada, seu dinheiro já está em sua conta.");
        Console.WriteLine("-----------------------");

        extratoCliente.Tipo.Add("Emprestimo");
        if (Int32.Parse(parcelas) != 1)
        {
          extratoCliente.Valor.Add("Você tem um saldo devedor de " + valorFinalEmprestimo.ToString("C") + " que será dividido em " + parcelas + " vezes.");
        }
        else
        {
          extratoCliente.Valor.Add("Você tem um saldo devedor de " + valorFinalEmprestimo.ToString("C") + " que será dividido em " + parcelas + " vez.");
        }

        saldo = Int32.Parse(valorEmprestimo);
        return saldo;
      }
      else
      {
        Console.WriteLine("-----------------------");
        Console.WriteLine("Operação cancelada!");
        Console.WriteLine("-----------------------");
        return saldo;
      }
    }
  }
}
