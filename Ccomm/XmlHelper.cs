using System;
using System.Xml;
using System.Data;
using System.Collections;

namespace ebankGateway
{
    /// <summary>
    /// XmlHelper 的摘要说明
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
            //创建一个根节点（一级）
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
        /// 读取数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
        /// <returns>string</returns>
        /**************************************************
         * 使用示列:
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
        /// 读取指定节点的串联值
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns></returns>
        public string ReadByNode(string node)
        {
            return Read(node, "");
        }
        /// <summary>
        /// 读取指定属性的节点的串联值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public string ReadByAttribute(string node,string attribute,string value)
        {
            return Read(node + "[@" + attribute + "='" + value + "']", "");
        }
        /// <summary>
        /// 读取属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名称</param>
        /// <returns></returns>
        public string ReadAttributeValue(string node, string attribute)
        {
            return Read(node, attribute);
        }



        /// <summary>
        /// 插入数据
        /// </summary>        
        /// <param name="node">节点</param>
        /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
        /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
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
        /// 在节点中插入空元素
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="element">元素名</param>
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
        /// 在节点中插入带属性的元素
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="element">元素名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性值</param>
        /**************************************
         * 
         * InsertAttribute("/Root/Studio", "Site", "Name", "XX工作室");
         * 
         *************************************/
        public void InsertElementAndAttribute(string node, string element, string attribute, string value)
        {
            Insert(node, element, attribute, value);
        }

        /// <summary>
        /// 在节点中插入带正文的元素
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="element">元素名</param>
        /// <param name="value">正文</param>
        /**************************************
         * 
         * InsertContent("/Root/Studio/Site[@Name='XX工作室']", "Master", "张总");
         * InsertContent("/Root/Studio", "Master", "张总");
         * 
         *************************************/
        public void InsertContent(string node, string element, string value)
        {
            Insert(node, element, "", value);
        }

        /// <summary>
        /// 在节点中插入属性
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性值</param>
        /**************************************
         * 
         * InsertAttribute("/Root/Studio/Site[@Name='XX工作室']", "Url", "http://www.XXX.com/");
         * InsertAttribute("/Root/Studio, "Url", "http://www.XXX.com/");
         * 
         *************************************/
        public void InsertAttribute(string node, string attribute, string value)
        {
            Insert(node, "", attribute, value);
        }

        /// <summary>
        /// 在节点中插入带属性带正文的元素
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="element">元素名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="avalue">属性值</param>
        /// <param name="content">正文</param>
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
        /// 修改数据
        /// </summary>        
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
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
        /// 修改正文
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="value">正文</param>
        /**************************************
         * 
         * UpdateContent("/Root/Studio", "XX工作室");
         * 
         *************************************/
        public void UpdateContent(string node,string value)
        {
            Update(node, "", value);
        }
        
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性值</param>
        /**************************************
         * 
         * UpdateAttribute("/Root/Studio", "Name", "XX工作室");
         * 
         *************************************/
        public void UpdateAttribute(string node,string attribute, string value)
        {
            Update(node, attribute, value);
        }
        
        
        
        /// <summary>
        /// 删除数据
        /// </summary>        
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        /**************************************************
         * 使用示列:
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
        /// 删除元素
        /// </summary>
        /// <param name="node">节点</param>
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
        /// 删除指定节点的属性
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名</param>
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
