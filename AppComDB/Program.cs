using Microsoft.Data.SqlClient;
using System;
using System.Net.Mail;


class Hello
{


    //connect no db
    private static readonly string connectionString = "Server=Higor;Database=ConsoleAPP;Integrated Security=True;TrustServerCertificate=True;";
    public static void Main()
    {

        string email, senha;
        Console.Write("Olá, já possui conta ou deseja criar?? \n 1)Criar \n 2)Entrar\n ");
        string resposta = Console.ReadLine();
        



        switch(resposta)
        {
            case "1":
            while(true)
            {
                
                Console.WriteLine("Entre com o email para cadastro");
                
                email = Console.ReadLine();
               
                if(ValidarEmail(email))
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Email invalido insira um valido: ");
                    Thread.Sleep(1000);

                }
            }

            Console.WriteLine("Entre com a senha para cadastro");
            senha = Console.ReadLine();
            InserirDadosDB(email,senha);

            goto case "2";

            case "2":
            Console.WriteLine("Insira seu email de login");
            email = Console.ReadLine();
            Console.WriteLine("Entre com a senha de login");
            senha = Console.ReadLine();

            if(VerificarDB(email,senha))
            {
                Console.WriteLine("Login realizado com sucesso");
            }
            else
            {
                Console.WriteLine("Login ou senha incorretos! ");
            }
            break;

            default:
            Console.WriteLine("Escolha uma opção válida");
            break;
        }
    }



    static void InserirDadosDB(string email,string senha)
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {

            //abro conexão
            connection.Open();
            try
            {   //faço a pesquisa se possui os valores email e senha corretamente
                using(SqlCommand cmd = new SqlCommand("INSERT INTO Pessoas (Email, Senha) VALUES (@Email, @Senha)",connection))
                {
                    cmd.Parameters.AddWithValue("@Email",email);
                    cmd.Parameters.AddWithValue("@Senha",senha);
                }
            }
            catch(SqlException ex)
            {
                throw new Exception(ex.Message);
            }



            //fecho conexão
            connection.Close();

            Console.Clear();
            Console.WriteLine("Cadastro realizado com sucesso, Irei te redirecionar para o sistema de login!");
            Thread.Sleep(3000);
            Console.Clear();


        }
    }

    private static bool VerificarDB(string email,string senha)
    {
        using(SqlConnection connection = new SqlConnection(connectionString))
        {

            connection.Open();

            using(SqlCommand cmd = new SqlCommand("SELECT * FROM Pessoas WHERE Email = @Email AND @Senha = Senha",connection))
            {
                cmd.Parameters.AddWithValue("@Email",email);
                cmd.Parameters.AddWithValue("@Senha",senha);

                //executa a leitura
                using(SqlDataReader read = cmd.ExecuteReader())
                {
                    if(read.HasRows)
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

            connection.Close();
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
            Console.WriteLine("Email invalido");

            return false;
        }
    }
}