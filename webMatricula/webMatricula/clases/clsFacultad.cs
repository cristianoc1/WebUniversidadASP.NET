using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

//Reeferenciar y usar


namespace webMatricula.clases
{
    public class clsFacultad
    {
        #region "Atributos / Propiedades " 
        private string strApp;
        private string strSQL;
        public string Error { get; private set; }
        #endregion

        #region "Constructor "
        public clsFacultad(string NombreApp)
        {
            strApp = NombreApp;
            strSQL = string.Empty;
            Error = string.Empty;
        }

        #endregion

        #region " Metodos Publicos"
        public bool llenarCombo(DropDownList Combo)
        {
            try
            {
                if (Combo == null)
                {
                    Error = "Combo Nulo";
                    return false;
                }

                clsGenerales objllenar = new clsGenerales(strApp);
                strSQL = " Exec USP_Facultad_BuscarGeneral;";

                if (!objllenar.llenarCombo(Combo, strSQL, "Codigo", "Nombre"))
                {
                    Error = objllenar.Error;
                    objllenar = null;
                    return false;
                }

                objllenar = null;
                return true;
            }

            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }


        #endregion
    }
}