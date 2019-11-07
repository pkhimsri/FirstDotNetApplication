using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication_TestDGV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BindGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BindGrid()
        {


                string constring = @"Data Source=.\devdbserver17;Initial Catalog=MSQDSDB_Foehl;User id = sa;password=Mind@1234";
            using (SqlConnection con = new SqlConnection(constring)){
            
                string query= "SELECT  [IDDoc]";
                       query += ",DC.[Lang]";
                       query += ",[DocName]";
                       query += ",[Status]";
	                   query += ",ST.[StatusDesc]";
                       query += ",[Type]";
	                   query += ",DT.[TypeDesc]";
                       query += ",[V1]";
                       query += ",[V2]";
                       query += ",[V3]";
	                   query += ",CAST(DC.V1 as varchar(10))  + '.' + CAST(DC.V2 as varchar(10))  + '.' + CAST(DC.V3 as varchar(10)) as VersionNo";
                       query += ",[Extension]";
	                   query += ",CONCAT( 'pics/extension/', STUFF([Extension], CHARINDEX('.', [Extension]), LEN('.'), '')  , '.png') as DocImage";
                       query += ",[IDWebPath]";
                       query += ",[IsCycle]";
                       query += ",[LangStandard]";
                       query += ",lf.files as lfs";
	                   query += ",'pics/flaggen/' + DC.LANG + '.png' as DocImage";
                       query += ",[DateCre]";
                       query += ",[IdCre]";
	                   query += ",ACCreate.AccShort    as ACCreateAccShort";
                       query += ",[DatePub]";
                       query += ",[IdPub]";
	                   query += ",ACPub.AccShort as ACPubAccShort";
                       query += ",[DateMod]";
                       query += ",[IdMod]";
	                   query += ",ACMode.AccShort as ACModeAccShort";
                       query += ",[DatePrv]";
                       query += ",[IdPrv]";
	                   query += ",ACPRV.AccShort as ACPRVAccShort";
                       query += ",[DateExp]";
                       query += ",[DoNotPublish]";
                       query += ",[IDLck]";
                       query += ",[LANotes]";
                       query += ",[Translated]";
                       query += ",[LAIgnore]";
                       query += ",[IdRel]";
	                   query += ",ACREL.AccShort";
                       query += ",[DateRel]";
                       query += ",[DocNewName]";
                       query += ",[IdProcessOwner]";
                       query += ",[DateFrozen]";
                       query += ",[IdFrozen]";
	                   query += ",(select files from DocumentFlowStatus where Name = 'clear') as lockImage";
                       query += ",sf.files as sff"; 
                       query += " FROM [MSQDSDB_FOEHL].[DBO].[DOCUMENTS] DC";
                       query += " LEFT JOIN STATUS AS ST ON   DC.STATUS = ST.IDSTATUS";
                       query += " AND ST.Lang=DC.Lang";
                       query += " LEFT JOIN DOCTYPES DT ON DT.IDTYPE = DC.TYPE";
                       query += " AND DC.Lang=DT.Lang";
                       query += " LEFT JOIN ACCOUNTS ACCreate ON ACCreate.IdAcc= DC.IdCre";
                       query += " LEFT JOIN ACCOUNTS ACMode ON ACMode.IdAcc= DC.IdMod";
                       query += " LEFT JOIN ACCOUNTS ACPRV ON ACPRV.IdAcc= DC.IdPrv";
                       query += " LEFT JOIN ACCOUNTS ACREL ON ACREL.IdAcc= DC.IDRel";
                       query += " LEFT JOIN ACCOUNTS ACPub ON ACPub.IdAcc= DC.IDPub";
                       query += " LEFT JOIN ACCOUNTS ACProcessOwner ON ACProcessOwner.IdAcc= DC.IdProcessOwner";
                       query += " LEFT JOIN SaveFiles sf on sf.name = DC.Extension";
                       query += " LEFT JOIN [dbo].[LanguageFlags] lf on lf.name = DC.Lang";
                       using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
        }
    }
}
