using ISTENE_System___RTAPSTI.MenusForms;
using ISTENE_System___RTAPSTI.Notificaciones;
using ISTENE_System___RTAPSTI.Utils;
using LayerBusiness;
using LayerEntity;
using RJCodeAdvance.RJControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTENE_System___RTAPSTI.Forms
{
    public partial class frmAlumnos : Form
    {
        //Variables para realizar la busqueda
        static Boolean pasoLoad;
        static Int32 tabInicio = 0;

        #region Instancia para llamar al formulario de mensajes Temporales
        public void Alert(string mensaje, Notify.enmType type)
        {
            Notify frm = new Notify();
            frm.showAlert(mensaje, type);
        }
        #endregion

        //Instacion para cargar propiedades de un Progressbar del FrmMenu
        private frmMenu _formularioMenu;

        public frmAlumnos(frmMenu formularioMenu)
        {
            InitializeComponent();
            _formularioMenu = formularioMenu;
            #region Creación de Columnos en el dgvAlumnos
            dgvAlumnos.Columns.Add("Numero", "N°");
            dgvAlumnos.Columns.Add("IdAlumno", "ID");
            dgvAlumnos.Columns.Add("DocAlumno", "Documento");
            dgvAlumnos.Columns.Add("CodAlumno", "Codigo");
            dgvAlumnos.Columns.Add("NomAlumno", "Nombre");
            dgvAlumnos.Columns.Add("apePatAlumno", "Apellido Paterno");
            dgvAlumnos.Columns.Add("apeMatAlumno", "Apellido Materno");
            dgvAlumnos.Columns.Add("FotoAlumno", "Foto");
            dgvAlumnos.Columns.Add("TemaSustentacion", "Tema");

            dgvAlumnos.Columns.Add("IdArea", "IdArea");
            dgvAlumnos.Columns.Add("nomArea", "Area");

            dgvAlumnos.Columns.Add("fechaSustentacion", "Fecha");
           

            dgvAlumnos.Columns.Add("IdPrimerJurado", "IdJurado1");
            dgvAlumnos.Columns.Add("nomJurado1", "Primer Jurado");
            dgvAlumnos.Columns.Add("NotaPrimerJurado", "Nota N°1");

            dgvAlumnos.Columns.Add("IdSegundoJurado", "IdJurado2");
            dgvAlumnos.Columns.Add("nomJurado2", "Segundo Jurado");
            dgvAlumnos.Columns.Add("NotaSegundoJurado", "Nota N°2");

            dgvAlumnos.Columns.Add("IdTercerJurado", "IdJurado3");
            dgvAlumnos.Columns.Add("nomJurado3", "Jurado 3");
            dgvAlumnos.Columns.Add("NotaTercerJurado", "Nota N°3");

            dgvAlumnos.Columns.Add("PromedioSustentacion", "Promedio");
            dgvAlumnos.Columns.Add("Calificacion", "Calificación");
            dgvAlumnos.Columns.Add("IdUsuario", "IdUser");
            #endregion

        }

        private void frmAlumnos_Load(object sender, EventArgs e)
        {
            txtIdAlumno.Visible = false;
            gboxBusqueda.Enabled = false;

            #region Variables para realizar la busqueda
            pasoLoad = false;
            Boolean bResult;
            pasoLoad = true;
            #endregion

            lblCalificacion.Text = "";
            txtPromedioTotal.Enabled = false;

            #region LlenarCombox para el Registro de un nuevo Alumno
            fnLLenarCboAreaSustenciacion(cboArea, 0, "", false);
            fnLLenarCboJurado(cboJurado1, 0, "", "", "", false);
            fnLLenarCboJurado(cboJurado2, 0, "", "", "", false);
            fnLLenarCboJurado(cboJurado3, 0, "", "", "", false);
            #endregion

            #region Ocultar Tablas que no son necesarias su visualización
            dgvAlumnos.Columns[1].Visible = false;
            dgvAlumnos.Columns[2].Visible = false;
            dgvAlumnos.Columns[7].Visible = false;
            dgvAlumnos.Columns[9].Visible = false;
            dgvAlumnos.Columns[10].Visible = false;
            dgvAlumnos.Columns[12].Visible = false;
            dgvAlumnos.Columns[13].Visible = false;
            dgvAlumnos.Columns[15].Visible = false;
            dgvAlumnos.Columns[16].Visible = false;
            dgvAlumnos.Columns[18].Visible = false;
            dgvAlumnos.Columns[19].Visible = false;
            dgvAlumnos.Columns[23].Visible = false;
            //Se carga el dgvAlumnos al iniciar el Formulario para tener un vista de los registros
            fnLlenardgvAlumnos();
            #endregion

        }

        #region Función para llenar el dgvAlumnos al iniciar el Formulario
        public void fnLlenardgvAlumnos()
        {
            Boolean bResul;

            if (pasoLoad)
            {
                bResul = fnBuscarAlumnos(dgvAlumnos, 0);
                if (bResul == false)
                {
                    MessageFM.Show("No se ha Encontrado ningun registro \nPorfavor Registre un nuevo Alumno ",
          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (!bResul)
                {
                    MessageFM.Show("Error al Buscar Alumnos. Comunicar a Administrador de Sistema",
          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Funcion para llenar el ComboBox de jurado
        public static List<Jurado> fnLLenarCboJurado(ComboBox cbo, Int32 IdJurado, String nomJurado, String apePatJurado, String apeMatJurado, Boolean buscar)
        {
            BuLlenarJurado buobjetoJurado = new BuLlenarJurado();
            List<Jurado> lstJurado = new List<Jurado>();
            try
            {
                lstJurado = buobjetoJurado.BuLLenarJurado(IdJurado, nomJurado, apePatJurado, apeMatJurado, buscar);
                cbo.ValueMember = "IdJurado";
                foreach (Jurado jurado in lstJurado)
                {
                    jurado.NomJurado = $"{jurado.NomJurado} {jurado.ApePatJurado} {jurado.ApeMatJurado}";
                }
                cbo.DisplayMember = "NomJurado"; // Mostrar la concatenación en el ComboBox
                cbo.DataSource = lstJurado;

                return lstJurado;
            }
            catch (Exception ex)
            {

                return lstJurado;
            }
            finally
            {
                lstJurado = null;
            }
        }
        #endregion

        #region Funcion para llenar el ComboBox de Area de Sustentación
        public static List<Area> fnLLenarCboAreaSustenciacion(ComboBox cbo, Int32 IdArea, String nomArea, Boolean buscar)
        {
            BuLlenarArea buobjetoArea = new BuLlenarArea();
            List<Area> lstArea = new List<Area>();
            try
            {
                lstArea = buobjetoArea.BuLLenarArea(IdArea, nomArea, buscar);
                cbo.ValueMember = "IdArea";
                cbo.DisplayMember = "nomArea";
                cbo.DataSource = lstArea;

                return lstArea;
            }
            catch (Exception ex)
            {

                return lstArea;
            }
            finally
            {
                lstArea = null;
            }
        }
        #endregion

        #region Activacion del Timer para realizar la busqueda de un Estudiante por medio de DNI
        private void tmTraerDatosAlumno_Tick(object sender, EventArgs e)
        {
            if (_formularioMenu != null && _formularioMenu.PbTraerDatosAlumno != null)
            {
                ProgressBar progressBar = _formularioMenu.PbTraerDatosAlumno;

                if (progressBar.Value < 90)
                {
                    progressBar.Value += 10;
                }
                else if (progressBar.Value == 90)
                {
                    progressBar.Value += 1;
                    fnTraerDatosPersona();
                }
                else if (progressBar.Value > 90 && progressBar.Value < 100)
                {
                    progressBar.Value += 1;
                    progressBar.Value = 100;
                    progressBar.Visible = false;
                }
                else
                {
                    if (progressBar.Value >= 100)
                    {
                        tmTraerDatosAlumno.Stop();
                    }
                }
            }
        }
        #endregion

        #region Funcion para Buscar un Estudiante por DNI en una API de Sunat
        public void fnTraerDatosPersona()
        {
            BuscaAlumno Api = new BuscaAlumno();
            string token = "?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJlbWFpbCI6Imh0dHBzLmZyYW56QGdtYWlsLmNvbSJ9.zZV6zWvLKoce0NNoVIN9wXnAYtx6ieduZET1ynUJgfM";
            try
            {
                if (txtDocumentoAlumno.Text.Length == 8)
                {
                    //token
                    dynamic respuesta = Api.Get("https://dniruc.apisperu.com/api/v1/dni/" + txtDocumentoAlumno.Text + token);
                    txtNomAlumno.Text = respuesta.nombres.ToString();
                    txtApePatAlumno.Text = respuesta.apellidoPaterno.ToString();
                    txtApeMatAlumno.Text = respuesta.apellidoMaterno.ToString();
                    this.Alert("Datos Encontrados", Notify.enmType.Info);
                }

                else
                {
                    this.Alert("Error Al buscar DNI", Notify.enmType.Error);
                }
            }
            catch (Exception)
            {
                this.Alert("Ingrese documento valido", Notify.enmType.Error);
            }
        }
        #endregion

        #region Button para Buscar Alumnos por DNI
        private void btnBuscarAlumno_Click(object sender, EventArgs e)
        {
            ProgressBar progressBar = _formularioMenu.PbTraerDatosAlumno;
            progressBar.Visible = true;

            progressBar.Value = 0;
            if (progressBar.Value == 0)
            {
                tmTraerDatosAlumno.Start();
            }
        }
        #endregion

        #region Button para seleccionar una imagen desde el explorador de archivos
        private void btnSeleccFotoAlumno_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.png;*.jpg; *.jpeg; *.gif; *.bmp)|*.png;*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pboxImageAlumno.Image = new Bitmap(open.FileName);
            }
        }
        #endregion

        #region Button para calcular el promedio y agregar una calificación dependiendo del promedio calculado
        private void btnCacularPromedio_Click(object sender, EventArgs e)
        {
            int promedio1, promedio2, promedio3;
            if (int.TryParse(txtPromedioJ1.Text, out promedio1) &&
                int.TryParse(txtPromedioJ2.Text, out promedio2) &&
                int.TryParse(txtPromedioJ3.Text, out promedio3))
            {
                if (promedio1 <= 20 && promedio1 >= 0 && promedio2 <= 20 && promedio2 >= 0 && promedio3 <= 20 && promedio3 >= 0)
                {
                    int PromedioFinal = promedio1 + promedio2 + promedio3;

                    int promedio = PromedioFinal / 3;
                    txtPromedioTotal.Text = promedio.ToString();
                    if (promedio > 0 && promedio < 10)
                    {
                        lblCalificacion.Text = "Reprobado";
                    }
                    else if (promedio >= 10 && promedio < 12)
                    {
                        lblCalificacion.Text = "Recuperacion";
                    }
                    else if (promedio >= 12 && promedio < 17)
                    {
                        lblCalificacion.Text = "Aprobado";
                    }
                    else if (promedio >= 17 && promedio <= 20)
                    {
                        lblCalificacion.Text = "Excelente";
                    }
                    if (promedio >= 20)
                    {
                        MessageFM.Show("El promedio final de los valores no debe superar 20.");
                    }
                }
                else
                {
                    MessageFM.Show("Los promedio deben ser menor o igual a 20.");
                }
            }
            else
            {
                MessageFM.Show("Por favor ingresa valores numéricos válidos en todos los campos.");
            }
        }
        #endregion

        #region Button para Guardar un nuevo Alumno
        private void btnGuardarAlumno_Click(object sender, EventArgs e)
        {
            String lcResultado = "";
            lcResultado = fnGuardarAlumno();
            if (lcResultado == "OK")
            {
                this.Alert("Alumno Guardado", Notify.enmType.Info);
            }
            else
            {
                this.Alert("Error al Guardar lumno. Comunicar a Administrador de Sistema", Notify.enmType.Error);
            }
        }
        #endregion

        #region Funcion para Guardar un Nuevo Alumno
        public String fnGuardarAlumno()
        {
            Alumno objAlumno = new Alumno();
            BuGuardaAlumno buObjGuardaAlumno = new BuGuardaAlumno();
            String lcValidar = "";
            try
            {
                //Datos del estudiante
                objAlumno.IdAlumno = Convert.ToInt32(txtIdAlumno.Text.Trim() == "" ? "0" : txtIdAlumno.Text.Trim());
                objAlumno.DocAlumno = Convert.ToInt32(txtDocumentoAlumno.Text.Trim());
                objAlumno.CodAlumno = Convert.ToString(txtCodAlumno.Text.Trim());
                objAlumno.NomAlumno = Convert.ToString(txtNomAlumno.Text.Trim());
                objAlumno.ApePatAlumno = Convert.ToString(txtApePatAlumno.Text.Trim());
                objAlumno.ApeMatAlumno = Convert.ToString(txtApeMatAlumno.Text.Trim());
                objAlumno.FotoAlumno = FuncionesGenerales.ConvertirImagenABytes(pboxImageAlumno.Image);
                objAlumno.TemaSustentacion = Convert.ToString(txtAreaSustentacion.Text.Trim());
                objAlumno.IdArea = Convert.ToInt32(cboArea.SelectedValue);
                objAlumno.FechaSustentacion = Convert.ToDateTime(dtFechaSustentacion.Value);
               //Datos del primer Jurado
                objAlumno.IdPrimerJurado = Convert.ToInt32(cboJurado1.SelectedValue);
                objAlumno.NotaPrimerJurado = Convert.ToDecimal(txtPromedioJ1.Text.Trim());
                //Datos del Segundo Jurado
                objAlumno.IdSegundoJurado = Convert.ToInt32(cboJurado2.SelectedValue);
                objAlumno.NotaSegundoJurado = Convert.ToDecimal(txtPromedioJ2.Text.Trim());
                //Datos del Tercer Jurado
                objAlumno.IdTercerJurado = Convert.ToInt32(cboJurado3.SelectedValue);
                objAlumno.NotaTercerJurado = Convert.ToDecimal(txtPromedioJ3.Text.Trim());
                //Promedio Total y calificación
                objAlumno.PromedioSustentacion = Convert.ToDecimal(txtPromedioTotal.Text.Trim());
                objAlumno.Calificacion = Convert.ToString(lblCalificacion.Text.Trim());
                //Agregarmos el Id de usuario desde la clase Datos Login que es la cual tiene almacenada el idUser
                objAlumno.clsUsuario = new Usuario
                {
                    IdUsuario = DatosLogin.clsUsuario.IdUsuario
                };

                if (cboArea.SelectedIndex == 0 && cboJurado1.SelectedIndex==0 && cboJurado2.SelectedIndex == 0 && cboJurado3.SelectedIndex ==0)
                {
                    MessageFM.Show("Profavor Seleccione un Item de los ComboBox", "Error al Guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }else
                {
                    lcValidar = buObjGuardaAlumno.buGuardarAlumno(objAlumno, 0).Trim();
                    fnLimpiarControles();
                }
                return lcValidar;
            }
            catch (Exception ex)
            {
                return "NO";
            }

        }
        #endregion

        #region Funcion para limpiar controles
        public void fnLimpiarControles()
        {
            txtCodAlumno.Text = "";
            txtDocumentoAlumno.Text = "";
            txtNomAlumno.Text = "";
            txtApePatAlumno.Text = "";
            txtApeMatAlumno.Text = "";
            txtAreaSustentacion.Text = "";
            cboJurado1.SelectedIndex = 0;
            cboJurado2.SelectedIndex = 0;
            cboJurado3.SelectedIndex = 0;
            txtPromedioJ1.Text = "";
            txtPromedioJ2.Text = "";
            txtPromedioJ3.Text = "";
            txtPromedioTotal.Text = "";
            lblCalificacion.Text = "";
        }
        #endregion

        #region Funcion para buscar Alumnos y llenar el dgvAlumos
        private Boolean fnBuscarAlumnos(DataGridView dgv, Int32 numPagina)
        {
            BuBuscaAlumno buobjBuscarAlumnos = new BuBuscaAlumno();
            DataTable dtAlumnos = new DataTable();
            String nomAlumno;
            Int32 filas = 20;
            DateTime fechaInicial = dtFechaInicio.Value;
            DateTime fechaFinal = dtFechaFin.Value;
            Boolean habilitarFechas = chkHabilitarFechas.Checked ? true : false;
            try
            {
                if (txtBuscarAlumno.Text == "Ingrese nombre de alumno a buscar...")
                {
                    txtBuscarAlumno.Text = "";
                }
                nomAlumno = Convert.ToString(txtBuscarAlumno.Text.ToString());
                dtAlumnos = buobjBuscarAlumnos.BuBuscarAlumnos(habilitarFechas, fechaInicial, fechaFinal, nomAlumno, numPagina);
                dgvAlumnos.Rows.Clear();
                Int32 totalResultados = dtAlumnos.Rows.Count;

                if (dtAlumnos.Rows.Count > 0)
                {
                    Int32 y;
                    if (numPagina == 0)
                    {
                        y = 0;
                    }
                    else
                    {
                        tabInicio = (numPagina - 1) * filas;
                        y = tabInicio;
                    }
                    foreach (DataRow item in dtAlumnos.Rows)
                    {

                        y++;
                        dgvAlumnos.Rows.Add(
                            y,
                            item["IdAlumno"],
                            item["DocAlumno"],
                            item["CodAlumno"],
                            item["NomAlumno"],
                            item["apePatAlumno"],
                            item["apeMatAlumno"],
                            item["FotoAlumno"],
                            item["TemaSustentacion"],
                            item["IdArea"],
                            item["nomArea"],
                            item["fechaSustentacion"],
                            item["IdPrimerJurado"],
                            item["nomJurado1"],
                            item["NotaPrimerJurado"],
                            item["IdSegundoJurado"],
                            item["nomJurado2"],
                            item["NotaSegundoJurado"],
                            item["IdTercerJurado"],
                            item["nomJurado3"],
                            item["NotaTercerJurado"],
                            item["PromedioSustentacion"],
                            item["Calificacion"],
                            item["IdUsuario"]
                        );

                    }
                    // Establece el ancho de las columnas 
                    dgvAlumnos.Columns["Numero"].Width = 30;
                    dgvAlumnos.Columns["CodAlumno"].Width = 60;
                    dgvAlumnos.Columns["NomAlumno"].Width = 140;
                    dgvAlumnos.Columns["apePatAlumno"].Width = 100;
                    dgvAlumnos.Columns["apeMatAlumno"].Width = 100;
                    dgvAlumnos.Columns["TemaSustentacion"].Width = 180;
                    dgvAlumnos.Columns["fechaSustentacion"].Width = 80;
                    dgvAlumnos.Columns["NotaPrimerJurado"].Width = 50;
                    dgvAlumnos.Columns["NotaSegundoJurado"].Width = 50;
                    dgvAlumnos.Columns["NotaTercerJurado"].Width = 50;
                    dgvAlumnos.Columns["PromedioSustentacion"].Width = 60;
                    dgvAlumnos.Columns["Calificacion"].Width = 90;
                }

                if (numPagina == 0)
                {
                    Int32 totalRegistros = Convert.ToInt32(dtAlumnos.Rows[0][0]);
                    fnCalcularPaginacion(
                        totalRegistros,
                        filas,
                        totalResultados,
                        cboPagina,
                        btnTotalPaginas,
                        btnNumFilas,
                        btnTotalReg
                    );
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                buobjBuscarAlumnos = null;
            }
        }
        #endregion

        #region Función para calcular la Paginacion del dgvAlumnos
        private void fnCalcularPaginacion(Int32 totalRegistros, Int32 filas, Int32 totalResultados, ComboBox cboPagina, RJButton btnTotalPaginas, RJButton btnNumFilas, RJButton btnTotalReg)
        {
            Int32 residuo;
            Int32 cantidadPaginas;
            residuo = totalRegistros % filas;
            if (residuo == 0)
            {
                cantidadPaginas = (totalRegistros / filas);
            }
            else
            {
                cantidadPaginas = (totalRegistros / filas) + 1;
            }

            cboPagina.Items.Clear();

            for (Int32 i = 1; i <= cantidadPaginas; i++)
            {
                cboPagina.Items.Add(i);

            }

            cboPagina.SelectedIndex = 0;
            btnTotalPaginas.Text = Convert.ToString(cantidadPaginas);
            btnNumFilas.Text = Convert.ToString(totalResultados);
            btnTotalReg.Text = Convert.ToString(totalRegistros);
        }
        #endregion

        #region Evento CelldobleClick del dgvalumnos para enviar los datos del dgv a los controles
        private void dgvAlumnos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvAlumnos.Rows[e.RowIndex];
                string data = row.Cells[1].Value.ToString();
                txtIdAlumno.Text = data;

                string data1 = row.Cells[2].Value.ToString();
                txtDocumentoAlumno.Text = data1;

                string data2 = row.Cells[3].Value.ToString();
                txtCodAlumno.Text = data2;

                string data3 = row.Cells[4].Value.ToString();
                txtNomAlumno.Text = data3;

                string data4 = row.Cells[5].Value.ToString();
                txtApePatAlumno.Text = data4;

                string data5 = row.Cells[6].Value.ToString();
                txtApeMatAlumno.Text = data5;

                byte[] data6 = (byte[])row.Cells[7].Value;
                pboxImageAlumno.Image = FuncionesGenerales.ConvertirBytesAImagen(data6);

                string data7 = row.Cells[8].Value.ToString();
                txtAreaSustentacion.Text = data7;

                string data8 = row.Cells[9].Value.ToString();
                cboArea.SelectedValue = Convert.ToInt32(data8);

                DateTime data9;
                if (DateTime.TryParse(row.Cells[11].Value.ToString(), out data9))
                {
                    dtFechaSustentacion.Value = data9;
                }

                string data10 = row.Cells[12].Value.ToString();
                cboJurado1.SelectedValue = Convert.ToInt32(data10);

                decimal data11 = (decimal)row.Cells[14].Value;
                txtPromedioJ1.Text = data11.ToString();

                string data12 = row.Cells[15].Value.ToString();
                cboJurado2.SelectedValue = Convert.ToInt32(data12);

                decimal data13 = (decimal)row.Cells[17].Value;
                txtPromedioJ2.Text = data13.ToString();

                string data14 = row.Cells[18].Value.ToString();
                cboJurado3.SelectedValue = Convert.ToInt32(data14);

                decimal data15 = (decimal)row.Cells[20].Value;
                txtPromedioJ3.Text = data15.ToString();

                decimal data16 = (decimal)row.Cells[21].Value;
                txtPromedioTotal.Text = data16.ToString();

                string data17 = row.Cells[22].Value.ToString();
                lblCalificacion.Text = data17;
            }
        }
        #endregion

        #region Evento KeyPres del txtBuscar Alumnos para realizacion la busqueda al presionar la tecla enter
        private void txtBuscarAlumno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Ningun Alumno tiene en su nombre un caracter de numero.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Boolean bResul;
            if (e.KeyChar == (Char)Keys.Enter)
            {
                if (pasoLoad)
                {
                    bResul = fnBuscarAlumnos(dgvAlumnos, 0);
                    if (!bResul)
                    {
                        MessageFM.Show("Error al Buscar Alumnos. Comunicar a Administrador de Sistema",
          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
        }
        #endregion

        #region Evento de selección del comboBox Pagina para realizar la bsuqeda
        private void cboPagina_SelectedIndexChanged(object sender, EventArgs e)
        {
            Boolean bResul;

            Int32 numPagina = Convert.ToInt32(cboPagina.Text.ToString());
            if (pasoLoad)
            {
                bResul = fnBuscarAlumnos(dgvAlumnos, numPagina);
                if (!bResul)
                {
                    MessageFM.Show("Error al Buscar Alumnos. Comunicar a Administrador de Sistema",
          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Habilitar o desabilitar Fechas para realizar el filtro de busqueda
        private void chkHabilitarFechas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHabilitarFechas.Checked)
            {
                gboxBusqueda.Enabled = true;
            }
            else
            {
                gboxBusqueda.Enabled = false;
            }
        }
        #endregion

        #region Eliminar un registro del dgvAlumnos
        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvAlumnos.SelectedRows.Count > 0)
            {
                DialogResult result = MessageFM.Show("¿Estás seguro de que deseas eliminar el alumno?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    int IdAlumno = Convert.ToInt32(dgvAlumnos.SelectedRows[0].Cells["IdAlumno"].Value);
                    buEliminarAlumno buEliminarAlumno = new buEliminarAlumno();
                    try
                    {
                        buEliminarAlumno.EliminarAlumno(IdAlumno);
                        this.Alert("Alumno Eliminado",Notify.enmType.Info);
                        fnBuscarAlumnos(dgvAlumnos, 0);
                    }
                    catch (Exception ex)
                    {
                        this.Alert("Error al eliminar el Alumno " , Notify.enmType.Error);
                    }
                }
            }
        }
        #endregion

        #region Button para buscar  Alumnos
        private void btnBuscarAlumnos_Click(object sender, EventArgs e)
        {
            Boolean bResul;

            if (pasoLoad)
            {
                bResul = fnBuscarAlumnos(dgvAlumnos, 0);
                if (bResul == false)
                {
                    MessageFM.Show("No se ha Encontrado ningun registro \nPorfavor Registre un nuevo Alumno ",
          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (!bResul)
                {
                    MessageFM.Show("Error al Buscar Alumnos. Comunicar a Administrador de Sistema",
          "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Validacion de controles
        //Validación de Datos del Alumno
        private void txtDocumentoAlumno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
                MessageFM.Show("Solo se permiten números.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtDocumentoAlumno.Text.Length >= 8 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
                MessageFM.Show("Se permiten solo 8 dígitos.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtNomAlumno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; 
                MessageFM.Show("Solo se permiten letras.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtApePatAlumno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Solo se permiten letras.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtApeMatAlumno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Solo se permiten letras.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtAreaSustentacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Solo se permiten letras.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //Validacion de TextBox de Notas de los Jurados
        private void txtPromedioJ1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Solo se permiten números.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtPromedioJ1.Text.Length >= 2 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Se permiten solo 2 dígitos.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtPromedioJ2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Solo se permiten números.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtPromedioJ2.Text.Length >= 2 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Se permiten solo 2 dígitos.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtPromedioJ3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Solo se permiten números.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (txtPromedioJ3.Text.Length >= 2 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                MessageFM.Show("Se permiten solo 2 dígitos.", "Error de entrada", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
