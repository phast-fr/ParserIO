﻿CREATE TABLE [dbo].[BarcodesStore] (
    [localId]                INT IDENTITY (1, 1) NOT NULL,
    [AnalyseId]              INT            NULL,
    [TimeStamp]              DATETIME       NULL,
    [ParserIOVersion]        NVARCHAR (MAX) NULL,
    [Barcode]                NVARCHAR (MAX) NULL,
    [InputCode]              NVARCHAR (MAX) NOT NULL,
    [Type]                   NVARCHAR (MAX) NOT NULL,
    [SubType]                NVARCHAR (MAX) NULL,
    [ContainsOrMayContainId] NVARCHAR (MAX) NULL,
    [ACL]                    NVARCHAR (MAX) NULL,
    [ADDITIONALID]           NVARCHAR (MAX) NULL,
    [CUSTPARTNO]             NVARCHAR (MAX) NULL,
    [BESTBEFORE]             NVARCHAR (MAX) NULL,
    [CIP]                    NVARCHAR (MAX) NULL,
    [CONTENT]                NVARCHAR (MAX) NULL,
    [COUNT]                  NVARCHAR (MAX) NULL,
    [Expiry]                 NVARCHAR (MAX) NULL,
    [Family]                 NVARCHAR (MAX) NULL,
    [GTIN]                   NVARCHAR (MAX) NULL,
    [INTERNAL_90]            NVARCHAR (MAX) NULL,
    [INTERNAL_91]            NVARCHAR (MAX) NULL,
    [INTERNAL_92]            NVARCHAR (MAX) NULL,
    [INTERNAL_93]            NVARCHAR (MAX) NULL,
    [INTERNAL_94]            NVARCHAR (MAX) NULL,
    [INTERNAL_95]            NVARCHAR (MAX) NULL,
    [INTERNAL_96]            NVARCHAR (MAX) NULL,
    [INTERNAL_97]            NVARCHAR (MAX) NULL,
    [INTERNAL_98]            NVARCHAR (MAX) NULL,
    [INTERNAL_99]            NVARCHAR (MAX) NULL,
    [LIC]                    NVARCHAR (MAX) NULL,
    [Lot]                    NVARCHAR (MAX) NULL,
    [LPP]                    NVARCHAR (MAX) NULL,
    [NaS7]                   NVARCHAR (MAX) NULL,
    [NormalizedBESTBEFORE]   NVARCHAR (MAX) NULL,
    [NormalizedExpiry]       NVARCHAR (MAX) NULL,
    [NormalizedPRODDATE]     NVARCHAR (MAX) NULL,
    [PCN]                    NVARCHAR (MAX) NULL,
    [PRODDATE]               NVARCHAR (MAX) NULL,
    [Quantity]               NVARCHAR (MAX) NULL,
    [Reference]              NVARCHAR (MAX) NULL,
    [Serial]                 NVARCHAR (MAX) NULL,
    [SSCC]                   NVARCHAR (MAX) NULL,
    [StorageLocation]        NVARCHAR (MAX) NULL,
    [UDI]                    NVARCHAR (MAX) NULL,
    [UDI_DI]                 NVARCHAR (MAX) NULL,
    [Issuer]                 NVARCHAR (MAX) NULL,
    [UoM]                    NVARCHAR (MAX) NULL,
    [VARCOUNT]               NVARCHAR (MAX) NULL,
    [VARIANT]                NVARCHAR (MAX) NULL,
    [Commentary]             NVARCHAR (MAX) NULL,
    [SymbologyID]            NVARCHAR (MAX) NULL,
    [SymbologyIDDesignation] NVARCHAR (MAX) NULL,
    [EAN]                    NVARCHAR (MAX) NULL,
    [NaSIdParamName]         NVARCHAR (MAX) NULL,
    [UPN]                    NVARCHAR (MAX) NULL,
    [AdditionalInformation]  NVARCHAR (MAX) NULL,
    [Identifiers]            NVARCHAR (MAX) NULL
);











