﻿// “Copyright (C) 2009-2014 Association Réseau Phast”
// This file is part of ParserIO.
// ParserIO is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// ParserIO is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with ParserIO.  If not, see <http://www.gnu.org/licenses/>.
//
//For more information, please consult the ParserIO web site at
//<http://parserio.codeplex.com>
//
// Fomm1.cs (CIOdm_parser.Form1)

// 11/01/10 Version 0.2
// 11/01/10 NC [fr] Corrigé GTIN() pour inclure la clé
//             [en] Fixed GTIN() for including the key
// 11/01/10 NC [fr] Modifié Variante() et Lot() pour supporter GS1-128 01.17.30.10
//             [en] Modified Variante() and Lot() for supporting GS1-128 01.17.30.10
// 12/01/10 NC [fr] Corrigé Date() pour supporter la subType GS1-128 01
//             [en] Fixed Date() for supporting variant GS1-128 01

// 12/02/10 Version 0.3
// 12/02/10 NC [fr] Testé Variante() pour toutes les variantes d'HIBC (sauf +$+ et suivantes)
//             [en] Tested Variante() for all the variants of HIBC (except +$+ and following ones)

// 23/02/10 Version 0.4
// 23/02/10 NC [fr] Intégré la contribution de l'équipe chinoise concernant le calcul de la date "normale"
//             [en] Integrated the contribution from the chineese team concerning the calculation of the "normal" date

// 24/02/10 Version 0.5
// 24/02/10 NC [fr] Modifié Variante() et Lot() pour supporter GS1-128 01.17.30.10 avec 30 de longueur 4
//             [en] Modified Variante() and Lot() for supporting GS1-128 01.17.30.10 with a length of 4 for 30
// 24/02/10 NC [fr] Modifié Date(), Lot() et GetDateType() pour supporter HIBC Secondary.N avec numéro de lot
//             [en] Modified Date(), Lot() and GetDateType() for supporting HIBC Secondary.N with lot number
// 24/02/10 NC [fr] Supporté les variantes GS1-128 10 et 10.17
//             [en] Supported variants GS1-128 10 and 10.17

// 04/06/10 Version 0.6
// 04/06/10 NC [fr] Déplacé les fonctions de parsing vers une DLL
//             [en] Moved the parsing functions to a DLL

// 07/09/10 Version 0.9
// 07/09/10 NC [fr] Ajouté une fonction Reference
//             [en] Added a "Reference" function

// 30/11/10 Version 0.12
// 30/11/10 DU [fr] Ajouté une fonction HIBC Unit of Measure
//             [en] Added a HIBC Unit of Measure function

// 24/01/11 Version 0.19
// 24/01/11 DU [fr] Ajouté une fonction SSCC
//             [en] Added a SSCC function
// 24/01/11 DU [fr] Ajouté une fonction CONTENT
//             [en] Added a CONTENT function

// 25/02/11 Version 0.21
// 25/02/11 DU [fr] Ajouté Quantite
//             [en] Added Quantite
// 25/02/11 DU [fr] Ajouté VARCOUNT
//             [en] Added VARCOUNT
// 25/02/11 DU [fr] Ajouté COUNT
//             [en] Added COUNT

// 11/03/11 Version 0.23
// 11/03/11 DU [fr] Ajouté PRODDATE
//             [en] Added PRODDATE
// 11/03/11 DU [fr] Ajouté BESTBEFORE
//             [en] Added BESTBEFORE
// 11/03/11 DU [fr] Ajouté Serial
//             [en] Added Serial
//             [fr] Ajouté VARIANT
//             [en] Added VARIANT

// 01/04/11 Version 0.25
// 01/04/11 DU [fr] Renommé la description de Parser à ParserIO
//             [en] Rename description from Parser to ParserIO
// 13/04/11 DU [fr] Ajouté NormalizedPRODDATE
//             [en] Added NormalizedPRODDATE
// 13/04/11 DU [fr] Ajouté NormalizedBESTBEFORE
//             [en] Added NormalizedBESTBEFORE

// 13/09/11 Version 0.27
// 13/09/11 DU [fr] Ajouté ACL
//             [en] Added ACL
// 13/09/11 DU [fr] Supporté le Type ACL13
//             [en] Supported ACL13 Type

// 12-11-2014 Version 1.0.0.4
// 12-11-2014 DU [fr] Ajouté UPN
//               [en] Added UPN
// 12-11-2014 DU [fr] Ajouté EAN
//               [en] Added EAN
// 12-11-2014 DU [fr] Ajouté SymbologyID
//               [en] Added SymbologyID

// [fr] Voir aussi les commentaires dans Parser_functions.cs (CIOdm_Parser_functions.Parser_functions)
// [en] See also comments in Parser_functions.cs (CIOdm_Parser_functions.Parser_functions)

using System;
using System.Windows.Forms;
using System.Globalization;
using ParserIO.DAO;
using System.Reflection;

namespace ParserIO.WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Parse(object sender, EventArgs e)
        {
            buttonParser.Enabled = false;

            try
            {
                Core.Functions client = new Core.Functions();
                string Barcode = textBoxBarcode.Text;
                DAO.InformationSet result = client.GetFullInformationSet(Barcode);
                
                textBoxParserIOVersion.Text = result.ParserIOVersion;

                textBoxAdditionalInformation.Text = result.AdditionalInformation;
                textBoxACL.Text = result.ACL;
                textBoxADDITIONALID.Text = result.ADDITIONALID;
                textBoxBESTBEFORE.Text = result.BESTBEFORE;
                textBoxCIP.Text = result.CIP;
                textBoxcontainsOrMayContainId.Text = result.ContainsOrMayContainId.ToString(); //check type

                //Identifiers
                string IdentifiersRawList = string.Empty;
                bool isFirst = true;
                string returnCarriageLine = string.Empty;

                foreach (Identifier x in result.Identifiers)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        returnCarriageLine = "\r\n";
                    }
                    textBoxIdentifiers.AppendText(returnCarriageLine + x.Value);
                }

                textBoxCONTENT.Text = result.CONTENT;
                textBoxCOUNT.Text = result.COUNT;
                textBoxCUSTPARTNO.Text = result.CUSTPARTNO;
                textBoxEAN.Text = result.EAN;
                textBoxExpiry.Text = result.Expiry;
                textBoxFamily.Text = result.Family;
                textBoxGTIN.Text = result.GTIN;
                textBoxLIC.Text = result.LIC;
                textBoxLot.Text = result.Lot;
                textBoxLPP.Text = result.LPP;
                textBoxNaS7.Text = result.NaS7;
                textBoxNormalizedBESTBEFORE.Text = result.NormalizedBESTBEFORE;
                textBoxNormalizedExpiry.Text = result.NormalizedExpiry;
                textBoxNormalizedPRODDATE.Text = result.NormalizedPRODDATE;
                textBoxPCN.Text = result.PCN;
                textBoxPRODDATE.Text = result.PRODDATE;
                textBoxQuantity.Text = result.Quantity;
                textBoxReference.Text = result.Reference;
                textBoxNaSIdParamName.Text = result.NaSIdParamName;
                textBoxSerial.Text = result.Serial;
                textBoxSSCC.Text = result.SSCC;
                textBoxStorageLocation.Text = result.StorageLocation;
                textBoxSymbologyID.Text = result.SymbologyID;
                textBoxSymbologyIDDesignation.Text = result.SymbologyIDDesignation;
                textBoxType.Text = result.Type;
                textBoxUoM.Text = result.UoM;
                textBoxUPN.Text = result.UPN;
                textBoxVARCOUNT.Text = result.VARCOUNT;
                textBoxVARIANT.Text = result.VARIANT;
                textBoxVariante.Text = result.SubType;
                textBoxUDI.Text = result.UDI;
                textBoxUDI_DI.Text = result.UDI_DI;
                textBoxIssuer.Text = result.Issuer;
                textBoxINTERNAL_90.Text = result.INTERNAL_90;
                textBoxINTERNAL_91.Text = result.INTERNAL_91;
                textBoxINTERNAL_92.Text = result.INTERNAL_92;
                textBoxINTERNAL_93.Text = result.INTERNAL_93;
                textBoxINTERNAL_94.Text = result.INTERNAL_94;
                textBoxINTERNAL_95.Text = result.INTERNAL_95;
                textBoxINTERNAL_96.Text = result.INTERNAL_96;
                textBoxINTERNAL_97.Text = result.INTERNAL_97;
                textBoxINTERNAL_98.Text = result.INTERNAL_98;
                textBoxINTERNAL_99.Text = result.INTERNAL_99;

                if (result.Type == "HIBC")
                {
                    tabControlOutput.SelectedTab = hibcTabPage;
                }
                else if (result.Type.Contains("GS1") | result.Type == "EAN 13")
                {
                    tabControlOutput.SelectedTab = gs1TabPage;
                }
                else if (result.Type == "NaS")
                {
                    tabControlOutput.SelectedTab = nasTabPage;
                }

                textBoxExecuteResult.Text = Results.Ok.ToString();
            }
            catch (Exception ex)
            {
                textBoxExecuteResult.Text = Results.GeneralError.ToString();
            }
            finally
            {
                buttonParser.Enabled = false;
            }
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            textBoxParserIOVersion.Clear();
            textBoxExecuteResult.Clear();
            textBoxAdditionalInformation.Clear();
            textBoxACL.Clear();
            textBoxADDITIONALID.Clear();
            textBoxBESTBEFORE.Clear();
            textBoxCIP.Clear();
            textBoxcontainsOrMayContainId.Clear();
            textBoxIdentifiers.Clear();
            textBoxCONTENT.Clear();
            textBoxCOUNT.Clear();
            textBoxEAN.Clear();
            textBoxCUSTPARTNO.Clear();
            textBoxExpiry.Clear();
            textBoxFamily.Clear();
            textBoxGTIN.Clear();
            textBoxINTERNAL_90.Clear();
            textBoxINTERNAL_91.Clear();
            textBoxINTERNAL_92.Clear();
            textBoxINTERNAL_93.Clear();
            textBoxINTERNAL_94.Clear();
            textBoxINTERNAL_95.Clear();
            textBoxINTERNAL_96.Clear();
            textBoxINTERNAL_97.Clear();
            textBoxINTERNAL_98.Clear();
            textBoxINTERNAL_99.Clear();
            textBoxLIC.Clear();
            textBoxLot.Clear();
            textBoxLPP.Clear();
            textBoxNaS7.Clear();
            textBoxNormalizedBESTBEFORE.Clear();
            textBoxNormalizedExpiry.Clear();
            textBoxNormalizedPRODDATE.Clear();
            textBoxPCN.Clear();
            textBoxPRODDATE.Clear();
            textBoxQuantity.Clear();
            textBoxReference.Clear();
            textBoxNaSIdParamName.Clear();
            textBoxSerial.Clear();
            textBoxSSCC.Clear();
            textBoxStorageLocation.Clear();
            textBoxSymbologyID.Clear();
            textBoxSymbologyIDDesignation.Clear();
            textBoxType.Clear();
            textBoxUoM.Clear();
            textBoxUPN.Clear();
            textBoxVARCOUNT.Clear();
            textBoxVARIANT.Clear();
            textBoxVariante.Clear();
            textBoxUDI.Clear();
            textBoxUDI_DI.Clear();
            textBoxIssuer.Clear();

           tabControlOutput.SelectedTab = hibcTabPage;
           this.ActiveControl = textBoxBarcode;

            textBoxBarcode.Focus();
            
            buttonParser.Enabled = true;
        }

        private void aboutParserIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox MyAboutBox = new AboutBox();
            MyAboutBox.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
