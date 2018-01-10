using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAP_MED_DB
{
    public class Game
    {

        public int Id { get; set; }
        public string Titel { get; set; }
        public double Rating { get; set; }


        public Game()
        {

        }

        public Game(int id, string titel, int rating)
        {
            Id = id;
            Titel = titel;
            Rating = rating;
        }
    }
}