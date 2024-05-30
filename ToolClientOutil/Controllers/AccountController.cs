using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using Tool.Data;
using ToolClientOutil.Models;

namespace ToolClientOutil.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewModelLogin model)
        {
            if (ModelState.IsValid)
            {
                string connectionString = "Server=HUGO;Database=ToolClientOutil;Trusted_Connection=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string queryClient = "SELECT COUNT(1) FROM Client WHERE username=@Email AND password=@Password";
                    string queryAdmin = "SELECT COUNT(1) FROM Admin WHERE username=@Email AND password=@Password";

                    SqlCommand command = new SqlCommand(queryClient, connection);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Password", model.Password);

                    connection.Open();
                    int countClient = Convert.ToInt32(command.ExecuteScalar());

                    if (countClient == 1)
                    {
                        // Connexion réussie en tant que client
                        return RedirectToAction("ViewClient", new { email = model.Email });
                    }
                    else
                    {
                        // Vérifier dans la table Admin
                        command.CommandText = queryAdmin;
                        int countAdmin = Convert.ToInt32(command.ExecuteScalar());

                        if (countAdmin == 1)
                        {
                            // Connexion réussie en tant qu'admin
                            return RedirectToAction("Profile", new { email = model.Email, password = model.Password });
                        }
                        else
                        {
                            // Connexion échouée, ajouter une erreur de modèle
                            ModelState.AddModelError("", "Email ou mot de passe incorrect.");
                        }
                    }
                }
            }

            return View(model);
        }

        public ActionResult Profile(string email, string password)
        {
            var model = new ViewModelLogin
            {
                Email = email,
                Password = password
            };
            return View(model);
        }

        public ActionResult ViewClient(string email)
        {
            string connectionString = "Server=HUGO;Database=ToolClientOutil;Trusted_Connection=True";
            var projects = new List<Projet>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT O.NomOutil, O.VersionOutil, O.DateMaj, O.état, O.Commentaire
                    FROM Client C
                    JOIN Possede P ON C.IdClient = P.IdClient
                    JOIN Outil O ON P.IdOutil = O.IdOutil
                    WHERE C.username = @Email";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var project = new Projet
                    {
                        NomOutil = reader["NomOutil"].ToString(),
                        VersionOutil = reader["VersionOutil"].ToString(),
                        DateMaj = Convert.ToDateTime(reader["DateMaj"]),
                        Etat = reader["état"].ToString(),
                        Commentaire = reader["Commentaire"].ToString()
                    };
                    projects.Add(project);
                }
            }

            var model = new ViewClientModel
            {
                Email = email,
                Projects = projects
            };

            return View(model);
        }

        public ActionResult MCT()
        {
            string connectionString = "Server=HUGO;Database=ToolClientOutil;Trusted_Connection=True";
            var model = new ManageClientsToolsModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Récupérer les clients
                string queryClients = "SELECT IdClient, NomClient FROM Client";
                SqlCommand commandClients = new SqlCommand(queryClients, connection);
                SqlDataReader readerClients = commandClients.ExecuteReader();
                while (readerClients.Read())
                {
                    model.Clients.Add(new Models.Client
                    {
                        IdClient = Convert.ToInt32(readerClients["IdClient"]),
                        NomClient = readerClients["NomClient"].ToString()
                    });
                }
                readerClients.Close();

                // Récupérer les outils
                string queryTools = "SELECT IdOutil, NomOutil FROM Outil";
                SqlCommand commandTools = new SqlCommand(queryTools, connection);
                SqlDataReader readerTools = commandTools.ExecuteReader();
                while (readerTools.Read())
                {
                    model.Tools.Add(new Outils
                    {
                        IdOutil = Convert.ToInt32(readerTools["IdOutil"]),
                        NomOutil = readerTools["NomOutil"].ToString()
                    });
                }
                readerTools.Close();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkClientTool(int clientId, int toolId)
        {
            string connectionString = "Server=HUGO;Database=ToolClientOutil;Trusted_Connection=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Possede (IdClient, IdOutil) VALUES (@ClientId, @ToolId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientId", clientId);
                command.Parameters.AddWithValue("@ToolId", toolId);

                connection.Open();
                command.ExecuteNonQuery();
            }

            return RedirectToAction("MCT");
        }
    }
}
