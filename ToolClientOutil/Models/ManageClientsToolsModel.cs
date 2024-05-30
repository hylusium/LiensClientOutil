using System.Collections.Generic;
using Tool.Data;

namespace ToolClientOutil.Models
{
    public class ManageClientsToolsModel
    {
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<Outils> Tools { get; set; } = new List<Outils>();
    }

    public class Client
    {
        public int IdClient { get; set; }
        public string NomClient { get; set; }
    }

    public class Outils
    {
        public int IdOutil { get; set; }
        public string NomOutil { get; set; }
    }
}
