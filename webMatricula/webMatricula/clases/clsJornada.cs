using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using libLlenarRBList;

namespace webMatricula.clases
{
    public class clsJornada
    {
        #region "Atributos / Propiedades " 
        private string strApp;
        private string strSQL;
        public string Error { get; private set; }
        #endregion

        #region "Constructor "
        public clsJornada(string NombreApp)
        {
            strApp = NombreApp;
            strSQL = string.Empty;
            Error = string.Empty;
        }

        #endregion

        #region "Metodos Publicos"
        public bool llenarRBL(RadioButtonList rblALlenar)
        {
            try
            {
                if (rblALlenar == null)
                {
                    Error = "Radio nulo";
                    return false;
                }

                clsGenerales objLlenar = new clsGenerales(strApp);
                strSQL = " Exec USP_Jornada_BuscarGeneral ;";
                if (!objLlenar.llenarRadioBL(rblALlenar, strSQL, "Codigo", "Nombre"))
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