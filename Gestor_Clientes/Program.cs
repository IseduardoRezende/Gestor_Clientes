using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gestor_Clientes 
{
    class Program
    {
        [System.Serializable]
        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>(); //Dados Impersistentes pois são salvos na memória RAM;
        enum Menu { Listar = 1, Adicionar, Remover, Sair }

        static void Main(string[] args)
        {
            Carregar();

            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de Clientes - Bem vindo!\n");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Sair");
                int intopc = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intopc;

                switch (opcao)
                {
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Listar:
                        Listagem();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        escolheuSair = true;
                        break;
                }
                Console.Clear();
            }
        }

        static void Adicionar()     //Cadastro do Cliente
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Cadastro de Cliente:\n ");
            Console.WriteLine("Nome do Cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Email do Cliente: ");
            cliente.email = Console.ReadLine();
            Console.WriteLine("CPF do Cliente: ");
            cliente.cpf = Console.ReadLine();

            clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro do Cliente concluído !\nAperte Enter para Sair:");
            Console.ReadLine();
        }

        static void Listagem()    //Lista de Clientes:
        {
            if (clientes.Count > 0)
            {
                Console.WriteLine("Lista de Clientes:\n ");
                int indice = 0;
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID: {indice}");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"Email: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("======================");
                    indice++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum Cliente Cadastrado !\nAperte ENTER para Sair:");
            }

            Console.ReadLine();
        }

        static void Remover()
        {
            Console.WriteLine("Remoção:");
            Listagem();
            Console.WriteLine("Remover:\nDigite o ID do Cliente que deseja remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Salvar();

                Console.WriteLine("Cliente Removido com sucesso !");
            }
            else
            {
                Console.WriteLine("ID digitado está inválido, tente novamente");
                Console.ReadLine();
            }
        }

        static void Salvar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, clientes);

            stream.Close();
        }

        static void Carregar()
        {
            FileStream stream = new FileStream("Clients.dat", FileMode.OpenOrCreate);

            try  //try roda tudo que está dentro dele, mesmo dando erro; 
            {
                BinaryFormatter enconder = new BinaryFormatter();

                clientes = (List<Cliente>)enconder.Deserialize(stream);

                if (clientes == null)
                {
                    clientes = new List<Cliente>();
                }

            }
            catch (Exception) //catch continua a rodar o erro, podendo solucionar este sem fechar o programa;
            {
                clientes = new List<Cliente>();
            }

            stream.Close();
        }

    }
}  
