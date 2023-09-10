using System;

namespace Creational
{
    // CREATIONAL
    // SIMPLE FACTORY

    // 1º cliente solicita um novo objeto, no caso pizza, ao objeto Factory passando as infos necessárias p/ o tipo de objeto (nome da pizza).
    // 2º a fábrica cria um novo produto concreto a partir da classe abstrata
    // 3º retorna ao cliente (Program) o produto recém criado.
    // 4º o cliente usa os produtos como produtos abstratos sem estar ciente de sua implementação concreta.

    public abstract class Pizza     // define como criar uma pizza 
    {
        public string Nome { get; set; }
        public abstract void Preparar();
        public abstract void Assar(int tempo);
        public abstract void Embalar();
    }


    public class PizzaCalabreza : Pizza    // produtos concretos
    {
        public PizzaCalabreza()
        {
            Nome = "Calabreza";
        }

        public override void Preparar()
        {
            System.Console.WriteLine($"Preparando a pizza de {Nome}.");
        }
        public override void Assar(int tempo)
        {
            System.Console.WriteLine($"Pizza de {Nome} assando por {tempo} minutos.");
        }

        public override void Embalar()
        {
            System.Console.WriteLine($"Embalando a pizza de {Nome}.");
        }
    }


    public class PizzaMussarela : Pizza
    {
        public PizzaMussarela()
        {
            Nome = "Mussarela";
        }

        public override void Preparar()
        {
            System.Console.WriteLine($"Preparando a pizza de {Nome}.");
        }

        public override void Assar(int tempo)
        {
            System.Console.WriteLine($"Pizza de {Nome} assando por {tempo} minutos.");
        }

        public override void Embalar()
        {
            System.Console.WriteLine($"Embalando a pizza de {Nome}.");
        }
    }


    public class Program   // solicita a criação dos objetos ao factory
    {
        public static void  Main(string[] args)
        {
            System.Console.WriteLine($"===== PIZZARIA =====");
            System.Console.WriteLine($"Informe a pizza que desejada (C)alabreza e (M)ussarela.");
            var pizzaEscolhida = Console.ReadLine().ToUpper();

            try
            {
                Pizza pizza = PizzaSimpleFactory.CriarPizza(pizzaEscolhida.ToUpper());
                pizza.Preparar();
                pizza.Assar(20);
                pizza.Embalar();
                System.Console.WriteLine($"Processo concluído.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            System.Console.ReadLine();
        }
    }


    public class PizzaSimpleFactory // centraliza a lógica de criação de objetos
    {
        public static Pizza CriarPizza(string nomePizza)   // retorna com base na condicional do cliente
        {
            Pizza pizza; // cria a instância
            switch(nomePizza)
            {
                case "C":
                    pizza = new PizzaCalabreza();
                    System.Console.WriteLine($"Pizza de calabreza selecionada.");
                    break;
                case "M":
                    pizza = new PizzaMussarela();
                    System.Console.WriteLine($"Pizza de mussarela selecionada.");
                    break;
                default:
                    throw new ApplicationException($"A pizza {nomePizza} não foi criada.");
            }

            return pizza;
        }
    }   
}