using JimFilmsTake2.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace JimFilmsTake2.Db
{
    // Ons database model, we kunnen Films en Bioscoopen apart op slaan, dit doen we omdat al onze bioscopen eigenaar zijn van hun 
    // Objecten, alleen films kunnen toegewezen worden aan meerdere bioscopen.
    public class JsonModel
    {
        public IList<Film> Films { get; set; }
        public IList<Bioscoop> Bioscopen { get; set; }
        public JsonModel()
        {
            this.Films = new List<Film>();
            this.Bioscopen = new List<Bioscoop>();
        }
    }
}
