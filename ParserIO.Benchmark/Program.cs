using ParserIO.Benchmark.ParserIO.WS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ParserIO.Benchmark
{
    class Program
    {
        enum Provider
        {
            dll,   //ParserIO.Core
            ws,    //ParserIO.WS
            wcf,   //ParserIO.WCF
            webapi, //ParserIO.WebAPI
            fdagudid // FDA GUDID Parser
        }


        static void Main(string[] args)
        {

            DateTime dt = DateTime.Now;

            Provider value = Provider.ws;
            string workingFolder = @"C:\_Phast\tools\ParserIO\";
            string sourceFileName = workingFolder + "Barcodestore_master_20240105100000.xml";

            string outputFileName = workingFolder + "Barcodestore_" + value + "_"+ dt.ToString("yyyymmddhhmmss") + ".xml";

            StreamReader source;
            XmlDocument document;
            XmlNodeList analyses;
            source = new StreamReader(sourceFileName, Encoding.UTF8);
            document = new XmlDocument();
            document.Load(source);

            DAO.Barcodestore bcs = new DAO.Barcodestore();
            bcs.Version = dt.ToString("yyyymmddhhmmss");
            bcs.ProviderName = value.ToString();
            bcs.Analyses = new List<DAO.Analyse>();
            
            analyses = document.GetElementsByTagName("Analyse");

            foreach (XmlElement sourceAnalyse in analyses)
            {
                DAO.Analyse x = new DAO.Analyse();

                x.AnalyseId = Convert.ToInt32(sourceAnalyse.GetElementsByTagName("AnalyseId")[0].InnerText);
                x.TimeStamp = Convert.ToDateTime(sourceAnalyse.GetElementsByTagName("TimeStamp")[0].InnerText);       
                x.Barcode = sourceAnalyse.GetElementsByTagName("Barcode")[0].InnerText;
                x.Commentary = sourceAnalyse.GetElementsByTagName("Commentary")[0].InnerText;
                
                if (value == Provider.dll)
                {
                    DAO.InformationSet targetAnalyse = new DAO.InformationSet();
                    Core.Functions client = new Core.Functions();
                    targetAnalyse = client.GetFullInformationSet(x.Barcode);

                    x.InformationSet.InputCode = targetAnalyse.InputCode;
                    x.InformationSet.ParserIOVersion = targetAnalyse.ParserIOVersion;
                    x.InformationSet.Type = targetAnalyse.Type;
                    x.InformationSet.SubType = targetAnalyse.SubType;
                    //x.InformationSet.executeResult = targetAnalyse.executeResult;
                    x.InformationSet.ACL = targetAnalyse.ACL;
                    x.InformationSet.ADDITIONALID = targetAnalyse.ADDITIONALID;
                    x.InformationSet.BESTBEFORE = targetAnalyse.BESTBEFORE;
                    x.InformationSet.CIP = targetAnalyse.CIP;
                    x.InformationSet.Company = ""; // targetAnalyse.Company; //Obsolete
                    x.InformationSet.ContainsOrMayContainId = targetAnalyse.ContainsOrMayContainId;
                    x.InformationSet.Identifiers = targetAnalyse.Identifiers;
                    x.InformationSet.CONTENT = targetAnalyse.CONTENT;
                    x.InformationSet.COUNT = targetAnalyse.COUNT;
                    x.InformationSet.CUSTPARTNO = targetAnalyse.CUSTPARTNO;
                    x.InformationSet.EAN = targetAnalyse.EAN;
                    x.InformationSet.Expiry = targetAnalyse.Expiry;
                    x.InformationSet.Family = targetAnalyse.Family;
                    x.InformationSet.GTIN = targetAnalyse.GTIN;
                    x.InformationSet.INTERNAL_90 = targetAnalyse.INTERNAL_90;
                    x.InformationSet.INTERNAL_91 = targetAnalyse.INTERNAL_91;
                    x.InformationSet.INTERNAL_92 = targetAnalyse.INTERNAL_92;
                    x.InformationSet.INTERNAL_93 = targetAnalyse.INTERNAL_93;
                    x.InformationSet.INTERNAL_94 = targetAnalyse.INTERNAL_94;
                    x.InformationSet.INTERNAL_95 = targetAnalyse.INTERNAL_95;
                    x.InformationSet.INTERNAL_96 = targetAnalyse.INTERNAL_96;
                    x.InformationSet.INTERNAL_97 = targetAnalyse.INTERNAL_97;
                    x.InformationSet.INTERNAL_98 = targetAnalyse.INTERNAL_98;
                    x.InformationSet.INTERNAL_99 = targetAnalyse.INTERNAL_99;
                    x.InformationSet.LIC = targetAnalyse.LIC;
                    x.InformationSet.Lot = targetAnalyse.Lot;
                    x.InformationSet.LPP = targetAnalyse.LPP;
                    x.InformationSet.NaS7 = targetAnalyse.NaS7;
                    x.InformationSet.NaSIdParamName = targetAnalyse.NaSIdParamName;
                    x.InformationSet.NormalizedBESTBEFORE = targetAnalyse.NormalizedBESTBEFORE;
                    x.InformationSet.NormalizedExpiry = targetAnalyse.NormalizedExpiry;
                    x.InformationSet.NormalizedPRODDATE = targetAnalyse.NormalizedPRODDATE;
                    x.InformationSet.PCN = targetAnalyse.PCN;
                    x.InformationSet.PRODDATE = targetAnalyse.PRODDATE;
                    x.InformationSet.Product = ""; // targetAnalyse.Product; //Obsolete
                    x.InformationSet.Quantity = targetAnalyse.Quantity;
                    x.InformationSet.Reference = targetAnalyse.Reference;
                    x.InformationSet.Serial = targetAnalyse.Serial;
                    x.InformationSet.SSCC = targetAnalyse.SSCC;
                    x.InformationSet.StorageLocation = targetAnalyse.StorageLocation;
                    x.InformationSet.SymbologyID = targetAnalyse.SymbologyID;
                    x.InformationSet.SymbologyIDDesignation = targetAnalyse.SymbologyIDDesignation;
                    x.InformationSet.UDI = targetAnalyse.UDI;
                    x.InformationSet.UDI_DI = targetAnalyse.UDI_DI;
                    x.InformationSet.Issuer = targetAnalyse.Issuer;
                    x.InformationSet.UoM = targetAnalyse.UoM;
                    x.InformationSet.UPN = targetAnalyse.UPN;
                    x.InformationSet.VARCOUNT = targetAnalyse.VARCOUNT;
                    x.InformationSet.VARIANT = targetAnalyse.VARIANT;
                    x.InformationSet.AdditionalInformation = targetAnalyse.AdditionalInformation;
                }
                else if (value==Provider.ws)
                {
                    ParserIO.WS.ParserIO client = new ParserIO.WS.ParserIO();

                    ParserIO.WS.InformationSetRequestType Request = new ParserIO.WS.InformationSetRequestType();
                    Request.UserId = "";
                    Request.PinCode = "";
                    Request.Code = x.Barcode;

                    ParserIO.WS.InformationSetResponseType Response = new InformationSetResponseType();
                    Response = client.GetFullInformationSet(Request);

                    x.InformationSet.InputCode = Response.InformationSet.InputCode;
                    x.InformationSet.ParserIOVersion = Response.InformationSet.ParserIOVersion;
                    x.InformationSet.Type = Response.InformationSet.Type;
                    x.InformationSet.SubType = Response.InformationSet.SubType;
                    //x.InformationSet.executeResult = Response.InformationSet.executeResult;
                    x.InformationSet.ACL = Response.InformationSet.ACL;
                    x.InformationSet.ADDITIONALID = Response.InformationSet.ADDITIONALID;
                    x.InformationSet.BESTBEFORE = Response.InformationSet.BESTBEFORE;
                    x.InformationSet.CIP = Response.InformationSet.CIP;
                    x.InformationSet.Company = ""; // Response.InformationSet.Company; //Obsolete
                    x.InformationSet.ContainsOrMayContainId = Response.InformationSet.ContainsOrMayContainId;
                    
                    foreach (ParserIO.WS.Identifier item in Response.InformationSet.Identifiers)
                    {
                        DAO.Identifier identifier = new DAO.Identifier();
                        identifier.Value = item.Value;

                        x.InformationSet.Identifiers.Add(identifier);
                    }

                    x.InformationSet.CONTENT = Response.InformationSet.CONTENT;
                    x.InformationSet.COUNT = Response.InformationSet.COUNT;
                    x.InformationSet.CUSTPARTNO = Response.InformationSet.CUSTPARTNO;
                    x.InformationSet.EAN = Response.InformationSet.EAN;
                    x.InformationSet.Expiry = Response.InformationSet.Expiry;
                    x.InformationSet.Family = Response.InformationSet.Family;
                    x.InformationSet.GTIN = Response.InformationSet.GTIN;
                    x.InformationSet.INTERNAL_90 = Response.InformationSet.INTERNAL_90;
                    x.InformationSet.INTERNAL_91 = Response.InformationSet.INTERNAL_91;
                    x.InformationSet.INTERNAL_92 = Response.InformationSet.INTERNAL_92;
                    x.InformationSet.INTERNAL_93 = Response.InformationSet.INTERNAL_93;
                    x.InformationSet.INTERNAL_94 = Response.InformationSet.INTERNAL_94;
                    x.InformationSet.INTERNAL_95 = Response.InformationSet.INTERNAL_95;
                    x.InformationSet.INTERNAL_96 = Response.InformationSet.INTERNAL_96;
                    x.InformationSet.INTERNAL_97 = Response.InformationSet.INTERNAL_97;
                    x.InformationSet.INTERNAL_98 = Response.InformationSet.INTERNAL_98;
                    x.InformationSet.INTERNAL_99 = Response.InformationSet.INTERNAL_99;
                    x.InformationSet.LIC = Response.InformationSet.LIC;
                    x.InformationSet.Lot = Response.InformationSet.Lot;
                    x.InformationSet.LPP = Response.InformationSet.LPP;
                    x.InformationSet.NaS7 = Response.InformationSet.NaS7;
                    x.InformationSet.NaSIdParamName = Response.InformationSet.NaSIdParamName;
                    x.InformationSet.NormalizedBESTBEFORE = Response.InformationSet.NormalizedBESTBEFORE;
                    x.InformationSet.NormalizedExpiry = Response.InformationSet.NormalizedExpiry;
                    x.InformationSet.NormalizedPRODDATE = Response.InformationSet.NormalizedPRODDATE;
                    x.InformationSet.PCN = Response.InformationSet.PCN;
                    x.InformationSet.PRODDATE = Response.InformationSet.PRODDATE;
                    x.InformationSet.Product = ""; // Response.InformationSet.Product; //Obsolete
                    x.InformationSet.Quantity = Response.InformationSet.Quantity;
                    x.InformationSet.Reference = Response.InformationSet.Reference;
                    x.InformationSet.Serial = Response.InformationSet.Serial;
                    x.InformationSet.SSCC = Response.InformationSet.SSCC;
                    x.InformationSet.StorageLocation = Response.InformationSet.StorageLocation;
                    x.InformationSet.SymbologyID = Response.InformationSet.SymbologyID;
                    x.InformationSet.SymbologyIDDesignation = Response.InformationSet.SymbologyIDDesignation;
                    x.InformationSet.UDI = Response.InformationSet.UDI;
                    x.InformationSet.UDI_DI = Response.InformationSet.UDI_DI;
                    x.InformationSet.Issuer = Response.InformationSet.Issuer;
                    x.InformationSet.UoM = Response.InformationSet.UoM;
                    x.InformationSet.UPN = Response.InformationSet.UPN;
                    x.InformationSet.VARCOUNT = Response.InformationSet.VARCOUNT;
                    x.InformationSet.VARIANT = Response.InformationSet.VARIANT;
                    x.InformationSet.AdditionalInformation = Response.InformationSet.AdditionalInformation;


                }
                else if (value == Provider.wcf)
                {
                    //todo
                }
                else if (value == Provider.webapi)
                {
                    //todo
                }
                else if (value == Provider.fdagudid)
                {
                    //todo
                }

                

                bcs.Analyses.Add(x);
                Console.WriteLine(x.AnalyseId);
            }

            using (StreamWriter myWriter = new StreamWriter(outputFileName, false))
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(DAO.Barcodestore), new XmlRootAttribute("Barcodestore"));
                mySerializer.Serialize(myWriter, bcs);
            }

        }
    }
}
