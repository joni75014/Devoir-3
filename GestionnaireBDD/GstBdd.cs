using ClassesMetier;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace GestionnaireBDD
{
    public class GstBdd
    {
        MySqlConnection cnx;
        MySqlCommand cmd;
        MySqlDataReader dr;

        public GstBdd()
        {
            string driver = "server=localhost;user id=root;password=;database=devoir_reservation";
            cnx = new MySqlConnection(driver);
            cnx.Open();
        }

        public List<Manifestation> GetAllManifestations()
        {
            List<Manifestation> lesManifestations = new List<Manifestation>();
            cmd = new MySqlCommand("select idManif, nomManif, dateDebut, dateFin,idSalle,numSAlle,nbPlaces from manifestation inner join salle  where numSalle=idSalle",cnx);
        
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                Salle uneSalle = new Salle()
                {
                    IdSalle = Convert.ToInt16(dr[4].ToString()),
                    NomSalle = dr[5].ToString(),
                    NbPlaces = Convert.ToInt16(dr[6].ToString()),
                };

             

                Manifestation uneManifestation = new Manifestation()
                {
                    IdManif = Convert.ToInt16(dr[0].ToString()),
                    NomManif = dr[1].ToString(),
                    DateDebutManif = dr[2].ToString(),
                    DateFinManif = dr[3].ToString(),
                    LaSalle = uneSalle,
                };
                lesManifestations.Add(uneManifestation);              
            }

            dr.Close();
            return lesManifestations;
        }

        public List<Place> GetAllPlacesByIdManifestation(int idManif, int idSalle)
        {

                 List<Place> lesPlaces = new List<Place>();
                 cmd = new MySqlCommand("select idPlace, nbPlaces, nomSalle, libre, numPlace, numSalle, codeTarif from manifestation inner join occuper on idManif = numManif inner join place on numPlace = place.idPlace where idManif =" + idManif + " and place.numSalle =" + idSalle + ";", cnx);
                 dr = cmd.ExecuteReader();

                while (dr.Read())
               {
                 Place unePlace = new Place()
               {
              IdPlace = Convert.ToInt16(dr[0].ToString()),
              CodeTarif = Convert.ToChar(dr[1].ToString()),
              Occupee = Convert.ToBoolean(dr[2].ToString()),

             };
                lesPlaces.Add(unePlace);

                


        }
            dr.Close();
            return lesPlaces;

        }
        public List<Tarif> GetAllTarifs()
        {

            List<Tarif> lesTarifs = new List<Tarif>();
            cmd = new MySqlCommand("select idTarif, nomTarif, prix from tarif", cnx);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Tarif unTarif = new Tarif()
                {
                    IdTarif = Convert.ToChar(dr[0].ToString()),
                    NomTarif = dr[1].ToString(),
                    Prix = Convert.ToDouble(dr[2].ToString())
                };
                lesTarifs.Add(unTarif);
            }
            dr.Close();

            return lesTarifs;
      
        }

        public void ReserverPlace(int idPlace, int idSalle,int idManif)
        {
          
        }
    }
}
