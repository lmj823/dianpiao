using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace ebankGateway
{
    class mydataset
    {     
       
        private static DataSet dzDs;
        private static DataSet dataDic;
        private static string sdt = "";
        
        public static string Sdt
        {
            get { return mydataset.sdt; }
        }
       
        public mydataset()
        {
            try
            {
                
                DD();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        
        public void DD()
        {
            int count = 0;
           
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("net");
                dt.Columns.Add("cor");
                dataDic = new DataSet();
                dataDic.Tables.Add(dt);

                string line = null;
                string id = "";
                string net = "";
                string cor = "";
                              
               

                FileStream fs = new FileStream(@"DD.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                while (true)
                {
                    line = sr.ReadLine();
                    count++;
                    if (line != null)
                    {
                        line = line.Trim();
                        if (line.Length > 0)
                        {
                            if (line.IndexOf(";") == 0)
                            {
                                continue;
                            }

                            if (line.IndexOf("[") == 0 && line.IndexOf("]") > 0)
                            {
                                id = line.Substring(line.IndexOf('[') + 1, line.IndexOf(']') - (line.IndexOf('[') + 1));
                               
                                continue;
                            }
                            else 
                            {
                                int itmp = line.IndexOf(" ");
                                net = line.Substring(0, itmp);
                                line = line.Substring(itmp).Trim();
                                itmp = line.IndexOf(" ");
                                cor = line.Substring(0, itmp);

                                DataRow dr = dataDic.Tables[0].NewRow();
                                dr["id"] = id;
                                dr["net"] = net;
                                dr["cor"] = cor;
                                dataDic.Tables[0].Rows.Add(dr);                                
                            }
                        }
                        else
                        {
                            id = "";                           
                        }

                    }
                    else
                    {
                        break;
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                LogWrite.WriteLog("原因：" + ex.Message, "'DD' 初始化失败，行" + count.ToString());
                throw ex;
            }
        }
       

       
        public static string getNet(string id,string cor)
        {
            string filter = "id='" + id + "' and cor='" + cor + "'";
            try
            {
                  
                DataRow[] rows = dataDic.Tables[0].Select(filter);
                
                if (rows.Length > 0)
                {
                    return rows[0][1].ToString();
                }
                else
                {
                    return cor;
                }
            }
            catch
            {
                return cor;
            }
        }
       
        public static string getCor(string id, string net)
        {
            string filter = "id='" + id + "' and net='" + net + "'";
            try
            {
                            
                DataRow[] rows = dataDic.Tables[0].Select(filter);
                
                if (rows.Length > 0)
                {
                    return rows[0][2].ToString();
                }
                else
                {
                    return net;
                }
            }
            catch
            {
                return net;
            }
        }

        
       
    }
}
