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
        public void traerDatos(DataGridView grilla)
        {
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBd = comandoBD.ExecuteReader();

            grilla.Columns.Add("Nombre", "Nombre");
            grilla.Columns.Add("Apellido", "Apellido");
            grilla.Columns.Add("Edad", "Edad");


            if (lectorBd.HasRows)
            {
                while (lectorBd.Read())
                {
                    datosTabla += "-" + lectorBd[0];
                    grilla.Rows.Add(lectorBd[1], lectorBd[2], lectorBd[3]);
                }
            }
        }

        public void BuscarPorId(int codigo)
        {
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBd = comandoBD.ExecuteReader();


            if (lectorBd.HasRows) //SI TIENE FILAS
            {
                bool Find = false;
                while (lectorBd.Read()) //mientras pueda leer, mostrar (leer)
                {
                    if (int.Parse(lectorBd[0].ToString()) == codigo)
                    {

                        //datosTabla += "-" + lectorBD[0]; //dato d la comlumna 0
                        MessageBox.Show("Cliente Existente" + lectorBd[0], "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Find = true;
                        break;
                    }

                }
                if (Find = false)
                {

                    MessageBox.Show("NO Existente" + lectorBd[0], "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  

                }
            }
        }
    }
}
