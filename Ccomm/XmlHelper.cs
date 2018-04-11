using System;
using System.Xml;
using System.Data;
using System.Collections;

namespace ebankGateway
{
    /// <summary>
    /// XmlHelper ��ժҪ˵��
    /// </summary>
    public class XmlHelper
    {
        XmlDocument doc;
        XmlNode xn;
        string path;
        XmlNamespaceManager xmlns;

        public static XmlDocument CreateXML(string rootName)
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xdoc.AppendChild(dec);
            //����һ�����ڵ㣨һ����
            XmlElement root = xdoc.CreateElement(rootName);
            xdoc.AppendChild(root);
            return xdoc;            
        }
        public static XmlDocument CreateXML()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlDeclaration dec = xdoc.CreateXmlDeclaration("1.0", "GBK", null);
            xdoc.AppendChild(dec);
            XmlElement root = xdoc.CreateElement("ebank");
            xdoc.AppendChild(root);
            XmlNode xxn = xdoc.SelectSingleNode(@"/ebank");
            XmlElement xe = xdoc.CreateElement("hostReturnCode");
            xxn.AppendChild(xe);
            xe = xdoc.CreateElement("hostErrorMessage");
            xxn.AppendChild(xe);
            xe = xdoc.CreateElement("list");
            xxn.AppendChild(xe);

            return xdoc;
        }
        //public XmlHelper(string xmlStr)
        //{
        //    doc = new XmlDocument();
        //    doc.LoadXml(xmlStr);            
        //}

        public XmlHelper(XmlDocument xmlDoc)
        {
            doc = xmlDoc;            
        }
        public XmlHelper(XmlDocument xmlDoc,string xmlnamespace)
        {
            doc = xmlDoc;
            xmlns = new XmlNamespaceManager(doc.NameTable);
            xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
        }
        public XmlHelper(string path)
        {
            doc = new XmlDocument();
            doc.Load(path);     
        }
        public XmlHelper(string path, string xmlnamespace)
        {
            doc = new XmlDocument();
            doc.Load(path);
            xmlns = new XmlNamespaceManager(doc.NameTable);
            xmlns.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");            
        }
        public XmlHelper(string path, bool isFile)
        {
            doc = new XmlDocument();
            doc.Load(path);
        }
                
        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">���������ǿ�ʱ���ظ�����ֵ�����򷵻ش���ֵ</param>
        /// <returns>string</returns>
        /**************************************************
         * ʹ��ʾ��:
         * Read("/Node", "")
         * Read("/Node/Element[@Attribute='Name']", "Attribute")
         ************************************************/
        private string Read(string node, string attribute)
        {
            string value = "";
            try
            {                
                xn = doc.SelectSingleNode(node);
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch { }
            return value;
        }
        /// <summary>
        /// ��ȡָ���ڵ�Ĵ���ֵ
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <returns></returns>
        public string ReadByNode(string node)
        {
            return Read(node, "");
        }
        /// <summary>
        /// ��ȡָ�����ԵĽڵ�Ĵ���ֵ
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">��������</param>
        /// <param name="value">����ֵ</param>
        /// <returns></returns>
        public string ReadByAttribute(string node,string attribute,string value)
        {
            return Read(node + "[@" + attribute + "='" + value + "']", "");
        }
        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">��������</param>
        /// <returns></returns>
        public string ReadAttributeValue(string node, string attribute)
        {
            return Read(node, attribute);
        }



        /// <summary>
        /// ��������
        /// </summary>        
        /// <param name="node">�ڵ�</param>
        /// <param name="element">Ԫ�������ǿ�ʱ������Ԫ�أ������ڸ�Ԫ���в�������</param>
        /// <param name="attribute">���������ǿ�ʱ�����Ԫ������ֵ���������Ԫ��ֵ</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        /**************************************************
         * ʹ��ʾ��:
         * Insert("/Node", "Element", "", "Value")
         * Insert("/Node", "Element", "Attribute", "Value")
         * Insert("/Node", "", "Attribute", "Value")
         ************************************************/
        private void Insert(string node, string element, string attribute, string value)
        {
            try
            {                
                xn = doc.SelectSingleNode(node);
                if (element.Equals(""))
                {
                    if (!attribute.Equals(""))
                    {
                        XmlElement xe = (XmlElement)xn;
                        xe.SetAttribute(attribute, value);
                    }
                }
                else
                {
                    XmlElement xe = doc.CreateElement(element);
                    if (attribute.Equals(""))
                        xe.InnerText = value;
                    else
                        xe.SetAttribute(attribute, value);
                    xn.AppendChild(xe);
                }
                //doc.Save(path);
            }
            catch { }
        }

        /// <summary>
        /// �ڽڵ��в����Ԫ��
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="element">Ԫ����</param>
        /**************************************
         * 
         * InsertElement("/Root", "Studio");
         * 
         *************************************/
        public void InsertElement(string node, string element)
        {
            Insert(node, element, "", "");
        }

        /// <summary>
        /// �ڽڵ��в�������Ե�Ԫ��
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="element">Ԫ����</param>
        /// <param name="attribute">������</param>
        /// <param name="value">����ֵ</param>
        /**************************************
         * 
         * InsertAttribute("/Root/Studio", "Site", "Name", "XX������");
         * 
         *************************************/
        public void InsertElementAndAttribute(string node, string element, string attribute, string value)
        {
            Insert(node, element, attribute, value);
        }

        /// <summary>
        /// �ڽڵ��в�������ĵ�Ԫ��
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="element">Ԫ����</param>
        /// <param name="value">����</param>
        /**************************************
         * 
         * InsertContent("/Root/Studio/Site[@Name='XX������']", "Master", "����");
         * InsertContent("/Root/Studio", "Master", "����");
         * 
         *************************************/
        public void InsertContent(string node, string element, string value)
        {
            Insert(node, element, "", value);
        }

        /// <summary>
        /// �ڽڵ��в�������
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">������</param>
        /// <param name="value">����ֵ</param>
        /**************************************
         * 
         * InsertAttribute("/Root/Studio/Site[@Name='XX������']", "Url", "http://www.XXX.com/");
         * InsertAttribute("/Root/Studio, "Url", "http://www.XXX.com/");
         * 
         *************************************/
        public void InsertAttribute(string node, string attribute, string value)
        {
            Insert(node, "", attribute, value);
        }

        /// <summary>
        /// �ڽڵ��в�������Դ����ĵ�Ԫ��
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="element">Ԫ����</param>
        /// <param name="attribute">������</param>
        /// <param name="avalue">����ֵ</param>
        /// <param name="content">����</param>
        public void InsertElementAndAttributeAndContent(string node, string element, string attribute, string avalue, string content)
        {
            try
            {
                xn = doc.SelectSingleNode(node);

                XmlElement xe = doc.CreateElement(element);
                xe.InnerText = content;
                xe.SetAttribute(attribute, avalue);
                xn.AppendChild(xe);

                //doc.Save(path);
            }
            catch { }
        }
        


        /// <summary>
        /// �޸�����
        /// </summary>        
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">���������ǿ�ʱ�޸ĸýڵ�����ֵ�������޸Ľڵ�ֵ</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        /**************************************************
         * ʹ��ʾ��:
         * Insert("/Node", "", "Value")
         * Insert("/Node", "Attribute", "Value")
         ************************************************/
        private void Update(string node, string attribute, string value)
        {
            try
            {
                xn = doc.SelectSingleNode(node);
                
                if (xn == null)
                {
                    string[] ele = node.Split('/');
                    InsertElement(ele[1], ele[ele.Length - 1]);
                    xn = doc.SelectSingleNode(node);
                }
                XmlElement xe = (XmlElement)xn;
                
                if (attribute.Equals(""))
                    xe.InnerText = value;
                else
                    xe.SetAttribute(attribute, value);
                //doc.Save(path);
            }
            catch { }
        }

        private void set(string section, string key, string value)
        {
            XmlNode xn = doc.SelectSingleNode(@"/root/"+section+"/"+key);
            if (xn == null)
            {
                xn = doc.SelectSingleNode(@"/root/"+section);
                XmlElement xe = doc.CreateElement(key);
                xe.InnerText=value;
                xn.AppendChild(xe);
            }
            else
            {
                XmlElement xe = (XmlElement)xn;
                xe.InnerText = value;
            }
        }

        public void setBody(string key,string value)
        {
            set("body", key, value);
        }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="value">����</param>
        /**************************************
         * 
         * UpdateContent("/Root/Studio", "XX������");
         * 
         *************************************/
        public void UpdateContent(string node,string value)
        {
            Update(node, "", value);
        }
        
        /// <summary>
        /// �޸�����ֵ
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">������</param>
        /// <param name="value">����ֵ</param>
        /**************************************
         * 
         * UpdateAttribute("/Root/Studio", "Name", "XX������");
         * 
         *************************************/
        public void UpdateAttribute(string node,string attribute, string value)
        {
            Update(node, attribute, value);
        }
        
        
        
        /// <summary>
        /// ɾ������
        /// </summary>        
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">���������ǿ�ʱɾ���ýڵ�����ֵ������ɾ���ڵ�ֵ</param>
        /// <param name="value">ֵ</param>
        /// <returns></returns>
        /**************************************************
         * ʹ��ʾ��:
         * Delete("/Node", "")
         * Delete("/Node", "Attribute")
         ************************************************/
        private void Delete(string node, string attribute)
        {
            try
            {
                xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                //doc.Save(path);
            }
            catch { }
        }
        
        /// <summary>
        /// ɾ��Ԫ��
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /**********************************************
         * 
         * DeleteElement("/Root/Studio");
         * 
         *********************************************/
        public void DeleteElement(string node)
        {
            Delete(node, "");
        }
        
        /// <summary>
        /// ɾ��ָ���ڵ������
        /// </summary>
        /// <param name="node">�ڵ�</param>
        /// <param name="attribute">������</param>
        /**********************************************
         * 
         * DeleteAttribute("/Root/Studio","Url");
         * 
         *********************************************/
        public void DeleteAttribute(string node, string attribute)
        {
            Delete(node, attribute);
        }
        public XmlDocument GetXmlDoc()
        {
            return doc;
        }
        public string GetBody()
        {
            Delete("/ebank/tellNo", "");
            Delete("/ebank/nodeNo", "");
            Delete("/ebank/tranCode", "");
            XmlNode xn1 = doc.SelectSingleNode("/ebank");

            //return Common.GbkToUtf8(xn1.InnerXml);
            return xn1.InnerXml;
        }
        public string GetBodyPage()
        {
            string recv;
            Delete("/ebank/tellNo", "");
            Delete("/ebank/nodeNo", "");
            Delete("/ebank/tranCode", "");
            XmlNode xn1 = doc.SelectSingleNode("/ebank/pageSize");
            recv = xn1.OuterXml;
            xn1 = doc.SelectSingleNode("/ebank/currentPage");
            recv += xn1.OuterXml;
            Delete("/ebank/pageSize", "");
            Delete("/ebank/currentPage", "");
            return recv;
        }
        public string GetBodyPage(string nodename)
        {
            string recv = "";
            Delete("/ebank/tellNo", "");
            Delete("/ebank/nodeNo", "");
            Delete("/ebank/tranCode", "");
            string[] arr = nodename.Split('|');
            foreach (string s in arr)
            {
                XmlNode xn1 = doc.SelectSingleNode("/ebank/" + s);
                recv += xn1.OuterXml;
                Delete("/ebank/" + s, "");
            }
           
            return recv;
        }

        public void AddList(XmlNodeList xn3,ref int count)
        {
            System.Text.StringBuilder strXml = new System.Text.StringBuilder();
            count = 0;
            strXml.AppendLine("<list>");
            ArrayList al = new ArrayList();
            foreach (XmlNode xn4 in xn3)
            {
                strXml.AppendLine("<row>");
                foreach (XmlNode xn5 in xn4.ChildNodes)
                {                    
                    if (xn5.ChildNodes.Count > 1)
                    {
                        if (!al.Contains(xn5.LocalName))
                        {
                            al.Add(xn5.LocalName);
                        }                   
                    }
                    else
                    {                        
                        strXml.AppendLine("<" + xn5.LocalName + ">" + xn5.InnerText + "</" + xn5.LocalName + ">");
                    }                    
                }
                foreach (string tar in al)
                {
                    XmlDocument doc11 = new XmlDocument();
                    doc11.LoadXml(xn4.OuterXml);
                    strXml.AppendLine("<" + tar + ">");
                    XmlNodeList xnl11 = doc11.SelectNodes("/list/" + tar);
                    foreach (XmlNode xn6 in xnl11)
                    {
                        strXml.AppendLine("<row>");
                        foreach (XmlNode xn7 in xn6.ChildNodes)
                        {
                            strXml.AppendLine("<" + xn7.LocalName + ">" + xn7.InnerText + "</" + xn7.LocalName + ">");
                        }
                        strXml.AppendLine("</row>");
                    }
                    strXml.AppendLine("</" + tar + ">");
                }

                strXml.AppendLine("</row>");
                count++;
            }
            strXml.AppendLine("</list>");

            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(strXml.ToString());

            XmlNode root1 = doc.SelectSingleNode("//ebank//list");
            foreach (XmlNode n in doc2.DocumentElement.ChildNodes)
            {
                XmlNode root2 = doc.ImportNode(n, true);
                root1.AppendChild(root2);
            } 
        }
        public void AddChild(XmlNode xn3)
        {
            XmlNode root1 = doc.SelectSingleNode("//ebank");
            //string xml = xn3.InnerXml;
            //XmlDocument doc2 = new XmlDocument();
            //doc2.LoadXml(xml);
            foreach (XmlNode xn4 in xn3.ChildNodes)
            {
                XmlNode xn5 = doc.ImportNode(xn4, true);
                root1.AppendChild(xn5);
            }
        }

        public XmlDocument DataTableToXml(DataTable dt)
        {
            System.Text.StringBuilder strXml = new System.Text.StringBuilder();
            strXml.AppendLine("<list>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strXml.AppendLine("<row>");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strXml.AppendLine("<" + dt.Columns[j].ColumnName + ">" + dt.Rows[i][j] + "</" + dt.Columns[j].ColumnName + ">");
                }
                strXml.AppendLine("</row>");
            }
            strXml.AppendLine("</list>");

            
            //DeleteElement("//ebank//list");
            
            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(strXml.ToString());

            XmlNode root1 = doc.SelectSingleNode("//ebank//list");
            foreach (XmlNode n in doc2.DocumentElement.ChildNodes)
            {
                XmlNode root2 = doc.ImportNode(n, true);
                root1.AppendChild(root2);
            } 
            
            
            return doc;
        }      

    }
}
