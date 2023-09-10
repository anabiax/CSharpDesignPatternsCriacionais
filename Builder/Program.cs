
namespace Builder
{

    public class Computador     // product 
    {
        string tipoComputador;

        public Computador(string tipoComputador)
        {
            this.tipoComputador = tipoComputador;
        }
    }


    public abstract class ComputadorBuilder     
    {
        public abstract void BuildSO();
        public abstract void BuildDispositivos();
        public Computador TipoComputador { get; }

    }


    // concreteBuilder
    public class DesktopBuilder : ComputadorBuilder
    {
        Computador computador;
        public DesktopBuilder()
        {
            computador = new Computador("Desktop");
        }

        public override void BuildDispositivos()
        {
            System.Console.WriteLine($"Build dispositivos no desktop.");
        }

        public override void BuildSO()
        {
            System.Console.WriteLine($"Build sistema operacional no desktop.");
        }

        public Computador TipoComputador
        {
            get 
            { 
                return computador; 
            }
        }
    }


     // concreteBuilder
    public class NotebookBuilder : ComputadorBuilder
    {
        Computador computador;
        public NotebookBuilder()
        {
            computador = new Computador("Notebook");
        }

        public override void BuildDispositivos()
        {
            System.Console.WriteLine($"Build dispositivos no notebook.");
        }

        public override void BuildSO()
        {
            System.Console.WriteLine($"Build sistema operacional no notebook.");
        }

        public Computador TipoComputador
        {
            get 
            { 
                return computador; 
            }
        }
    }


    // quem starta todo o processo
    public class Fabricante
    {
        // diretor
        public void Build(ComputadorBuilder computadorBuilder)
        {
            computadorBuilder.BuildDispositivos();
            computadorBuilder.BuildSO();
        }
    }

    
    public class Program
    {
        public static void Main(string[] args)
        {
            // instância de Diretor
            Fabricante fabricante = new();

            // instância do concreteBuilder - definindo a instância do objeto que eu vou querer criar
            NotebookBuilder notebookBuilder = new();
            DesktopBuilder desktopBuilder = new();

            // cria objetos conforme o builder - o tipo de computador que eu quero criar
            fabricante.Build(notebookBuilder);
            fabricante.Build(desktopBuilder);

            Console.ReadLine();
        }
    }
}