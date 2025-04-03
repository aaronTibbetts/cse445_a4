using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Remoting.Contexts;
using Microsoft.SqlServer.Server;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "https://aarontibbetts.github.io/cse445_a4/Hotels.xml";
        public static string xmlErrorURL = "https://aarontibbetts.github.io/cse445_a4/HotelsErrors.xml";
        public static string xsdURL = "https://aarontibbetts.github.io/cse445_a4/Hotels.xsd";
        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            //result = Verification(xmlErrorURL, xsdURL);
            //Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            string message = " ";
            XmlSchemaSet sc = new XmlSchemaSet();
            sc.Add(null, xsdUrl);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;
            XmlReader reader = XmlReader.Create(xmlUrl, settings);
            try
            {
                while (reader.Read())
                {
                    message = "No Error";
                }
            }
            catch (System.Xml.Schema.XmlSchemaValidationException ex)
            {
                message = ex.ToString();
            }
            return message; 
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
        }

        public static string Xml2Json(string xmlUrl)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented,true);
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }

        
    }

}
