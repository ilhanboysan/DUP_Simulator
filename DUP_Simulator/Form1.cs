using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static DUP_Simulator.Form1;

namespace DUP_Simulator
{
    public partial class Form1 : Form
    {
        private string lastSelectedLastOperation = string.Empty;
        private SqlConnection connection;
        /*MACHINE ID*/
        string machineID = "151";
        /*PART REFERENCE CODE*/
        string ConnectorPartReferenceCode = "42043496";
        string HPCBPartReferenceCode = "42014936";
        string OMVPartReferenceCode = "28675840";
        string BodyPartReferenceCode = "28675818";
        /*OPERASYON NO*/
        string ConnectorOperasyonNo = "111";
        string HPCBOperasyonNo = "1111";
        string OMVOperasyonNo = "221";
        string BodyOperasyonNo = "222";

        public Form1()
        {
            InitializeComponent();
            InitializeDefaultValues();
            InitializeDatabaseConnection();
            FillMachineNamesComboBox();
            BindDataGridView("42043496");
            BindDataGridView("42014936");
            BindDataGridView("28675840");
            BindDataGridView("28675818");
            ConnectorSeriNotxt.TextChanged += ConnectorSeriNotxt_TextChanged;




        }
        private void Form1_Load(object sender, EventArgs e)
        {
                 
        }
        private void ConnectorSeriNotxt_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ConnectorSeriNotxt.Text))
            {
                ComponentPairing();
            }
        }
        private void InitializeDefaultValues()
        {
            /*CONNECTOR*/
            ConnectorMachineID.Text = machineID;
            ConnectorMachineID.ReadOnly = true;
            ConnectorOpNo.Text = ConnectorOperasyonNo;
            ConnectorOpNo.ReadOnly = true;
            ConnectorPartRef.Text = ConnectorPartReferenceCode;
            ConnectorPartRef.ReadOnly = true;
            /*HPCB*/
            HPCBMachineID.Text = machineID;
            HPCBMachineID.ReadOnly = true;
            HPCBOpNo.Text = HPCBOperasyonNo;
            HPCBOpNo.ReadOnly = true;
            HPCBPartRef.Text = HPCBPartReferenceCode;
            HPCBPartRef.ReadOnly = true;
            /*OMV*/
            OmvMachineID.Text = machineID;
            OmvMachineID.ReadOnly = true;
            OmvOpNo.Text = OMVOperasyonNo;
            OmvOpNo.ReadOnly = true;
            OmvPartRef.Text = OMVPartReferenceCode;
            OmvPartRef.ReadOnly = true;
            /*BODY*/
            BodyMachineId.Text = machineID;
            BodyMachineId.ReadOnly = true;
            BodyOpNo.Text = BodyOperasyonNo;
            BodyOpNo.ReadOnly = true;
            BodyPartRef.Text = BodyPartReferenceCode;
            BodyPartRef.ReadOnly = true;
            ///////
            ConnectorDmtxt.ReadOnly = true;
            ConnectorSeriNotxt.ReadOnly = true;
            HpcbDmtxt.ReadOnly = true;
            HpcbSeriNotxt.ReadOnly = true;
            OmvDmtxt.ReadOnly = true;
            OmvSeriNotxt.ReadOnly = true;
            BodyDmtxt.ReadOnly = true;
            BodySeriNotxt.ReadOnly = true;
            //////////////////////
            Machinecmb.DropDownStyle = ComboBoxStyle.DropDownList;
            OperationCmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnHeaderCreate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ConnectorPartReferenceCode))
            {
                var result = CreateDmProduction(ConnectorOperasyonNo, ConnectorPartReferenceCode);
                string dm = result.Dm;
                string seriNo = result.SerialNo;
                string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
                string sqlQuery = "INSERT INTO ContinueData (PartRef, Dm, SeriNo, LastOperation) VALUES (@PartRef, @Dm, @SeriNo, @LastOperation)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@PartRef", ConnectorPartReferenceCode);
                        cmd.Parameters.AddWithValue("@Dm", dm);
                        cmd.Parameters.AddWithValue("@SeriNo", seriNo);
                        cmd.Parameters.AddWithValue("@LastOperation", "0");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            ConnectorDmtxt.Text = string.Empty;
                            ConnectorSeriNotxt.Text = string.Empty;
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    RefreshContinueData();
                }
            }
        }
        private void btnHPCBCreater_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HPCBPartReferenceCode))
            {
                var result = CreateDmProduction(HPCBOperasyonNo, HPCBPartReferenceCode);
                string dm = result.Dm;
                string seriNo = result.SerialNo;
                string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
                string sqlQuery = "INSERT INTO ContinueData (PartRef, Dm, SeriNo, LastOperation) VALUES (@PartRef, @Dm, @SeriNo, @LastOperation)";
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@PartRef", HPCBPartReferenceCode);
                        cmd.Parameters.AddWithValue("@Dm", dm);
                        cmd.Parameters.AddWithValue("@SeriNo", seriNo);
                        cmd.Parameters.AddWithValue("@LastOperation", "0");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            HpcbDmtxt.Text = string.Empty;
                            HpcbSeriNotxt.Text = string.Empty;
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    RefreshContinueData();
                }
            }
        }
        private void OmvCreateButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(OMVPartReferenceCode))
            {
                var result = CreateDmProduction(OMVOperasyonNo, OMVPartReferenceCode);
                string dm = result.Dm;
                string seriNo = result.SerialNo;
                string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
                string sqlQuery = "INSERT INTO ContinueData (PartRef, Dm, SeriNo, LastOperation) VALUES (@PartRef, @Dm, @SeriNo, @LastOperation)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.Parameters.AddWithValue("@PartRef", OMVPartReferenceCode);
                        cmd.Parameters.AddWithValue("@Dm", dm);
                        cmd.Parameters.AddWithValue("@SeriNo", seriNo);
                        cmd.Parameters.AddWithValue("@LastOperation", "0");
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            OmvDmtxt.Text = string.Empty;
                            OmvSeriNotxt.Text = string.Empty;
                        }
                        else
                        {

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    OvmProcedure(seriNo);
                    RefreshContinueData();
                }
            }
        }
        private void BodyCreateBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BodyPartReferenceCode))
            {
                var result = CreateDmProduction(BodyOperasyonNo, BodyPartReferenceCode);

                string dm = result.Dm;
                string seriNo = result.SerialNo;

                string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
                string sqlQuery = "INSERT INTO ContinueData (PartRef, Dm, SeriNo, LastOperation) VALUES (@PartRef, @Dm, @SeriNo, @LastOperation)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
                {
                    try
                    {
                        connection.Open();

                        cmd.Parameters.AddWithValue("@PartRef", BodyPartReferenceCode);
                        cmd.Parameters.AddWithValue("@Dm", dm);
                        cmd.Parameters.AddWithValue("@SeriNo", seriNo);
                        cmd.Parameters.AddWithValue("@LastOperation", "0");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {

                            BodyDmtxt.Text = string.Empty;
                            BodySeriNotxt.Text = string.Empty;
                        }
                        else
                        {

                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    RefreshContinueData();
                }
            }
        }
        private void RequestForOperation(string opNo, string partref, string machineId, string seriNo, string? compSeriNo)
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string procedureName = "trizm_sp_RequestForOperation";
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PartRef", partref);
                        command.Parameters.AddWithValue("@MachineID", machineId);
                        command.Parameters.AddWithValue("@SerialNo", seriNo);
                        command.Parameters.AddWithValue("@RequestedOperationNo", opNo);
                        command.Parameters.AddWithValue("@ComponentSerialNo", compSeriNo);
                        SqlParameter returnResultParam = new SqlParameter("@returnResult", SqlDbType.VarChar, 200);
                        returnResultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(returnResultParam);
                        command.ExecuteNonQuery();
                        string result = returnResultParam.Value.ToString();
                        MessageBox.Show("INFO: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void LogDataOperation(string opNo, string partref, string machineId, string seriNo, string? compSeriNo)
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    string procedureName = "trizm_sp_LogDataOperation";

                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PartRef", partref);
                        command.Parameters.AddWithValue("@MachineID", machineId);
                        command.Parameters.AddWithValue("@SerialNo", seriNo);
                        command.Parameters.AddWithValue("@OperationStatus", "PASS");
                        command.Parameters.AddWithValue("@OperationNo", opNo);
                        command.Parameters.AddWithValue("@ComponentSerialNo", compSeriNo);
                        SqlParameter returnResultParam = new SqlParameter("@returnResult", SqlDbType.VarChar, 200);
                        returnResultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(returnResultParam);
                        command.ExecuteNonQuery();
                        string result = returnResultParam.Value.ToString();
                        MessageBox.Show("INFO: " + result);

                        string resultValue = GetResultValue(result);

                        if (resultValue == "10")
                        {
                            string opInfo = OperationCmb.SelectedItem?.ToString();
                            string opNo2 = null;

                            if (!string.IsNullOrWhiteSpace(opInfo))
                            {
                                opNo2 = opInfo.Substring(0, opInfo.IndexOf("-"));
                            }
                            else
                            {
                                MessageBox.Show("Operasyon seçilmedi. Lütfen bir operasyon seçin.");
                                return;
                            }
                            string connectionString2 = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
                            using (SqlConnection connection = new SqlConnection(connectionString2))
                            {
                                connection.Open();
                                bool atLeastOneSuccess = false;
                                string[] partRefs = { ConnectorPartRef.Text, HPCBPartRef.Text, OmvPartRef.Text, BodyPartRef.Text };
                                string[] dms = { ConnectorDmtxt.Text, HpcbDmtxt.Text, OmvDmtxt.Text, BodyDmtxt.Text };
                                string[] seriNos = { ConnectorSeriNotxt.Text, HpcbSeriNotxt.Text, OmvSeriNotxt.Text, BodySeriNotxt.Text };

                                for (int i = 0; i < partRefs.Length; i++)
                                {
                                    if (string.IsNullOrWhiteSpace(partRefs[i]) || string.IsNullOrWhiteSpace(dms[i]) || string.IsNullOrWhiteSpace(seriNos[i]))
                                    {
                                        continue;
                                    }
                                    using (SqlCommand cmd = new SqlCommand("UPDATE ContinueData " +
                                             "SET LastOperation = @LastOperation " +
                                             "WHERE Dm = @Dm AND Name = 'Connector'", connection))
                                    {
                                        cmd.Parameters.AddWithValue("@PartRef", partRefs[i]);
                                        cmd.Parameters.AddWithValue("@Dm", dms[i]);
                                        cmd.Parameters.AddWithValue("@SeriNo", seriNos[i]);
                                        cmd.Parameters.AddWithValue("@LastOperation", opNo);
                                        SqlDataReader reader = cmd.ExecuteReader();
                                        reader.Close();


                                    }

                                }

                            }
                        }




                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);


            }

        }
        private string GetResultValue(string result)
        {
            int colonIndex = result.IndexOf(':');
            if (colonIndex >= 0)
            {

                return result.Substring(0, colonIndex);
            }

            return result;
        }
        private void CreateDmProduction(string partReference)
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            try
            {
                int machineId = Convert.ToInt32(HPCBMachineID.Text);
                int opNo = Convert.ToInt32(HPCBOpNo.Text);
                using (connection)
                {
                    connection.Open();
                    string procedureName = "trizm_sp_createdatamatrix";
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MachineId", machineId);
                        command.Parameters.AddWithValue("@OpNo", opNo);
                        command.Parameters.AddWithValue("@PartRef", partReference);
                        SqlParameter returnResultParam = new SqlParameter("@returnResult", SqlDbType.VarChar, 200);
                        returnResultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(returnResultParam);
                        command.ExecuteNonQuery();
                        string result = returnResultParam.Value.ToString();
                        MessageBox.Show("Successful: " + result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private SpResult CreateDmProduction(string opNo, string partReference)
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
            SpResult spResult = new SpResult();
            try
            {
                int machineId = Convert.ToInt32(HPCBMachineID.Text);
                using (connection)
                {
                    connection.Open();
                    string procedureName = "trizm_sp_createdatamatrix";
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MachineId", machineId);
                        command.Parameters.AddWithValue("@OpNo", opNo);
                        command.Parameters.AddWithValue("@PartRef", partReference);
                        SqlParameter returnResultParam = new SqlParameter("@returnResult", SqlDbType.VarChar, 200);
                        returnResultParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(returnResultParam);
                        command.ExecuteNonQuery();
                        var result = returnResultParam.Value.ToString();
                        int dmStartIndex = result.IndexOf(":") + 1;
                        int dmEndIndex = result.IndexOf(" - SeriNo:");
                        if (dmStartIndex >= 0 && dmEndIndex >= 0)
                        {
                            string dmValue = result.Substring(dmStartIndex, dmEndIndex - dmStartIndex);
                            spResult.Dm = dmValue;
                        }
                        int serialNoStartIndex = result.LastIndexOf(":") + 1;
                        if (serialNoStartIndex >= 0)
                        {
                            string serialNoValue = result.Substring(serialNoStartIndex);
                            spResult.SerialNo = serialNoValue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return spResult;
        }
        private void InitializeDatabaseConnection()
        {

            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            connection = new SqlConnection(connectionString);
        }
        private void FillMachineNamesComboBox()
        {
            try
            {
                connection.Open();
                string query = "SELECT MachineID, MachineName FROM cfgMachines";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    Machinecmb.Items.Clear();
                    while (reader.Read())
                    {
                        Machinecmb.Items.Add((reader["MachineID"] + "-" + reader["MachineName"]).ToString());
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı hatası: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void FillOperatorsComboBox(string machineId)
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            SqlConnection connection = new SqlConnection(connectionString);
            string sqlQuery = "SELECT    mc.MachineID,  mo.OperationNo, op.OperationDescription " +
                              "FROM    cfgMachines AS mc " +
                              "INNER JOIN   cfgMachineOperations AS mo ON mc.MachineID = mo.MachineID " +
                              "INNER JOIN cfgOperations as op ON mo.OperationNo=op.OperationNo " +
                              "WHERE mc.MachineID = @machineId";
            SqlCommand command = new SqlCommand(sqlQuery, connection);
            command.Parameters.AddWithValue("@machineId", machineId);
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                OperationCmb.Items.Clear();
                while (reader.Read())
                {
                    OperationCmb.Items.Add(reader["OperationNo"].ToString() + "-" + reader["OperationDescription"].ToString());

                }
            }
            connection.Close();
        }
        private void Machinecmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Machinecmb.SelectedItem != null)
            {
                machineID = Machinecmb.SelectedItem.ToString();
                machineID = machineID.Substring(0, machineID.IndexOf("-"));
                FillOperatorsComboBox(machineID);
            }
        }
        private void btnLogDataOperation_Click(object sender, EventArgs e)
        {
            string opInfo = OperationCmb.SelectedItem.ToString();
            string opNo = opInfo.Substring(0, opInfo.IndexOf("-"));

            switch (opNo)
            {
                case "111":
                    RequestForOperation(opNo,
                      ConnectorPartReferenceCode,
                      machineID,
                      ConnectorDmtxt.Text,
                      HpcbDmtxt.Text);
                    LogDataOperation(opNo,
                        ConnectorPartReferenceCode,
                        machineID,
                        ConnectorDmtxt.Text,
                        HpcbDmtxt.Text);
                    RefreshContinueData();
                    ComponentPairing();
                    break;
                case "1111":
                    RequestForOperation(opNo, HPCBPartReferenceCode, machineID, HpcbDmtxt.Text, null);
                    LogDataOperation(opNo, HPCBPartReferenceCode, machineID, HpcbDmtxt.Text, null);
                    RefreshContinueData();
                    ComponentPairing();
                    break;
                case "121":

                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, HpcbDmtxt.Text);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, HpcbDmtxt.Text);
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "122":

                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, "");
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, "");
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "131":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, "");
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, "");
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "132":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, "");
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, "");
                    RefreshContinueData();
                    ComponentPairing();
                    break;
                case "221":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, OmvDmtxt.Text);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, OmvDmtxt.Text);
                    RefreshContinueData();
                    ComponentPairing();
                    break;
                case "222":
                    BodyProcedure();
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, BodyDmtxt.Text);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, BodyDmtxt.Text);
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "223":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "300":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "400":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "500":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "600":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    RefreshContinueData();
                    ComponentPairing();

                    break;
                case "700":
                    RequestForOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    LogDataOperation(opNo, ConnectorPartReferenceCode, machineID, ConnectorDmtxt.Text, null);
                    RefreshContinueData();
                    ComponentPairing();
                    MessageBox.Show("Finish");
                    break;
                default:
                    break;
            }
        }





        private void txtbtn_Click(object sender, EventArgs e)
        {
            string opInfo = OperationCmb.SelectedItem?.ToString();
            string opNo = null;

            if (!string.IsNullOrWhiteSpace(opInfo))
            {
                opNo = opInfo.Substring(0, opInfo.IndexOf("-"));
            }
            else
            {
                MessageBox.Show("Operasyon seçilmedi. Lütfen bir operasyon seçin.");
                return;
            }
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            string sqlQuery = "UPDATE ContinueData " +
                "SET LastOperation = @LastOperation, " +
                    "EntryDate = GETDATE() " +
                "WHERE Dm = @Dm";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sqlQuery, connection))
            {
                try
                {
                    connection.Open();
                    bool atLeastOneSuccess = false;
                    string[] partRefs = { ConnectorPartRef.Text, HPCBPartRef.Text, OmvPartRef.Text, BodyPartRef.Text };
                    string[] dms = { ConnectorDmtxt.Text, HpcbDmtxt.Text, OmvDmtxt.Text, BodyDmtxt.Text };
                    string[] seriNos = { ConnectorSeriNotxt.Text, HpcbSeriNotxt.Text, OmvSeriNotxt.Text, BodySeriNotxt.Text };
                    for (int i = 0; i < partRefs.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(partRefs[i]) || string.IsNullOrWhiteSpace(dms[i]) || string.IsNullOrWhiteSpace(seriNos[i]))
                        {
                            continue;
                        }
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@PartRef", partRefs[i]);
                        cmd.Parameters.AddWithValue("@Dm", dms[i]);
                        cmd.Parameters.AddWithValue("@SeriNo", seriNos[i]);
                        cmd.Parameters.AddWithValue("@LastOperation", opNo);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            atLeastOneSuccess = true;
                        }
                    }
                    if (atLeastOneSuccess)
                    {
                        ConnectorDmtxt.Clear();
                        ConnectorSeriNotxt.Clear();
                        HpcbDmtxt.Clear();
                        HpcbSeriNotxt.Clear();
                        OmvDmtxt.Clear();
                        OmvSeriNotxt.Clear();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void SavedData()
        {



            string opInfo = OperationCmb.SelectedItem?.ToString();
            string opNo = null;

            if (!string.IsNullOrWhiteSpace(opInfo))
            {
                opNo = opInfo.Substring(0, opInfo.IndexOf("-"));
            }
            else
            {
                MessageBox.Show("Operasyon seçilmedi. Lütfen bir operasyon seçin.");
                return;
            }
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                bool atLeastOneSuccess = false;
                string[] partRefs = { ConnectorPartRef.Text, HPCBPartRef.Text, OmvPartRef.Text, BodyPartRef.Text };
                string[] dms = { ConnectorDmtxt.Text, HpcbDmtxt.Text, OmvDmtxt.Text, BodyDmtxt.Text };
                string[] seriNos = { ConnectorSeriNotxt.Text, HpcbSeriNotxt.Text, OmvSeriNotxt.Text, BodySeriNotxt.Text };

                for (int i = 0; i < partRefs.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(partRefs[i]) || string.IsNullOrWhiteSpace(dms[i]) || string.IsNullOrWhiteSpace(seriNos[i]))
                    {
                        continue;
                    }
                    using (SqlCommand cmd = new SqlCommand("UPDATE ContinueData " +
                             "SET LastOperation = @LastOperation " +
                             "WHERE Dm = @Dm AND Name = 'Connector'", connection))
                    {
                        cmd.Parameters.AddWithValue("@PartRef", partRefs[i]);
                        cmd.Parameters.AddWithValue("@Dm", dms[i]);
                        cmd.Parameters.AddWithValue("@SeriNo", seriNos[i]);
                        cmd.Parameters.AddWithValue("@LastOperation", opNo);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Close();


                    }

                }

            }
        }
        private void BindDataGridView(string partRefValue)
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT [Name], [Dm], [SeriNo],[LastOperation] FROM [TRDB].[dbo].[ContinueData] WHERE [PartRef] = {partRefValue}";
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (partRefValue == "42043496")
                {

                    ConnectorDataGrid.DataSource = dataTable;
                    ConnectorDataGrid.ReadOnly = true;
                    ConnectorDataGrid.Columns["LastOperation"].Visible = false;
                    ConnectorDataGrid.Columns["SeriNo"].Width = 138;
                    ConnectorDataGrid.Columns["Dm"].Width = 138;

                    foreach (DataGridViewColumn column in ConnectorDataGrid.Columns)
                    {
                        column.ReadOnly = true;
                    }
                }
                else if (partRefValue == "42014936")
                {
                    HpcbDataGrid.DataSource = dataTable;
                    HpcbDataGrid.ReadOnly = true;
                    HpcbDataGrid.Columns["LastOperation"].Visible = false;
                    HpcbDataGrid.Columns["SeriNo"].Width = 138;
                    HpcbDataGrid.Columns["Dm"].Width = 138;

                    foreach (DataGridViewColumn column in HpcbDataGrid.Columns)
                    {
                        column.ReadOnly = true;
                    }
                }
                else if (partRefValue == "28675840")
                {
                    OmvDataGrid.DataSource = dataTable;
                    OmvDataGrid.ReadOnly = true;
                    OmvDataGrid.Columns["LastOperation"].Visible = false;
                    OmvDataGrid.Columns["SeriNo"].Width = 138;
                    OmvDataGrid.Columns["Dm"].Width = 138;

                    foreach (DataGridViewColumn column in OmvDataGrid.Columns)
                    {
                        column.ReadOnly = true;
                    }
                }
                else if (partRefValue == "28675818")
                {
                    BodyDataGrid.DataSource = dataTable;
                    BodyDataGrid.ReadOnly = true;
                    BodyDataGrid.Columns["LastOperation"].Visible = false;
                    BodyDataGrid.Columns["SeriNo"].Width = 138;
                    BodyDataGrid.Columns["Dm"].Width = 138;


                    foreach (DataGridViewColumn column in BodyDataGrid.Columns)
                    {
                        column.ReadOnly = true;
                    }
                }
            }
        }
        public class SpResult
        {
            public string Dm { get; set; }
            public string SerialNo { get; set; }
        }
        private void ConnectorDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ConnectorDataGrid.Rows[e.RowIndex];
                if (e.ColumnIndex == ConnectorDataGrid.Columns["Name"].Index ||
                    e.ColumnIndex == ConnectorDataGrid.Columns["Dm"].Index ||
                    e.ColumnIndex == ConnectorDataGrid.Columns["SeriNo"].Index)
                {
                    ConnectorDmtxt.Text = row.Cells["Dm"].Value.ToString();
                    ConnectorSeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();


                }

                List<Label> labels = new List<Label>
                {
                  label33, label34, label35, label36, label37, label38,
                  label39, label40, label41, label42, label43, label44, label45, label46
                };


                int lastOperationColumnIndex = ConnectorDataGrid.Columns["LastOperation"].Index;
                string lastOperationValue = row.Cells[lastOperationColumnIndex].Value.ToString();

                if (lastSelectedLastOperation != lastOperationValue)
                {

                    foreach (Label label in labels)
                    {
                        label.BackColor = Color.Transparent;
                    }

                    switch (lastOperationValue)
                    {
                        case "111":

                            label33.BackColor = Color.Green;
                            break;
                        case "1111":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            break;
                        case "121":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            break;
                        case "122":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            break;
                        case "131":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            break;
                        case "132":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            break;
                        case "221":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            break;
                        case "222":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label40.BackColor = Color.Green;
                            break;
                        case "223":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label40.BackColor = Color.Green;
                            label41.BackColor = Color.Green;
                            break;
                        case "300":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label40.BackColor = Color.Green;
                            label41.BackColor = Color.Green;
                            label42.BackColor = Color.Green;
                            break;
                        case "400":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label40.BackColor = Color.Green;
                            label41.BackColor = Color.Green;
                            label42.BackColor = Color.Green;
                            label43.BackColor = Color.Green;
                            break;
                        case "500":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label40.BackColor = Color.Green;
                            label41.BackColor = Color.Green;
                            label42.BackColor = Color.Green;
                            label43.BackColor = Color.Green;
                            label44.BackColor = Color.Green;

                            break;
                        case "600":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label40.BackColor = Color.Green;
                            label41.BackColor = Color.Green;
                            label42.BackColor = Color.Green;
                            label43.BackColor = Color.Green;
                            label44.BackColor = Color.Green;
                            label45.BackColor = Color.Green;
                            break;
                        case "700":
                            label33.BackColor = Color.Green;
                            label34.BackColor = Color.Green;
                            label35.BackColor = Color.Green;
                            label36.BackColor = Color.Green;
                            label37.BackColor = Color.Green;
                            label38.BackColor = Color.Green;
                            label39.BackColor = Color.Green;
                            label40.BackColor = Color.Green;
                            label41.BackColor = Color.Green;
                            label42.BackColor = Color.Green;
                            label43.BackColor = Color.Green;
                            label44.BackColor = Color.Green;
                            label45.BackColor = Color.Green;
                            label46.BackColor = Color.Green;
                            break;

                    }
                }


                lastSelectedLastOperation = lastOperationValue;
            }



        }
        private void HpcbDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = HpcbDataGrid.Rows[e.RowIndex];
                if (e.ColumnIndex == ConnectorDataGrid.Columns["Name"].Index || e.ColumnIndex == ConnectorDataGrid.Columns["Dm"].Index || e.ColumnIndex == ConnectorDataGrid.Columns["SeriNo"].Index)
                {
                    HpcbDmtxt.Text = row.Cells["Dm"].Value.ToString();
                    HpcbSeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();
                }
            }
        }
        private void OmvDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = OmvDataGrid.Rows[e.RowIndex];
                if (e.ColumnIndex == ConnectorDataGrid.Columns["Name"].Index || e.ColumnIndex == ConnectorDataGrid.Columns["Dm"].Index || e.ColumnIndex == ConnectorDataGrid.Columns["SeriNo"].Index)
                {
                    OmvDmtxt.Text = row.Cells["Dm"].Value.ToString();
                    OmvSeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();
                }
            }
        }

        private void BodyDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = BodyDataGrid.Rows[e.RowIndex];

                BodyDmtxt.Text = row.Cells["Dm"].Value.ToString();
                BodySeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();
            }
        }
        private void BodyDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = BodyDataGrid.Rows[e.RowIndex];

                BodyDmtxt.Text = row.Cells["Dm"].Value.ToString();
                BodySeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();
            }
        }
        private void OmvDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = OmvDataGrid.Rows[e.RowIndex];

                OmvDmtxt.Text = row.Cells["Dm"].Value.ToString();
                OmvSeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();
            }
        }
        private void HpcbDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = HpcbDataGrid.Rows[e.RowIndex];

                HpcbDmtxt.Text = row.Cells["Dm"].Value.ToString();
                HpcbSeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();
            }
        }
        private void ConnectorDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ConnectorDataGrid.Rows[e.RowIndex];

                ConnectorDmtxt.Text = row.Cells["Dm"].Value.ToString();
                ConnectorSeriNotxt.Text = row.Cells["SeriNo"].Value.ToString();
            }
        }
        private void RefreshContinueData()
        {
            string selectedPartRefValueCon = "42043496";
            string selectedPartRefValueHpcb = "42014936";
            string selectedPartRefValueOmv = "28675840";
            string selectedPartRefValueBody = "28675818";
            BindDataGridView(selectedPartRefValueCon);
            BindDataGridView(selectedPartRefValueHpcb);
            BindDataGridView(selectedPartRefValueOmv);
            BindDataGridView(selectedPartRefValueBody);
        }
        private void OvmProcedure(string OmvSeriNo)
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("trizm_sp_LogDataOperation", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OperationNo", "3152");
                        cmd.Parameters.AddWithValue("@SerialNo", OmvSeriNo);
                        cmd.Parameters.AddWithValue("@MachineID", "77140");
                        cmd.Parameters.AddWithValue("@PartRef", "28675840");
                        cmd.Parameters.AddWithValue("@OperationStatus", "PASS");
                        SqlParameter returnResultParam = new SqlParameter("@returnResult", SqlDbType.VarChar, 200);
                        returnResultParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(returnResultParam);
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }
        private void BodyProcedure()
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("trizm_sp_LogDataOperation", connection))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@OperationNo", "2211");
                        cmd.Parameters.AddWithValue("@SerialNo", BodySeriNotxt.Text);
                        cmd.Parameters.AddWithValue("@MachineID", "2191");
                        cmd.Parameters.AddWithValue("@PartRef", "28675818");
                        cmd.Parameters.AddWithValue("@OperationStatus", "PASS");
                        SqlParameter returnResultParam = new SqlParameter("@returnResult", SqlDbType.VarChar, 200);
                        returnResultParam.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(returnResultParam);
                        int rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                }
            }
        }
        private void ComponentPairing()
        {
            string connectionString = "Server=BORG-W10-LDRPH2;Database=TRDB;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT ComponentSerialNo FROM (SELECT ComponentSerialNo FROM traRequestForOperationLog " +
                                  "WHERE SerialNo = @SerialNo AND ComponentSerialNo IS NOT NULL AND ComponentSerialNo != '' " +
                                  "GROUP BY ComponentSerialNo) tt, traProducts prd WHERE tt.ComponentSerialNo = prd.RailBodyRef";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@SerialNo", ConnectorSeriNotxt.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        listBox1.Items.Clear();

                        while (reader.Read())
                        {
                            string componentSerialNo = reader["ComponentSerialNo"].ToString();
                            listBox1.Items.Add(componentSerialNo);
                        }
                    }
                }
            }
        }
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            string selectedValue = listBox1.SelectedItem?.ToString();
            if (selectedValue != null)
            {
                bool foundInHpcb = false;
                foreach (DataGridViewRow row in HpcbDataGrid.Rows)
                {
                    string dmValue = row.Cells["DM"].Value?.ToString();
                    if (dmValue == selectedValue)
                    {
                        row.Selected = true;
                        HpcbDataGrid.CurrentCell = HpcbDataGrid["DM", row.Index];
                        foundInHpcb = true;
                        HpcbDmtxt.Text = dmValue;
                        HpcbSeriNotxt.Text = row.Cells["SeriNo"].Value?.ToString();
                        break;
                    }
                }
                if (!foundInHpcb)
                {
                    bool foundInOmv = false;
                    foreach (DataGridViewRow row in OmvDataGrid.Rows)
                    {
                        string omvValue = row.Cells["DM"].Value?.ToString();
                        if (omvValue == selectedValue)
                        {
                            row.Selected = true;
                            OmvDataGrid.CurrentCell = OmvDataGrid["DM", row.Index];
                            foundInOmv = true;
                            OmvDmtxt.Text = omvValue;
                            OmvSeriNotxt.Text = row.Cells["SeriNo"].Value?.ToString();
                            break;
                        }
                    }
                    if (!foundInOmv)
                    {
                        foreach (DataGridViewRow row in BodyDataGrid.Rows)
                        {
                            string bodyValue = row.Cells["DM"].Value?.ToString();
                            if (bodyValue == selectedValue)
                            {
                                row.Selected = true;
                                BodyDataGrid.CurrentCell = BodyDataGrid["DM", row.Index];
                                BodyDmtxt.Text = bodyValue;
                                BodySeriNotxt.Text = row.Cells["SeriNo"].Value?.ToString();
                                break;
                            }
                        }
                    }
                }
            }
        }
       
        
    }
}

