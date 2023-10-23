﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryBaseDeDatosSocios
{
    
    public partial class frmMain : Form
    {
        clsAccesoADatos objBaseDatos;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            objBaseDatos = new clsAccesoADatos();
            objBaseDatos.ConectarBD();

            lblEstadoDeConexion.Text = objBaseDatos.estadoDeConexion;

            lblEstadoDeConexion.BackColor = Color.Green;
            lblEstadoDeConexion.ForeColor = Color.White;

            objBaseDatos.traerDatos();

            lblDatos.Text = objBaseDatos.datosTabla;

        }
    }
}