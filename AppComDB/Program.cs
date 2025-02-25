using Microsoft.Data.SqlClient;
using System;
using System.Net.Mail;
using System.Threading;

class Hello
{
    // Conexão com o banco de dados
    private static readonly string connectionString = "Server=Seu-Servidor;Database=ConsoleAPP;Integrated Security=True;TrustServerCertificate=True;";
    
    public static void Main()
    {
        string email, senha;
        Console.Write("Olá, já possui conta ou deseja criar? \n 1) Criar \n 2) Entrar\n ");
        string resposta = Console.ReadLine();

        switch (resposta)
        {
            case "1":
                while (true)
                {
                    Console.WriteLine("Entre com o email para cadastro:");
                    email = Console.ReadLine();

                    if (ValidarEmail(email))
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Email inválido. Insira um válido:");
                        Thread.Sleep(1000);
                    }
                }

                Console.WriteLine("Entre com a senha para cadastro:");
                senha = Console.ReadLine();
                InserirDadosDB(email, senha);
                goto case "2";

            case "2":
                Console.WriteLine("Insira seu email de login:");
                email = Console.ReadLine();
                Console.WriteLine("Entre com a senha de login:");
                senha = Console.ReadLine();

                if (VerificarDB(email, senha))
                {
                    Console.WriteLine("Login realizado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Login ou senha incorretos!");
                }
                break;

            default:
                Console.WriteLine("Escolha uma opção válida.");
                break;
        }
    }

    static void InserirDadosDB(string email, string senha)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Pessoas (Email, Senha) VALUES (@Email, @Senha)", connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Senha", senha);
                    cmd.ExecuteNonQuery();  // Executa a inserção dos dados
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            Console.Clear();
            Console.WriteLine("Cadastro realizado com sucesso! Irei redirecioná-lo para o sistema de login.");
            Thread.Sleep(3000);
            Console.Clear();
        }
    }

    private static bool VerificarDB(string email, string senha)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Pessoas WHERE Email = @Email AND Senha = @Senha", connection))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.HasRows)
                    {
                        Console.Clear();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }

    static bool ValidarEmail(string email)
    {
        try
        {
            var address = new MailAddress(email);
            return address.Address == email;
        }
        catch
        {
            Console.WriteLine("Email inválido");
            return false;
        }
    }
}
