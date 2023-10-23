using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Reflection.Emit;
using System.Windows.Forms;


namespace PryBaseDeDatosSocios
{
    internal class clsAccesoADatos
    {
        OleDbConnection conexionBD;
        OleDbCommand comandoBD;
        OleDbDataReader lectorBd;

        string cadenaDeConexion = @"Provider = Microsoft.ACE.OLEDB.12.0;" + " Data Source = ..\\..\\Resources\\EL_CLUB.accdb";

        public string estadoDeConexion = "";
        public string datosTabla = "";

        public void ConectarBD()
        {
            try
            {
                conexionBD = new OleDbConnection();
                conexionBD.ConnectionString = cadenaDeConexion;
                conexionBD.Open();
                estadoDeConexion = "Conectado";
            }
            catch (Exception ex)
            {
                estadoDeConexion = "Error" + ex.Message;
            }
           
        }
        public void traerDatos()
        {
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBd = comandoBD.ExecuteReader();

            if (lectorBd.HasRows)
            {
                while (lectorBd.Read())
                {
                    datosTabla += "-" + lectorBd[1];
                }
            }
        }
    }
}
