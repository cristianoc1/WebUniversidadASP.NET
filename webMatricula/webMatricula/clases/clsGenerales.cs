using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

//Reeferenciar y usar
using libConexionBD;
using libLlenarCombos;
using libLlenarGrids;
using libLlenarRBList;

namespace webMatricula.clases
{
    public class clsGenerales
    {
        #region "Atributos/Propiedades" 
        private string strApp;
        public string Error { get; private set; }
        #endregion

        #region "Constructores"
        public clsGenerales(string NombreApp)
        {
            strApp = NombreApp;
            Error = string.Empty;
        }
        #endregion

        #region "Métodos Privados" 
        private bool validar()
        {
            if (string.IsNullOrEmpty(strApp))
            {
                Error = "Falta el nombre de la aplicación";
                return false;
            }
            return true;
        }
        #endregion

        #region " Métodos Públicos"
        public bool llenarGrid(GridView Grid, string SQL)
        {
            try

            {
                if (!validar())
                    return false;
                if (Grid == null)
                {
                    Error = "Sin Grid a llenar";
                    return false;
                }

                clsLlenarGrids objXX = new clsLlenarGrids(strApp);
                objXX.SQL = SQL;
                if (!objXX.llenarGridWeb(Grid))
                {
                    Error = objXX.Error;
                    objXX = null;
                    return false;
                }

                objXX = null;

                Grid.GridLines = GridLines.Both;
                Grid.CellPadding = 2;
                Grid.ForeColor = System.Drawing.Color.Black;
                Grid.BackColor = System.Drawing.Color.Beige;
                Grid.AlternatingRowStyle.BackColor = System.Drawing.Color.Gainsboro;
                Grid.HeaderStyle.BackColor = System.Drawing.Color.Aqua;
                return true;

            }

            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }










        public bool llenarCombo(DropDownList Combo, string SQL, string CampoPK, string CampoTexto)
        {
            try
            {
                if (!validar())
                    return false;
                if (Combo == null)
                {
                    Error = "Sin Combo a llenar";
                    return false;
                }


                if (string.IsNullOrEmpty(CampoPK))
                {
                    Error = "cb80 Falta el dato de la aplicacion";
                    return false;
                }


                if (string.IsNullOrEmpty(CampoTexto))
                {
                    Error = "cb8J Falta el dato de la aplicacion";
                    return false;
                }

                clsLlenarCombos objXX = new clsLlenarCombos(strApp);
                objXX.SQL = SQL;
                objXX.campoID = CampoPK;
                objXX.campoTexto = CampoTexto;
                if (!objXX.llenarComboWeb(Combo))
                {
                    Error = objXX.Error;
                    objXX = null;
                    return false;
                }

                objXX = null;
                return true;

            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public bool llenarRadioBL(RadioButtonList radioBL, string SQL, string CampoPK, string CampoTexto)
        {
            try
            {
                if (!validar())
                    return false;
                if (radioBL == null)
                {
                    Error = "Sin Radios a llenar";
                    return false;
                }

                if (string.IsNullOrEmpty(CampoPK))
                {
                    Error = "rb80 Falta el date de la aplicaci6n";
                    return false;
                }

                if (string.IsNullOrEmpty(CampoTexto))
                {
                    Error = "rb81 Falla el date de la aplicacion ";
                    return false;
                }

                clsLlenarRBList objXX = new clsLlenarRBList(strApp);
                objXX.SQL = SQL;
                objXX.campoID = CampoPK;
                objXX.campoTexto = CampoTexto;
                if (!objXX.llenarRadioBLWeb(radioBL))
                {
                    Error = objXX.Error;
                    objXX = null;
                    return false;
                }
                objXX = null;
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