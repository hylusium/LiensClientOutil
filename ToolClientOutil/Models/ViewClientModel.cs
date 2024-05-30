using System;
using System.Collections.Generic;
using Tool.Data;

namespace ToolClientOutil.Models
{
    public class ViewClientModel
    {
        public string Email { get; set; }
        public List<Projet> Projects { get; set; }
    }

    public class Projet
    {
        public string NomOutil { get; set; }
        public string VersionOutil { get; set; }
        public DateTime DateMaj { get; set; }
        public string Etat { get; set; }
        public string Commentaire { get; set; }
    }
}
