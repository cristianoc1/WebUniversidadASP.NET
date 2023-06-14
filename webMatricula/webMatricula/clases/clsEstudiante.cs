using libConexionBD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using libLlenarGrids;
using libLlenarRBList;

namespace webMatricula.clases
{
    public class clsEstudiante
    {
        #region "Atributos / Propiedades" 
        private string strApp;
        public string Codigo { get; set; }
        public int nroDocumento { get; set; }
        public string Nombre { get; set; }
        public int Programa { get; set; }
        public bool Activo { get; set; }
        public int Jornada { get; set; }
        public string Observacion { get; set; }
        public int Facultad { get; set; }
        public string nombrePrograma { get; set; }
        public string Respuesta { get; private set; }
        public string Error { get; private set; }
        private string strSQL;
        private clsConexionBD objCnx;
        private SqlDataReader readerLocal;
        #endregion

        #region "Constructor "
        public clsEstudiante(string nombreApp)
        {
            strApp = nombreApp;
            Codigo = string.Empty;
            nroDocumento = 0;
            Nombre = string.Empty;
            Programa = 0;
            Activo = false;
            Jornada = 0;
            Observacion = string.Empty;
            Facultad = 0;
            strSQL = string.Empty;
            Respuesta = string.Empty;
            Error = string.Empty;
        }

        public clsEstudiante(string nombreApp, string Carnet, int nroDoc, string Nombre,
        int idPrograma, bool Activo, int idJornada, string Observaciones)
        {
            strApp = nombreApp;
            Codigo = Carnet;
            nroDocumento = nroDoc;
            this.Nombre = Nombre;
            Programa = idPrograma;
            this.Activo = Activo;
            Jornada = idJornada;
            Observacion = Observaciones;
            Facultad = 0;
            strSQL = string.Empty;
            Respuesta = string.Empty;
            Error = string.Empty;
        }

        #endregion

        #region    "Metodos  Privados" 
        private bool validar()
        {
            if (string.IsNullOrEmpty(Codigo))
            {
                Error = "Nro. de Carne no va lido";
                return false;
            }

            if (nroDocumento <= 0)
            {
                Error = "Nro. de Documento no valido";
                return false;
            }

            if (string.IsNullOrEmpty(Nombre))
            {
                Error = "Nombre no valido";
                return false;
            }

            if (Programa <= 0)
            {
                Error = "Seleccione un programa";
                return false;
            }

            return true;

        }



        private bool Grabar()
        {
            try
            {
                objCnx = new clsConexionBD(strApp);
                objCnx.SQL = strSQL;
                int Act = (Activo) ? 1 : 0;
                //Enviar parametros requeridos por el USP
                if (!objCnx.agregarParametro(System.Data.ParameterDirection.Input, "@strCodigo", System.Data.SqlDbType.VarChar,
                10, Codigo))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.agregarParametro(System.Data.ParameterDirection.Input, "@intNroDoc", System.Data.SqlDbType.Int,
                4, nroDocumento))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.agregarParametro(System.Data.ParameterDirection.Input, "@strNombre", System.Data.SqlDbType.VarChar, 50, Nombre))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.agregarParametro(System.Data.ParameterDirection.Input, "@intPrograma", System.Data.SqlDbType.Int,
            4, Programa))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.agregarParametro(System.Data.ParameterDirection.Input, "@ bitActivo", System.Data.SqlDbType.Bit, 1, Act))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.agregarParametro(System.Data.ParameterDirection.Input, "@intldjor", System.Data.SqlDbType.Int, 4, Jornada))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.agregarParametro(System.Data.ParameterDirection.Input, "@strObserv", System.Data.SqlDbType.VarChar, 500, Observacion))
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                if (!objCnx.consultarValorUnico(true))  // Ejecutar stored procedure asincronicamente  --> true
                {
                    Error = objCnx.Error;
                    objCnx = null;
                    return false;
                }

                Respuesta = objCnx.vrUnico.ToString().Trim();
                objCnx = null;
                return true;
            }

            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }


        #endregion

        #region  "Metodos Publicos "
        public bool llenarGrid(GridView gridALlenar, int idPrograma)
        {
            try
            {
                if (gridALlenar == null)
                {
                    Error = "Grid es nulo";
                    return false;
                }

                strSQL = "Exec usp_EstudiantesXPrograma " + idPrograma + ";";
                clsGenerales objllenar = new clsGenerales(strApp);
                if (!objllenar.llenarGrid(gridALlenar, strSQL))
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


        public bool buscarMaestro(string Carnet)
        {
            try
            {
                if (string.IsNullOrEmpty(Carnet))
                {
                    Error = "Carne no valido";
                    return false;
                }

                strSQL = "EXEC USP_Estudiante_BuscarXCodigo '" + Carnet + "'; ";
                objCnx = new clsConexionBD(strApp);
                objCnx.SQL = strSQL;
                if (!objCnx.Consultar(false))  // Ejecuta sincr6nicamente
                {
                    Error = objCnx.Error;
                    return false;
                }

                readerLocal = objCnx.dataReaderLleno;
                if (!readerLocal.HasRows)
                {
                    Error = "No se encontr6 ningun registro: " + Carnet;
                    readerLocal.Close();
                    objCnx = null;
                    return false;
                }

                readerLocal.Read();
                Codigo = readerLocal.GetString(0);
                nroDocumento = readerLocal.GetInt32(1);
                Nombre = readerLocal.GetString(2);
                Facultad = readerLocal.GetInt32(3);
                Programa = readerLocal.GetInt32(4);
                Activo = readerLocal.GetBoolean(5);
                Jornada = readerLocal.GetInt32(6);
                Observacion = readerLocal.GetString(7);
                nombrePrograma = readerLocal.GetString(8);
                readerLocal.Close();
                objCnx = null;
                return true;
            }

            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public bool grabarMaestro()
        {
            if (!validar())
                return false;
            strSQL = "USP_Estudiante_Grabar";
            return Grabar();
        }
        public bool modificarMaestro()
        {
            if (!validar())
                return false;
            strSQL = "USP_Estudiante_Modificar ";
            return Grabar();
        }




        #endregion
    }
}