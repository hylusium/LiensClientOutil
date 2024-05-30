using System;
using System.Data.SqlClient;
using System.Web.Mvc;
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

        public ActionResult ViewClient(string email) {
            var model = new ViewModelLogin { Email = email };
            string connString = "Server=HUGO;Database=ToolClientOutil;Trusted_Connection=True";
            using (SqlConnection sqlConnection = new SqlConnection(connString))
            {
                string queryPossede = "SELECT "
                sqlConnection.Open();
            }
            return View(model); }
    }
}
