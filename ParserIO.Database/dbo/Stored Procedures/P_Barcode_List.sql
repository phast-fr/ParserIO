﻿CREATE PROCEDURE [dbo].[P_Barcode_List]
AS
BEGIN
SELECT [localId]
      ,[AnalyseId]
      ,[TimeStamp]
	  ,[Barcode]
	  ,[SymbologyID]
      ,[Type]
      ,[SubType]
      ,[ContainsOrMayContainId]
      ,[ACL]
      ,[ADDITIONALID]
      ,[BESTBEFORE]
      ,[CIP]
      ,[CONTENT]
      ,[COUNT]
	  ,[EAN]
      ,[Expiry]
      ,[Family]
      ,[GTIN]
	  ,[INTERNAL_91]
	  ,[INTERNAL_92]
	  ,[INTERNAL_93]
	  ,[INTERNAL_94]
	  ,[INTERNAL_95]
	  ,[INTERNAL_96]
	  ,[INTERNAL_97]
	  ,[INTERNAL_98]
	  ,[INTERNAL_99]
      ,[LIC]
      ,[Lot]
      ,[LPP]
      ,[NaS7]
	  ,[NaSIdParamName]
      ,[NormalizedBESTBEFORE]
      ,[NormalizedExpiry]
      ,[NormalizedPRODDATE]
      ,[PCN]
      ,[PRODDATE]
      ,[Quantity]
      ,[Reference]
      ,[Serial]
      ,[SSCC]
	  ,[StorageLocation]
      ,[UDI]
      ,[UoM]
	  ,[UPN]
      ,[VARCOUNT]
      ,[VARIANT]
	  ,[AdditionalInformation]
      ,[Commentary]
  FROM [dbo].[BarcodesStore]
  ORDER BY [AnalyseId]
  END