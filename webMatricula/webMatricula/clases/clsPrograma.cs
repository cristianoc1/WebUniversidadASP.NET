using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace webMatricula.clases
{
    public class clsPrograma
    {
        #region "Atributos / Propiedades"
        private string strApp;
        private string strSQL;
        public string Error { get; private set; }
        #endregion

        #region "Constructor "
        public clsPrograma(string nombreApp)
        {
            strApp = nombreApp;
            strSQL = string.Empty;
            Error = string.Empty;
        }

        #endregion

        #region "Metodos Publicos"
        public bool llenarCombo(DropDownList Combo, int Identif)
        {
            try
            {
                if (Combo == null || Identif <= 0)
                {
                    Error = "Combo o ldentificador no valido";
                    return false;
                }

                clsGenerales objLlenar = new clsGenerales(strApp);
                strSQL = "Exec USP_Programa_LlenarCombo " + Identif;
                if (!objLlenar.llenarCombo(Combo, strSQL, "Codigo", "Nombre"))
                {
                    Error = objLlenar.Error;
                    objLlenar = null;
                    return false;
                }

                objLlenar = null;
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