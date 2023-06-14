using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webMatricula
{
    public partial class Formulario_web14 : System.Web.UI.Page
    {
        #region "Variables Globales"

        static string strApp;
        static int intOpcion; // 0 Nada ; 1 insert ; 2 update
        static string strCarnet;
        string strNombre, strObservac;
        int intNroDoc, intFacultad, intPrograma, intJornada;
        bool blnAct;


        #endregion

        #region "Metodos propios"

        private void Mensaje(string Msj)
        {
            this.lblMsj.Text = Msj;
        }
        private void llenarComboFac()
        {
            clases.clsFacultad objBuscar = new clases.clsFacultad(strApp);
            if (!objBuscar.llenarCombo(ddlFacultad))
            {
                Mensaje(objBuscar.Error);
                return;

            }

            llenarComboProg(Convert.ToInt32(this.ddlFacultad.SelectedValue));
            ddlPrograma_SelectedIndexChanged1(null, null);
        }
        private void llenarJornada()
        {
            clases.clsJornada objJor = new clases.clsJornada(strApp);
            if (!objJor.llenarRBL(rblJornada))
            {
                Mensaje(objJor.Error);
                return;

            }
            this.rblJornada.SelectedIndex = 0;


        }
        private void llenarComboProg(int idFac)
        {
            clases.clsPrograma objPro = new clases.clsPrograma(strApp);
            if (!objPro.llenarCombo(ddlPrograma, idFac))
            {
                Mensaje(objPro.Error);
                return;


            }
            if (this.ddlPrograma.Items.Count > 0)
            {
                intPrograma = Convert.ToInt32(this.ddlPrograma.SelectedValue);
                llenarGrid();

            }



        }
        private void llenarGrid()
        {
            clases.clsEstudiante objE = new clases.clsEstudiante(strApp);
            if (!objE.llenarGrid(grvDatos, intPrograma))
            {
                Mensaje(objE.Error);
                return;

            }
        }
        private void Limpiar()
        {
            this.txtCarne.Text = string.Empty;
            this.txtNroDoc.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.ddlFacultad.SelectedIndex = 0;  //Llenar  Combo programa acorde a Facultad
            this.ddlPrograma.Items.Clear();
            int intFacultad = Convert.ToInt16(this.ddlFacultad.SelectedValue);
            llenarComboProg(intFacultad);
            this.chkActivo.Checked = true;
            this.rblJornada.SelectedIndex = 0;
            this.txtObservac.Text = string.Empty;

        }
        private void limpiarTexto()
        {
            Mensaje(string.Empty);
            this.txtCarne.Text = string.Empty;
            this.txtNroDoc.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.chkActivo.Checked = true;
            this.rblJornada.SelectedIndex = 0;
            this.txtObservac.Text = string.Empty;
        }
        private void Buscar(string Codigo)
        {
            limpiarTexto();
            clases.clsEstudiante objBuscar = new clases.clsEstudiante(strApp);
            if (!objBuscar.buscarMaestro(Codigo))
            {
                Limpiar();
                Mensaje(objBuscar.Error);
                objBuscar = null;
                return;
            }
            this.ddlFacultad.SelectedValue = Convert.ToString(objBuscar.Facultad);
            ddlFacultad_SelectedIndexChanged1(null, null);
            intPrograma = Convert.ToInt32(objBuscar.Programa);
            this.ddlPrograma.SelectedValue = intPrograma.ToString();
            this.txtCarne.Text = objBuscar.Codigo.ToString();
            this.txtNroDoc.Text = objBuscar.nroDocumento.ToString();
            this.txtNombre.Text = objBuscar.Nombre;
            this.chkActivo.Checked = objBuscar.Activo;
            this.rblJornada.SelectedValue = Convert.ToString(objBuscar.Jornada);
            this.txtObservac.Text = objBuscar.Observacion;
            llenarGrid();
            return;

        }
        private bool Grabar()
        {
            try
            {
                strCarnet = this.txtCarne.Text.Trim().ToUpper();
                intNroDoc = Convert.ToInt32(this.txtNroDoc.Text);
                strNombre = this.txtNombre.Text.Trim();
                intPrograma = Convert.ToInt16(this.ddlPrograma.SelectedValue);
                blnAct = this.chkActivo.Checked;
                intJornada = Convert.ToInt16(this.rblJornada.SelectedValue);
                strObservac = this.txtObservac.Text.Trim();

                clases.clsEstudiante objX = new clases.clsEstudiante(strApp, strCarnet, intNroDoc, strNombre, intPrograma, blnAct,
               intJornada, strObservac);

                if (intOpcion == 1)  //Agregar - Insert
                {
                    if (!objX.grabarMaestro())
                    {
                        Mensaje(objX.Error);
                        objX = null;
                        this.txtCarne.Focus();
                        return false;
                    }
                }

                else if (intOpcion == 2)   // Modificar - Update 
                {
                    if (!objX.modificarMaestro())
                    {
                        Mensaje(objX.Error);
                        objX = null;
                        this.txtNombre.Focus();
                        return false;
                    }

                }

                else
                {
                    Mensaje("Error No clasificado -> Consultar con admin. del Sistema");
                    objX = null;
                    this.txtNombre.Focus();
                    return false;
                }

                strCarnet = objX.Codigo;
                if (strCarnet == "-1")
                {
                    Mensaje("Ya existe un registro con el mismo Carné"); objX = null;
                    this.txtNombre.Focus();
                    return false;
                }

                if (strCarnet == "O")
                {
                    Mensaje("Error al realizar el procedimiento, reintente par favor ");
                    objX = null;
                    this.txtNombre.Focus();
                    return false;
                }

                objX = null;
                this.txtCarne.Text = strCarnet;
                this.txtCarne.ReadOnly = true;
                this.txtNombre.Focus();
                Mensaje("Registro Grabado con exito");
                Buscar(strCarnet);
                return true;
            }

            catch
            {
                Mensaje("Error en el grabar");
                return false;
            }

        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                strApp = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                intOpcion = 0;
                llenarComboFac();
                llenarJornada();

            }

        }

        protected void grvDatos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtCarne_TextChanged1(object sender, EventArgs e)
        {

        }

        protected void grvDatos_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void rblJornada_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void grvDatos_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            limpiarTexto();
            string opcion = e.CommandName.ToLower();
            int index = Convert.ToInt32(e.CommandArgument);
            strCarnet = grvDatos.Rows[index].Cells[1].Text;
            if (opcion.Equals("select"))
            {
                Buscar(strCarnet);

            }
        }

        protected void ibtnBuscar_Click1(object sender, ImageClickEventArgs e)
        {
            try

            {
                Mensaje(string.Empty);
                strCarnet = this.txtCarne.Text.Trim();
                if (string.IsNullOrEmpty(strCarnet))
                {
                    Mensaje("Carné No Valido");
                    this.txtCarne.ReadOnly = false;
                    this.txtCarne.Focus();
                    return;
                }

                this.txtCarne.ReadOnly = true;
                this.ibtnBuscar.Visible = false;
                Buscar(strCarnet);
            }

            catch (Exception ex)
            {
                Mensaje("Error-> " + ex.Message);
            }
        }

        protected void ddlFacultad_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                intFacultad = Convert.ToInt16(this.ddlFacultad.SelectedValue);
                this.ddlPrograma.Items.Clear();
                if (intFacultad > 0)
                    llenarComboProg(intFacultad);

            }
            catch (Exception ex)
            {

                Mensaje("Error Ocurrido" + ex.Message);
            }


            /*{
                intFacultad = Convert.ToInt16(this.ddlFacultad.SelectedValue);
                llenarComboProg(intFacultad);
                ddlPrograma.SelectedIndex = 0;
                ddlPrograma_SelectedIndexChanged(null, null);
            }*/
        }

        protected void ddlPrograma_SelectedIndexChanged1(object sender, EventArgs e)
        {
            intPrograma = Convert.ToInt16(this.ddlPrograma.SelectedValue);
            llenarGrid();
            limpiarTexto();
        }

        protected void mnuOpciones_MenuItemClick(object sender, MenuEventArgs e)
        {
            Mensaje(string.Empty);
            this.ibtnBuscar.Visible = false;
            this.txtCarne.ReadOnly = true;
            switch (this.mnuOpciones.SelectedValue.ToLower())
            {
                case "opcbuscar":
                    {
                        intOpcion = 0;
                        Limpiar();
                        this.ibtnBuscar.Visible = true;
                        this.txtCarne.ReadOnly = false;
                        this.txtCarne.Focus();
                        break;
                    }

                case "opcagregar":
                    {
                        intOpcion = 1;
                        Limpiar();
                        this.txtCarne.ReadOnly = false;
                        this.txtCarne.Focus();
                        break;
                    }

                case "opcmodificar":
                    {
                        intOpcion = 2;
                        this.txtCarne.ReadOnly = true;
                        this.txtNombre.Focus();
                        break;
                    }

                case "opcgrabar":
                    {
                        if (intOpcion == 1 || intOpcion == 2)
                        {
                            if (Grabar()) intOpcion = 0;
                        }
                        else
                            Mensaje("Opcion no valida, agregar primero o modificar primero");
                        break;
                    }

                case "opccancelar":
                    {
                        intOpcion = 0;
                        Limpiar();
                        break;
                    }

                case "opcimprimir":
                    {
                        intOpcion = 0;
                        strCarnet = this.txtCarne.Text.Trim();
                        if (string.IsNullOrEmpty("strCarnet"))
                        {
                            Mensaje("Buscar un Estudiante para poder imprimir su informacion");
                            return;
                        }
                        break;
                    }
                default:
                    {
                        Mensaje("Opcion No Valida");
                        break;
                    }
            }
        }
    }
}