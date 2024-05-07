using dquicaliquinS5T1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace dquicaliquinS5T1
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;

        public string statusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddNewPerson(Persona persona)
        {
            int result = 0;
            try
            {
                Init();

                if (persona == null || string.IsNullOrEmpty(persona.Nombre))
                    throw new Exception("El nombre es requerido.");

                result = conn.Insert(persona);

                statusMessage = string.Format("{0} registro agregado, Nombre: {1}", result, persona.Nombre);
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al agregar el dato {0}. Problema: {1}", persona.Nombre, ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al mostrar datos. {0}", ex.Message);
            }
            return new List<Persona>();
        }

        public void UpdatePerson(Persona persona)
        {
            try
            {
                Init();

                if (persona == null || persona.Id <= 0)
                    throw new Exception("La persona no es válida.");

                int result = conn.Update(persona);

                statusMessage = string.Format("{0} registro actualizado, ID: {1}", result, persona.Id);
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al actualizar el registro. Problema: {0}", ex.Message);
            }
        }

        public void DeletePerson(int personId)
        {
            try
            {
                Init();

                int result = conn.Delete<Persona>(personId);

                statusMessage = string.Format("{0} registro eliminado, ID: {1}", result, personId);
            }
            catch (Exception ex)
            {
                statusMessage = string.Format("Error al eliminar el registro. Problema: {0}", ex.Message);
            }
        }






    }
}
