using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Reflection.Emit;
using System.Windows.Forms;
using System.Data;


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
            grilla.Columns.Add("Lugar de Nacimiento","Lugar de Nacimiento");
            grilla.Columns.Add("Edad", "Edad");
            grilla.Columns.Add("Sexo", "Sexo");
            grilla.Columns.Add("Ingreso", "Ingreso");
            grilla.Columns.Add("Puntaje", "Puntaje");
            grilla.Columns.Add("Estado Cliente", "Estado Cliente");

            if (lectorBd.HasRows)
            {
                while (lectorBd.Read())
                {
                    datosTabla += "-" + lectorBd[0];
                    grilla.Rows.Add(lectorBd[1], lectorBd[2], lectorBd[3], lectorBd[4], lectorBd[5], lectorBd[6], lectorBd[7], lectorBd[8]);
                }
                
            }
        }

        public void BuscarPorId(string codigo)
        {
            comandoBD = new OleDbCommand();
            DataSet objDS = new DataSet();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBd = comandoBD.ExecuteReader();

            OleDbDataAdapter adaptadorBD = new OleDbDataAdapter(comandoBD);
            


            adaptadorBD.Fill(objDS, "SOCIOS");
            
            DataTable dt = objDS.Tables["SOCIOS"];

            if (lectorBd.HasRows) //SI TIENE FILAS
            {
                bool Find = false;
                

                while (lectorBd.Read()) //mientras pueda leer, mostrar (leer)
                {
                    if ((lectorBd[2].ToString()) == codigo)
                    {

                        datosTabla += "-" + lectorBd[0]; //dato d la comlumna 0
                        DialogResult respuesta = MessageBox.Show(" El Cliente " + lectorBd[2] + " existe en el sistema" + " ¿Desea cambiar su estado a activo?", "Consulta", MessageBoxButtons.YesNo);

                        if (respuesta == DialogResult.Yes)
                        {

                            foreach(DataRow dr in dt.Rows)
                            {
                                dr.BeginEdit();
                                dr["ESTADO CLIENTE"] = "ACTIVO";
                                dr.EndEdit();
                            }
                        }
                        Find = true;
                        break;
                    }

                }
                if (Find == false)
                {

                    MessageBox.Show("NO Existente", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  
                }
            }

            
        }
    }
}
