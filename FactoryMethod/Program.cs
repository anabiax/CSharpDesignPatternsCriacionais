using System.Collections;
using System.Text;

namespace Creational
{

    public abstract class Pizza             // define a abstraçao p/ os objetos criados pelo feactory method
    {
        protected string Nome { get; set; }
        protected string massa;
        protected string molho;
        protected ArrayList ingredientes = new();

        public string Preparar()
        {
            StringBuilder sb = new();
            sb.Append($"\nPreparando {Nome} \n");
            sb.Append(massa + "\n");
            sb.Append(molho + "\n");
            sb.Append($"Ingredientes: \n");
            
            foreach(string ingrediente in ingredientes)
            {
                sb.Append($"\t {ingrediente} \n");
            }

            sb.Append(Cozinhar());
            sb.Append(Fatiar());
            sb.Append(Embalar());

            return sb.ToString();
        }

        public virtual string Cozinhar()
        {
            return $"Cozinhar por 25 minutos a 350º. \n";
        }

        public virtual string Fatiar()
        {
            return $"Fatiar a pizza em 8 pedaços. \n";
        }

        public virtual string Embalar()
        {
            return $"Embalar a pizza.";
        }

    }


    // implementa/herda a abstração do produto(Pizza) e sabe criar produtos.
    public class SPPizzaMussarela : Pizza   // vai decidir como criar as pizzas da sua loja
    {
        public SPPizzaMussarela()
        {
            Nome = $"Pizza de mussarela paulista";   
            massa = $"Massa fina crocante paulista"; 
            molho = $"Molho de tomate italiano paulista"; 
            ingredientes.Add($"Queijo parmesão"); 
            ingredientes.Add($"Azeitonas verdes"); 
        }
    }

    public class SPPizzaCalabreza : Pizza   // vai decidir como criar as pizzas da sua loja
    {
        public SPPizzaCalabreza()
        {
            Nome = $"Pizza de calabreza paulista";   
            massa = $"Massa fina crocante"; 
            molho = $"Molho de tomate italiano"; 
            ingredientes.Add($"Fatias de calabreza defumada especial"); 
            ingredientes.Add($"Queijo parmesão italiano tradicional"); 
        }
    }

    public class RJPizzaMussarela : Pizza  
    {
        public RJPizzaMussarela()
        {
            Nome = $"Pizza de mussarela";   
            massa = $"Massa fina crocante"; 
            molho = $"Molho de tomate italiano"; 
            ingredientes.Add($"Queijo parmesão"); 
            ingredientes.Add($"Azeitonas verdes"); 
        }
    }

    public class RJPizzaCalabreza : Pizza   
    {
        public RJPizzaCalabreza()
        {
            Nome = $"Pizza de calabreza";   
            massa = $"Massa fina crocante"; 
            molho = $"Molho de tomate italiano"; 
            ingredientes.Add($"Fatias de calabreza defumada especial"); 
            ingredientes.Add($"Queijo parmesão italiano tradicional"); 
        }
    }

    // define um FM que as subclasses vao implementar p/ fabricar pizza - é o Creator.
    public abstract class PizzaFactoryMethod     // vai delegar às subclasses a responsa de QUAL classe instanciar 
    {
        public Pizza MontarPizza(string tipo)
        {
            Pizza pizza;
            pizza = CriarPizza(tipo);

            return pizza;
        }

        // aqui eu delego a decisao de qual classe instanciar p/ criar a pizza
        protected abstract Pizza CriarPizza(string tipo);

    }


    // Concrete creator - sobreescreve o Factory method p/ retornar um objeto da classe ConcreteProduct(decide qual classe instanciar)
    public class RJPizzaFactory : PizzaFactoryMethod   // vai decidir como criar as pizzas de suas filiais
    {
        protected override Pizza CriarPizza(string tipo)
        {
            if(tipo.Equals("M"))
            {
                return new RJPizzaMussarela();
            }

            if(tipo.Equals("C"))
            {
                return new RJPizzaCalabreza();
            }

            return null;
        }
    }

    public class SPPizzaFactory : PizzaFactoryMethod
    {
        protected override Pizza CriarPizza(string tipo)
        {
            if(tipo.Equals("M"))
            {
                return new SPPizzaMussarela();
            }

            if(tipo.Equals("C"))
            {
                return new SPPizzaCalabreza();
            }

            return null;        
        }
    }

    public class PizzaSimpleFactory 
    {
        public static PizzaFactoryMethod CriarPizzaria(string local)     // criar instâncias das filiais(as subclasses do factory method)
        {   
            PizzaFactoryMethod pizzaria;
            switch(local)
            {
                case "S":
                    pizzaria = new SPPizzaFactory();
                    break;
                case "R":
                    pizzaria = new RJPizzaFactory();
                    break;
                default:
                    throw new ApplicationException($"A pizzaria {local} não foi criada.");
            }

            return pizzaria;
        }
    }

    public class Program   
    {   
        public static void  Main(string[] args)
        {
            System.Console.WriteLine($"===== PIZZARIA =====\n");

            System.Console.WriteLine($"Informe o local (S)São Paulo ou (R)Rio de Janeiro");
            var localEscolhido = Console.ReadLine().ToUpper();


            System.Console.WriteLine($"Informe a pizza que desejada (C)alabreza e (M)ussarela.");
            var pizzaEscolhida = Console.ReadLine().ToUpper();

            try
            {
                PizzaFactoryMethod pizzaria = PizzaSimpleFactory.CriarPizzaria(localEscolhido);

                var pizza = pizzaria.MontarPizza(pizzaEscolhida);
                System.Console.WriteLine(pizza.Preparar());
                System.Console.WriteLine($"\n Pizza concluída com sucesso.");

            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}